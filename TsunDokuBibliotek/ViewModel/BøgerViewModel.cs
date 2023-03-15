namespace TsundokuBibliotek.ViewModel;

public partial class BøgerViewModel : BaseViewModel
{
    BogRepository repository;
    ObservableCollection<Bog> bøger = new();
    public  ObservableCollection<Bog> Bøger
    {
        get
            {
            if (bøger is null || !bøger.Any())
            {
                var bøger = GetBøgerASync();
            }

            return bøger;
        }

    }

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
            var bøgerTemp = await repository.GetBøgerAsync();

            if (bøger is null || bøger.Any())
                bøger.Clear();

            foreach (var bog in bøgerTemp)
            {
                bøger.Add(bog);
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
}