using System;
using System.Collections.Generic;
using System.Text;

namespace PoliceHR.ViewModels
{
    public class staticDataSource
    {
        public List<string> SearchColmn = new List<string>();
        public List<string> SearchActivity = new List<string>();
        public staticDataSource()
        {
            SearchColmn.Add("الاسم");
            SearchColmn.Add("الكنية");
            SearchColmn.Add("الرقم العسكري");
            SearchColmn.Add("تاريخ الميلاد");
            SearchColmn.Add("مكان الولادة");
            SearchColmn.Add("الخانة");
            SearchColmn.Add("مكان الولادة");
            SearchColmn.Add("الرقم الوطني");

            SearchActivity.Add("اسم المستخدم");
            SearchActivity.Add("تاريخ النشاط");
            SearchActivity.Add("تفاصيل النشاط");
        }
         

    }
}
