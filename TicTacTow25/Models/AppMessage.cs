using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TicTacTow25.Models
{
    public class AppMessage<T>(T msg) : ValueChangedMessage<T>(msg)
    {

    }
}
