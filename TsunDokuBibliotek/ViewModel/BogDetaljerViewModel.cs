using System;
namespace TsundokuBibliotek.ViewModel;

[QueryProperty(nameof(Bog), "Bog")]
public partial class BogDetaljerViewModel : BaseViewModel
{
    public BogDetaljerViewModel()
    {
    }

    [ObservableProperty]
    Bog bog;
}

