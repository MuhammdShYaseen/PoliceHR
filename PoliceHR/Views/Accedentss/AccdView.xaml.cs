using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.Seizuress;
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

namespace PoliceHR.Views.Accedentss
{
    /// <summary>
    /// Interaction logic for AccdView.xaml
    /// </summary>
    public partial class AccdView : Page
    { 
        int Elm_ID2;
        public AccdView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Accidents;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID));
                GrdAccd.ItemsSource = searchresult.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int Acc_ID=0;
        private void GrdAccd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdAccd.SelectedItems.Count > 0)
            {
                Accident member = new Accident();
                foreach (var obj in GrdAccd.SelectedItems)
                {
                    member = obj as Accident;
                    str = member.AccidentsId;
                }
            }
            else
            {

            }
            Acc_ID = str;
        }

        private void GrdAccd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Acc_ID != 0)
            { 
                var SS = new AccdAddEdit(0, Acc_ID);
                    SS.Show();

            }
               
        }

        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var SS = new AccdAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void BtnViewSeizure_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var SS = new SeizureAddEdit(0, int.Parse(button.DataContext.ToString()));
            SS.Show();
        }
        private void MIDelete_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (SubjectivityContext ss = new SubjectivityContext())
            {
                if (ss.Accidents.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Accident Del = ss.Accidents.Find(Acc_ID);
                        ss.Accidents.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Accidents;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdAccd.ItemsSource = searchresult.ToList();
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdAccd, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة الحوادث  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }

    }
}
