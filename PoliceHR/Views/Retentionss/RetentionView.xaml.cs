using PoliceHR.Models;
using PoliceHR.ViewModels;
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

namespace PoliceHR.Views.Retentionss
{
    /// <summary>
    /// Interaction logic for RetentionView.xaml
    /// </summary>
    public partial class RetentionView : Page
    {
        int Elm_ID2;
        public RetentionView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Retentions;
                var searchresult = table.Where(c => c.ElementId.Equals(Elm_ID));
                GrdReserve.ItemsSource = searchresult.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                if (ss.Retentions.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Retention Del = ss.Retentions.Find(Reserve_ID);
                        ss.Retentions.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Retentions;
                        var searchresult = table.Where(c => c.ElementId.Equals(Elm_ID2));
                        GrdReserve.ItemsSource = searchresult.ToList();
                    }
                }

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
            var SS = new RetentionAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdReserve_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Reserve_ID != 0)
            {
                var SS = new RetentionAddEdit(0, Reserve_ID);
                SS.Show();
            }
        }
        int Reserve_ID;
        private void GrdReserve_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdReserve.SelectedItems.Count > 0)
            {
                Retention member = new Retention();
                foreach (var obj in GrdReserve.SelectedItems)
                {
                    member = obj as Retention;
                    str = member.RetentionId;
                }
            }
            else
            {

            }
            Reserve_ID = str;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdReserve, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة الاحتياط  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
