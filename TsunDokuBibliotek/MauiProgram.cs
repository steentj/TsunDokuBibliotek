using CommunityToolkit.Maui.Storage;
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
        builder.Services.AddSingleton<TsundokuPreferenceView>();
        builder.Services.AddSingleton<AboutView>();
        builder.Services.AddSingleton<TsundokuPreferencesViewModel>();
        builder.Services.AddTransient<BogDetaljerViewModel>();
        builder.Services.AddTransient<BogDetaljerView>();
        builder.Services.AddSingleton<IFolderPicker>(FolderPicker.Default);
        return builder.Build();
    }
}