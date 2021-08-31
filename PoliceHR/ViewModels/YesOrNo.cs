using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace PoliceHR.ViewModels
{
    public class YesOrNo : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch (value.ToString())
                {
                    case "True":
                        return "نعم";
                    case "False":
                        return "لا";
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
