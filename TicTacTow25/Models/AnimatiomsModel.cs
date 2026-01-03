namespace TicTacTow25.Models
{
    public abstract class AnimatiomsModel
    {
        protected bool incrementOpacity = true;
        protected TimerSettings timerSettings = new(Keys.TotalAnimationTime, Keys.AnimationTimeInterval);
        public EventHandler? OpacityChanged;
        public double Opacity { get; protected set; }
        public bool IsLooping { get; set; }
        public abstract void StartOpacityAnimation();
    }
}
