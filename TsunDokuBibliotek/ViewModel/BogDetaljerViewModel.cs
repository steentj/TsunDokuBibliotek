using TsundokuBibliotek.Model;

namespace TsundokuBibliotek.ViewModel;

[QueryProperty("Id", "Id")]
public partial class BogDetaljerViewModel : BaseViewModel
{
    BogRepository repository;
    public BogDetaljerViewModel(BogRepository repository)
    {
        this.repository = repository;
    }

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    Bog vistBog;

    [ObservableProperty]
    Bog editBog;

    public bool Saveable => ValidateTitel() && IsEdit;
    public string[] Stati => Enum.GetNames(typeof(Status));
    public string[] Formats => Enum.GetNames(typeof(Format));

    [RelayCommand]
    private async Task Initialize()
    {
        VistBog = await repository.GetBogAsync(Id);
        if (Id != -1)
        {
            EditBog = new Bog();
            EditBog.Id = VistBog.Id;
            EditBog.Titel = VistBog.Titel;
            EditBog.Forfatter = VistBog.Forfatter;
            EditBog.Format = VistBog.Format;
            EditBog.Status = VistBog.Status;
            EditBog.Synopsis = VistBog.Synopsis;
            EditBog.Hvorfor = VistBog.Hvorfor;
            EditBog.BilledeLink = VistBog.BilledeLink;
            OnPropertyChanged(nameof(EditBog));
        }
    }

    [RelayCommand]
    public void EditBook()
    {
        IsEdit = true;
    }

    [RelayCommand]
    private async Task SaveBook()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var op = await repository.SaveBookAsync(EditBog);

            if (op)
                await Shell.Current.Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task DeleteBook()
    {
        IsBusy = true;
        var confirm = await Shell.Current.DisplayAlert("Bekræft", "Er du sikker på du vil slette denne bog?", "Ja", "Nej");

        try
        {
            if (confirm)
            {
                await repository.DeleteBookAsync(VistBog);
                await Shell.Current.Navigation.PopAsync();
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public void CancelEdit()
    {
        IsEdit = false;
    }

    public bool ValidateTitel() => !string.IsNullOrEmpty(EditBog?.Titel.Trim());
}
