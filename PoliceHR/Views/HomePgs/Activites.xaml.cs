using Microsoft.EntityFrameworkCore;
using PoliceHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PoliceHR.Views
{
    /// <summary>
    /// Interaction logic for Activites.xaml
    /// </summary>
    public partial class Activites : Page
    {
        public Activites()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    var HR = new SubjectivityContext();
                    var table = HR.ActivtisRecourds;
                    switch (lbxSearchOption.Text)
                    {
                        case "اسم المستخدم":
                            var searchresult = table.Where(c => c.User.UserName.Contains(txtSearch.Text));
                            GrdActive.ItemsSource = searchresult.ToList();
                            if (searchresult.Count() == 0) { MessageBox.Show("لا يوجد نتائج لعرضها ل " + lbxSearchOption.Text + " " + ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            break;
                        case "تفاصيل النشاط":
                            var searchresult2 = table.Where(c => c.ActivityDes.Contains(txtSearch.Text));
                            GrdActive.ItemsSource = searchresult2.ToList();
                            if (searchresult2.Count() == 0) { MessageBox.Show("لا يوجد نتائج لعرضها ل " + lbxSearchOption.Text + "  " + ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            break;
                        case "تاريخ النشاط":
                           using(SubjectivityContext HRR =new SubjectivityContext())
                            {
                                var date = DateTime.Parse(txtSearch.Text);
                                var searchresult3 =  HRR.ActivtisRecourds.Where(c => c.ActivityDateTime.Value.Date == date).ToList();
                                GrdActive.ItemsSource = searchresult3;
                                if (searchresult3.Count() == 0) { MessageBox.Show("لا يوجد نتائج بحث لعرضها ل " + lbxSearchOption.Text + " " + ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            }
                            
                            break; 
                    }
                    var Rec = new ViewModels.AddActiveRec();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
