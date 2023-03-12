namespace TsundokuBibliotek;

public partial class AppShell : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

    public AppShell()
    {
        InitializeComponent();

        Routes.Add(nameof(BogDetaljer), typeof(BogDetaljer));
        Routes.Add(nameof(Settings), typeof(Settings));
        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }

        BindingContext = this;
    }
}