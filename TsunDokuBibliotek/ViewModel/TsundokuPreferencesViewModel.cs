namespace TsundokuBibliotek.ViewModel;

public partial class TsundokuPreferencesViewModel : BaseViewModel
{
	//readonly IFolderPicker folderPicker;

	//public TsundokuPreferencesViewModel(IFolderPicker folderPicker)
	//{
	//	this.folderPicker = folderPicker;
	//	TsundokuSettings = new()
	//	{
	//		TsundokuFolder = Preferences.Default.Get("tsundokufolder", FileSystem.AppDataDirectory)
	//	};
	//}

	[ObservableProperty]
	TsundokuPreferences tsundokuSettings;

	[RelayCommand]
	public async Task PickDataFolderASync()
	{
	//    if (IsBusy)
	//        return;

	//    try
	//    {
	//        IsBusy = true;

	//        var folderPickerResultat = await folderPicker.PickAsync(TsundokuSettings.TsundokuFolder, default);
	//        if (folderPickerResultat.IsSuccessful &&
	//            folderPickerResultat.Folder.Path != TsundokuSettings.TsundokuFolder)
	//        {
	//            TsundokuSettings = new() { TsundokuFolder = folderPickerResultat.Folder.Path };
	//            Preferences.Default.Set("tsundokufolder", folderPickerResultat.Folder.Path);
	//            //TODO Her skal der nok gøres noget ved repository, så db flyttes, hvis den findes
	//        }
	//    }
	//    catch (Exception ex)
	//    {
	//        Debug.WriteLine($"Kunne ikke læse data folder: {ex.Message}");
	//        await Shell.Current.DisplayAlert("Fejl", $"Kunne ikke læse data folder: {ex.Message}", "OK");
	//    }
	//    finally
	//    {
	//        IsBusy = false;
	//    }
	}
}