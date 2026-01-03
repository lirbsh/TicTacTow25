using CommunityToolkit.Mvvm.Messaging;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class Animatioms : AnimatiomsModel
    {
        public override void StartOpacityAnimation()
        {
            timerSettings.AnimationType = AnimationTypes.Opacity;
            WeakReferenceMessenger.Default.Send(new AppMessage<TimerSettings>(timerSettings));
        }
        public override void StartTextAnimation(string text)
        {
            textToAnimate = text;
            timerSettings.AnimationType = AnimationTypes.Text;
            timerSettings.TotalTimeInMilliseconds = text.Length * Keys.SecondToMillisconds;
            timerSettings.IntervalInMilliseconds = Keys.SecondToMillisconds;
            WeakReferenceMessenger.Default.Send(new AppMessage<TimerSettings>(timerSettings));
        }
        private void OnMessageReceived(long timeLeft)
        {
            if (timeLeft == Keys.FinishedSignal)
            {
                Opacity = 1.0;
                if (IsLooping)
                {
                    incrementOpacity = !incrementOpacity;
                    StartOpacityAnimation();
                }
                else
                    WeakReferenceMessenger.Default.Unregister<AppMessage<long>>(this);
            }
            else if(incrementOpacity)
                Opacity = (double)(Keys.TotalAnimationTime - timeLeft) / Keys.TotalAnimationTime;
            else
                Opacity = (double)timeLeft / Keys.TotalAnimationTime;
            OpacityChanged?.Invoke(this, EventArgs.Empty);
        }
        private void OnMessageReceived(int timeLeft)
        {
            if (timeLeft == Keys.FinishedSignal)
            {
                Text = textToAnimate;
                if (IsLooping)
                    StartTextAnimation(textToAnimate);
                else
                    WeakReferenceMessenger.Default.Unregister<AppMessage<int>>(this);
            }
            else
            {
                int charsToShow = textToAnimate.Length - (timeLeft / Keys.SecondToMillisconds);
                Text = textToAnimate[..charsToShow];
            }
            TextChanged?.Invoke(this, EventArgs.Empty);
        }
        public Animatioms()
        {
            WeakReferenceMessenger.Default.Register<AppMessage<long>>(this, (r, m) =>
            {
                OnMessageReceived(m.Value);
            });
            WeakReferenceMessenger.Default.Register<AppMessage<int>>(this, (r, m) =>
            {
                OnMessageReceived(m.Value);
            });
        }
    }
}
