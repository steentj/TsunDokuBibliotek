namespace TsundokuBibliotek.Views;

public partial class BogDetaljerView : ContentPage
{
    BogDetaljerViewModel vm;

    public BogDetaljerView(BogDetaljerViewModel viewModel)
	{
		InitializeComponent();
        vm = viewModel;
		BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await vm.InitializeCommand.ExecuteAsync(null);
    }
}
