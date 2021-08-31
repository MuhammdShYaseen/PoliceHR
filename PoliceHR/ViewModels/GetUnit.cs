﻿using PoliceHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PoliceHR.ViewModels
{
    public class GetUnit : IValueConverter
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
                var table = HR.Unites;
                var searchresult = table.Where(c => c.UnitId.Equals(IDD));
                foreach (var ss in searchresult)
                {
                    Uname = ss.UnitName;
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
