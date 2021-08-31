using PoliceHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoliceHR.ViewModels
{
    public class GetElmPersonalInfo
    {
        public string ID_TO_NAME(int IDD)
        {
            
            using (var HR = new SubjectivityContext())
            {
                string Uname = "الاسم الثلاثي : ";
                var table = HR.Elements;
                var searchresult = table.Where(c => c.ElmId.Equals(IDD));
                foreach (var ss in searchresult)
                {
                    Uname += ss.ElmName+" ";
                    Uname += ss.ElmFather + " ";
                    Uname += ss.ElmLastName + " ";
                    Uname += "الرقم العسكري :" + " ";
                    Uname += ss.ElmMNumber;

                }
                return Uname;

            }

        }
    }
}
