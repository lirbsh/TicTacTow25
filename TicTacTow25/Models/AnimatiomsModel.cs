namespace TicTacTow25.Models
{
    public abstract class AnimatiomsModel
    {
        protected bool incrementOpacity = true;
        protected string textToAnimate = string.Empty;
        protected TimerSettings timerSettings = new(Keys.TotalAnimationTime, Keys.AnimationTimeInterval);
        public EventHandler? OpacityChanged;
        public EventHandler? TextChanged;
        public enum AnimationTypes { Opacity, Text }
        public double Opacity { get; protected set; }
        public string Text { get; protected set; } = string.Empty;
        public bool IsLooping { get; set; }
        public abstract void StartOpacityAnimation();
        public abstract void StartTextAnimation(string text);
    }
}
