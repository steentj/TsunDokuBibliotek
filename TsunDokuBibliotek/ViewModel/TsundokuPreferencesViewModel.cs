using System;
using CommunityToolkit.Maui.Core;
using System.Threading;
using CommunityToolkit.Maui.Storage;

namespace TsundokuBibliotek.ViewModel
{
	[QueryProperty(nameof(TsundokuSettings), "TsundokuSettings")]
	public partial class TsundokuPreferencesViewModel : BaseViewModel
	{
        readonly IFolderPicker folderPicker;

        public TsundokuPreferencesViewModel(IFolderPicker folderPicker)
		{
            this.folderPicker = folderPicker;
            TsundokuSettings = new()
            {
                TsundokuFolder = Preferences.Default.Get("tsundokufolder", FileSystem.AppDataDirectory)
            };
		}

		[ObservableProperty]
		TsundokuSettings tsundokuSettings;

        [RelayCommand]
        public async Task PickDataFolderASync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var folderPickerResultat = await folderPicker.PickAsync(TsundokuSettings.TsundokuFolder, default);
                if (folderPickerResultat.IsSuccessful)
                {
                    TsundokuSettings = new() { TsundokuFolder = folderPickerResultat.Folder.Path };
                    //TsundokuSettings.TsundokuFolder = folderPickerResultat.Folder.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Kunne ikke læse data folder: {ex.Message}");
                await Shell.Current.DisplayAlert("Fejl", $"Kunne ikke læse data folder: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

