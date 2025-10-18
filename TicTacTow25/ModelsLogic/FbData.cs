using System.Text.RegularExpressions;
using TicTacTow25.Models;

namespace TicTacTow25.ModelsLogic
{
    partial class FbData:FbDataModel
    {
        public override async void CreateUserWithEmailAndPasswordAsync(string email, string password, string name, Action<System.Threading.Tasks.Task> OnComplete)
        {
           await facl.CreateUserWithEmailAndPasswordAsync(email, password, name).ContinueWith(OnComplete);
        }
        public override async void SignInWithEmailAndPasswordAsync(string email, string password, Action<System.Threading.Tasks.Task> OnComplete)
        {
            await facl.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(OnComplete);
        }

        public override string GetErrorMessage(string errMessage)
        {
            int start = errMessage.IndexOf(Keys.MessageKey), 
                end = errMessage.IndexOf(Keys.ErrorsKey,start);
            string title = errMessage[(start + Keys.MessageKey.Length)..end]
                .Replace(Keys.Apostrophe, string.Empty)
                .Replace(Keys.Colon,string.Empty)
                .Replace(Keys.Comma,string.Empty)
                .Trim();
            title = string.Join(Keys.WordsDelimiter, title.Split(Keys.TitleDelimiter));
            errMessage = errMessage[(errMessage.IndexOf(Keys.ReasonKey) + 
                Keys.ReasonKey.Length)..];
            errMessage = string.Join(Keys.WordsDelimiter, 
                Regex.Split(errMessage, Keys.UpperCaseDelimiter)).Trim();
            return title + Keys.NewLine + Keys.ReasonKey + 
                Keys.WordsDelimiter + errMessage[..^1];
        }
    }
}
