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

namespace PoliceHR.Views.DeficitRatioss
{
    /// <summary>
    /// Interaction logic for DeficitRatioView.xaml
    /// </summary>
    public partial class DeficitRatioView : Page
    {
        int Elm_ID2;
        public DeficitRatioView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.DeficitRations;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID));
                GrdRetio.ItemsSource = searchresult.ToList();
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
                if (ss.DeficitRations.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        DeficitRation Del = ss.DeficitRations.Find(Rotio_ID);
                        ss.DeficitRations.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.DeficitRations;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdRetio.ItemsSource = searchresult.ToList();
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
            var SS = new DeficitRatioAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdRetio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Rotio_ID != 0 || Rotio_ID != 0)
            {
                var SS = new DeficitRatioAddEdit(0, Rotio_ID);
                SS.Show();
            }
        }
        int Rotio_ID;
        private void GrdRetio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdRetio.SelectedItems.Count > 0)
            {
                DeficitRation member = new DeficitRation();
                foreach (var obj in GrdRetio.SelectedItems)
                {
                    member = obj as DeficitRation;
                    str = member.DeficitRatio;
                }
            }
            else
            {

            }
            Rotio_ID = str;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdRetio, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة نسبة العجز  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
