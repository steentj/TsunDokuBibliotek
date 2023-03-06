using Microsoft.Extensions.Logging;

namespace TsundokuBibliotek;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
#if DEBUG
        //builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<BogRepository>();
        builder.Services.AddSingleton<BøgerViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddTransient<BogDetaljerViewModel>();
        builder.Services.AddTransient<BogDetaljer>();
        return builder.Build();
    }
}