namespace TsundokuBibliotek.ViewModel;

[QueryProperty("Id", "Id")]
public partial class BogDetaljerViewModel : BaseViewModel
{
    BogRepository repository;
    public BogDetaljerViewModel(BogRepository repository)
    {
        this.repository = repository;
    }

    [RelayCommand]
    private async Task Initialize()
    {
        VistBog = await repository.GetBogAsync(Id);
    }


    [ObservableProperty]
    private int id;

    //public int Id
    //{
    //    get => Id;
    //    set
    //    {
    //        Id = value;
    //        Bog = repository.GetBogAsync(Id).Result;
    //    }
    //}

    [ObservableProperty]
    Bog vistBog;
}
