using System;
namespace TsundokuBibliotek.ViewModel
{
	[QueryProperty(nameof(TsundokuSettings), "TsundokuSettings")]
	public partial class SettingsViewModel : BaseViewModel
	{
		public SettingsViewModel()
		{
		}

		[ObservableProperty]
		TsundokuSettings tsundokuSettings;
	}
}

