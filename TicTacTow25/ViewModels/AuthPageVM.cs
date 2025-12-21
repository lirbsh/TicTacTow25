using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Windows.Input;
using TicTacTow25.Models;
using TicTacTow25.ModelsLogic;

namespace TicTacTow25.ViewModels
{
    public partial class AuthPageVM : ObservableObject
    {
        private readonly User user = new();
        public ICommand AuthCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public bool IsBusy => user.IsBusy;
        public bool IsRegistered => user.IsRegistered;
        public string UserStateAction => user.IsRegistered?Strings.Login:Strings.Register;
        public string Name
        {
            get => user.Name;
            set
            {
                if (user.Name != value)
                {
                    user.Name = value;
                    (AuthCommand as Command)?.ChangeCanExecute();
                }
            }
        }
        public string Email
        {
            get => user.Email;
            set
            {
                if (user.Email != value)
                {
                    user.Email = value;
                    (AuthCommand as Command)?.ChangeCanExecute();
                }
            }
        }
        public string Password
        {
            get => user.Password;
            set
            {
                if (user.Password != value)
                {
                    user.Password = value;
                    (AuthCommand as Command)?.ChangeCanExecute();
                }
            }
        }
        public bool IsPassword { get; set; } = true;

        public AuthPageVM()
        {
            AuthCommand = user.IsRegistered? new Command(Login, CanAuth): new Command(Register, CanAuth);
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            user.AuthComplete += OnAuthComplete;
            user.AuthError += OnAuthError;
        }
        private void OnAuthError(object? sender, string errMessage)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(errMessage, ToastDuration.Long).Show();
            });
        }
        private void OnAuthComplete(object? sender, bool success)
        {
            
            if (success && Application.Current != null)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current.MainPage = new AppShell();
                });
            }
            else
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    (AuthCommand as Command)?.ChangeCanExecute();
                });
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        private bool CanAuth()
        {
            return user.IsValid() ;
        }
        private void Login()
        {
            if (!IsBusy)
            {
                user.Login();
                OnPropertyChanged(nameof(IsBusy));
                (AuthCommand as Command)?.ChangeCanExecute();
            }
        }
        private void Register()
        {
            if (!IsBusy)
            {
                user.Register();
                OnPropertyChanged(nameof(IsBusy));
                (AuthCommand as Command)?.ChangeCanExecute();
            }
        }
        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }
    }
}
