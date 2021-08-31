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

namespace PoliceHR.Views.Degreess
{
    /// <summary>
    /// Interaction logic for DegreeView.xaml
    /// </summary>
    public partial class DegreeView : Page
    {
        int Elm_ID2;
        public DegreeView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Dgrees;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID));
                GrdDegree.ItemsSource = searchresult.ToList();
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
                if (ss.Dgrees.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Dgree Del = ss.Dgrees.Find(Dgree_ID);
                        ss.Dgrees.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Dgrees;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdDegree.ItemsSource = searchresult.ToList();
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
            var SS = new DegreeAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdDegree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Dgree_ID !=0)
            {
                var SS = new DegreeAddEdit(0, Dgree_ID);
                SS.Show();
            }
        }
        int Dgree_ID;
        private void GrdDegree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdDegree.SelectedItems.Count > 0)
            {
                Dgree member = new Dgree();
                foreach (var obj in GrdDegree.SelectedItems)
                {
                    member = obj as Dgree;
                    str = member.DgreeId;
                }
            }
            else
            {

            }
            Dgree_ID = str;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdDegree, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة الشهادات  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }

}
