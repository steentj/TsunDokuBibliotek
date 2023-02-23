namespace TsundokuBibliotek.View;

public partial class MainPage : ContentPage
{
    public MainPage(BøgerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        viewModel.GetBøgerASync();
    }
}