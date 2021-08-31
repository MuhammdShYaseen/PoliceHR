using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace PoliceHR.ViewModels
{
    public class AllType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch (value.ToString())
                {
                    case "True":
                        return "مجند";
                    case "False":
                        return "متطوع";
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
