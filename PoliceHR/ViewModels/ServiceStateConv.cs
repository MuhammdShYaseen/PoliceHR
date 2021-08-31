using PoliceHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PoliceHR.ViewModels
{
    class ServiceStateConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return ID_TO_NAME(int.Parse(value.ToString()));
            }
            catch
            {

                return "other";
            }
        }
        private string ID_TO_NAME(int IDD)
        {
            string Uname = "";
            using (SubjectivityContext HR = new SubjectivityContext())
            {
                var table = HR.ServiceStates;
                var searchresult = table.Where(c => c.ServiceStateId.Equals(IDD));
                foreach (ServiceState ss in searchresult)
                {
                    Uname = ss.ServiceState1;
                }
                return Uname;

            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}
