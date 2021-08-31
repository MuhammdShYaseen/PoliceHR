using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace PoliceHR.ViewModels
{
   public class PenType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch (value.ToString())
                {
                    case "0":
                        return "حسم من الراتب";
                    case "1":
                        return "توقيف بسيط";
                    case "2":
                        return "توقيف شديد";
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
