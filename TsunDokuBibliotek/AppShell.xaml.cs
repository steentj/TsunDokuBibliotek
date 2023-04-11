namespace TsundokuBibliotek;

public partial class AppShell : Shell
{
    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

    public AppShell()
    {
        InitializeComponent();

        Routes.Add(nameof(BogDetaljerView), typeof(BogDetaljerView));
        Routes.Add(nameof(TsundokuPreferenceView), typeof(TsundokuPreferenceView));
        Routes.Add(nameof(AboutView), typeof(AboutView));
        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }

        BindingContext = this;
    }
}