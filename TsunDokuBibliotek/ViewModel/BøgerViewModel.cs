namespace TsundokuBibliotek.ViewModel;

public partial class BøgerViewModel : BaseViewModel
{
    public ObservableCollection<Bog> Bøger { get; } = new();
    BogRepository repository;

    public BøgerViewModel(BogRepository repository)
    {
        Title = "Mit tsundoku bibliotek";
        this.repository = repository;
    }

    [RelayCommand]
    public async Task GetBøgerASync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var bøger = await repository.GetBøgerAsync();

            if (Bøger.Any())
                Bøger.Clear();

            foreach (var bog in bøger)
            {
                Bøger.Add(bog);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Kunne ikke indlæse bøger fra json fil: {ex.Message}");
            await Shell.Current.DisplayAlert("Fejl", $"Fejl i indlæsning af json file: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GoToDetails(Bog bog)
    {
        if (bog is null)
            return;

        await Shell.Current.GoToAsync(nameof(BogDetaljer), true, new Dictionary<string, object>
        {
            { "Bog", bog}
        });
    }

    [RelayCommand]
    async Task GoToSettings()
    {
        await Shell.Current.GoToAsync(nameof(Settings), true);
    }
}