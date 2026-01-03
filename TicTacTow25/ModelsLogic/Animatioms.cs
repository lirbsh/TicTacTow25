using CommunityToolkit.Mvvm.Messaging;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    public class Animatioms : AnimatiomsModel
    {
        public override void StartOpacityAnimation()
        {
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
        public Animatioms()
        {
            WeakReferenceMessenger.Default.Register<AppMessage<long>>(this, (r, m) =>
            {
                OnMessageReceived(m.Value);
            });
        }
    }
}
