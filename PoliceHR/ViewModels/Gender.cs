using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace PoliceHR.ViewModels
{
   public class Gender : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch (value.ToString())
                {
                    case "True":
                        return "ذكر";
                    case "False":
                        return "انثى";
                    case "":
                        return "غير محدد";
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
