namespace TsundokuBibliotek;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(BogDetaljer), typeof(BogDetaljer));
        Routing.RegisterRoute(nameof(Settings), typeof(Settings));
    }
}