namespace TsundokuBibliotek.View;

public partial class BogDetaljer : ContentPage
{
	public BogDetaljer(BogDetaljerViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}
