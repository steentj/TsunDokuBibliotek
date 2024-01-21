namespace TsundokuBibliotek.ViewModel;

[QueryProperty("Id", "Id")]
public partial class BogDetaljerViewModel : BaseViewModel
{
    BogRepository repository;
    ImageRepository imageRepository;
    public BogDetaljerViewModel(BogRepository repository, ImageRepository imageRepository)
    {
        this.repository = repository;
        this.imageRepository = imageRepository;
    }

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private Bog vistBog;

    [ObservableProperty]
    private Bog editBog;

    [ObservableProperty]
    private bool isValid;

    [ObservableProperty]
    private string bookImageLink;

    [ObservableProperty]
    private int bookImageOpacity;

    [RelayCommand]
    private async Task Initialize()
    {
        EditBog = new Bog();
        if (Id > 0)
        {
            VistBog = await repository.GetBogAsync(Id);
            BookImageLink = VistBog.AbsolutBilledeLink;
            BookImageOpacity = 70;
            CopyBookDetailsToEditBook();
        }
        else
        {
            IsEdit = true;
            BookImageLink = EditBog.AbsolutBilledeLink;
            BookImageOpacity = 30;
            EditBog.BilledeLink = Constants.DefaultBookImage;
        }
    }

    private void CopyBookDetailsToEditBook()
    {
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

    [RelayCommand]
    public void EditBook()
    {
        IsEdit = true;
    }

    [RelayCommand]
    public async void EditBookImage()
    {
        IsBusy = true;

        var image = await ImageRepository.RetrieveImage();
        image = ResizeImage(image);
        var imagePath = await ImageRepository.SaveImage(image, Guid.NewGuid().ToString());
        if (imagePath is not null)
        {
            EditBog.BilledeLink = imagePath;
            BookImageOpacity = 30;
            BookImageLink = EditBog.AbsolutBilledeLink;
        }

        IsBusy = false;
    }

    private static Microsoft.Maui.Graphics.IImage ResizeImage(Microsoft.Maui.Graphics.IImage image) => image?.Downsize(150, true);

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
            {
                IsEdit = false;
                await Shell.Current.Navigation.PopAsync();
            }
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
                IsEdit = false;
                await Shell.Current.Navigation.PopAsync();
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async void CancelEdit()
    {
        IsEdit = false;
        if (EditBog.Id > 0)
        {
            CopyBookDetailsToEditBook();
        }
        else
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }

    [RelayCommand]
    public void Validate()
    {            
        IsValid = !IsEdit || !string.IsNullOrEmpty(EditBog?.Titel.Trim());
    }
}
