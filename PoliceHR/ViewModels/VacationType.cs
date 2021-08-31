using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace PoliceHR.ViewModels
{
   public class VacationType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch (value.ToString())
                {
                    case "0":
                        return "اجازة ادارية";
                    case "1":
                        return "استراحة خدمة";
                    case "2":
                        return "اجازة زواج";
                    case "3":
                        return "اجازة وفاة";
                    case "4":
                        return "اجازة امومة";
                    case "5":
                        return "اجازة حج";
                    case "6":
                        return "سفر خارجي";
                    default:
                        return "other";
                }
            }
            catch
            {

                return "other";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
