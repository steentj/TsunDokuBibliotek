namespace TsundokuBibliotek.ViewModel;

public partial class BaseViewModel : ObservableObject
{
   [ObservableProperty]
   [NotifyPropertyChangedFor(nameof(IsNotBusy))]
   bool isBusy;

   public bool IsNotBusy => !IsBusy;

   [ObservableProperty]
   [NotifyPropertyChangedFor(nameof(IsNotEdit))]
   bool isEdit;

   public bool IsNotEdit => !IsEdit;

   [ObservableProperty]
   string title;
}
