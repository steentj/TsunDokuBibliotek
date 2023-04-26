namespace TsundokuBibliotek.Views;

public partial class MainPage : ContentPage
{
    private BøgerViewModel vm;
    public MainPage(BøgerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        vm = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await vm.GetBøgerASync();
    }
}