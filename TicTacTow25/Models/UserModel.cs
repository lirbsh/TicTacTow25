using TicTacTow25.ModelsLogic;

namespace TicTacTow25.Models
{
    internal abstract class UserModel
    {
        protected FbData fbd = new();
        public bool IsRegistered => !string.IsNullOrWhiteSpace(Name);
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public abstract void Register();
        public abstract bool IsValid();
    }
}
