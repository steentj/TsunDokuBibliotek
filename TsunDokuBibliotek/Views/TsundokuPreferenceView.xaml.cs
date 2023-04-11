namespace TsundokuBibliotek.Views;

public partial class TsundokuPreferenceView : ContentPage
{
    public TsundokuPreferenceView(TsundokuPreferencesViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}
