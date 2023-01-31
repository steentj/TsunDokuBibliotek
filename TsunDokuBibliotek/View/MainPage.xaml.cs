namespace TsunDokuBibliotek;

public partial class MainPage : ContentPage
{
    public MainPage(BøgerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}