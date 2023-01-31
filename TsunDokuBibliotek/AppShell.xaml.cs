namespace TsundokuBibliotek;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(BogDetaljer), typeof(BogDetaljer));
    }
}

