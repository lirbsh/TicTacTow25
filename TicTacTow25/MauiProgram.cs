using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TicTacTow25.Interfaces;
using TicTacTow25.ModelsLogic;
namespace TicTacTow25
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialSymbolsOutlined.ttf", "MaterialSymbols");
                });
            builder.Services.AddSingleton<IFbData, FbData>();
            builder.Services.AddSingleton<IUser, User>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
