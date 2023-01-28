using System.Globalization;

namespace TsundokuLibrary.ViewModel;

public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Status && value is not null)
        {
            var status = (Status)value;
            var color = Colors.DarkGray;
            switch (status)
            {
                case Status.Købt:
                    color = Colors.Red;
                    break;
                case Status.Igang:
                    color = Colors.Yellow;
                    break;
                case Status.Læst:
                    color = Colors.Green;
                    break;
            }
            return color;
        }
        return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
