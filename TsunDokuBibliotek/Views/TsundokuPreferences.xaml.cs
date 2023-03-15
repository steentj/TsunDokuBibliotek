namespace TsundokuBibliotek.Views;

public partial class TsundokuPreferences : ContentPage
{
    public TsundokuPreferences(TsundokuPreferencesViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}
