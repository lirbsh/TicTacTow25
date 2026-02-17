namespace TicTacTow25.Interfaces
{
    internal interface IUser
    {
        public event EventHandler<bool>? AuthComplete;
        public event EventHandler<string>? AuthError;
        public bool IsBusy { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Prompt { get; }
        public void Register();
        public void Login();
        public bool IsValid();
    }
}
