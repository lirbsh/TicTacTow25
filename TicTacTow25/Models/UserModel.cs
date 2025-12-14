using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    public abstract class UserModel
    {
        protected FbData fbd = new();
        protected enum Actions { Register, Login }
        protected Actions CurrentAction = Actions.Login;
        protected abstract void OnComplete(Task task);
        protected abstract void ShowAlert(string errMessage);
        protected abstract void SaveToPreferences();

        public EventHandler<bool>? OnAuthComplete;
        public bool IsRegistered => !string.IsNullOrWhiteSpace(Name);
        public bool IsBusy { get;protected set; } = false;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public abstract void Register();
        public abstract void Login();
        public abstract bool IsValid();
    }
}