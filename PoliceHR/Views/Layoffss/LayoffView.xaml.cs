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

namespace PoliceHR.Views.Layoffss
{
    /// <summary>
    /// Interaction logic for LayoffView.xaml
    /// </summary>
    public partial class LayoffView : Page
    {
        int Elm_ID2;
        public LayoffView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Layoffs;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID));
                GrdLay.ItemsSource = searchresult.ToList();
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
                if (ss.Layoffs.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Layoff Del = ss.Layoffs.Find(Layoff_ID);
                        ss.Layoffs.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Layoffs;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdLay.ItemsSource = searchresult.ToList();
                    }
                }

            }
        }
        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 )
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var SS = new LayoffAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdLay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Layoff_ID != 0)
            {
                var SS = new LayoffAddEdit(0, Layoff_ID);
                SS.Show();
            }
        }
        int Layoff_ID;
        private void GrdLay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdLay.SelectedItems.Count > 0)
            {
                Layoff member = new Layoff();
                foreach (var obj in GrdLay.SelectedItems)
                {
                    member = obj as Layoff;
                    str = member.LayoffsId;
                }
            }
            else
            {

            }
            Layoff_ID = str;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdLay, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة تسريح  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
