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

namespace PoliceHR.Views.ClassChangeds
{
    /// <summary>
    /// Interaction logic for ClassChangedView.xaml
    /// </summary>
    public partial class ClassChangedView : Page
    {
        int Elm_ID2;
        public ClassChangedView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.ClassChanges;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID));
                GrdClassChange.ItemsSource = searchresult.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            var SS = new ClassChangedAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdClassChange_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Class_ID != 0 || Class_ID != 0)
            { 
                var SS = new ClassChangedAddEdit(0, Class_ID);
                SS.Show();
            }
            
        }
        int Class_ID;
        private void GrdClassChange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdClassChange.SelectedItems.Count > 0)
            {
                ClassChange member = new ClassChange();
                foreach (var obj in GrdClassChange.SelectedItems)
                {
                    member = obj as ClassChange;
                    str = member.ClassChangesId;
                }
            }
            else
            {

            }
            Class_ID = str;
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
                if (ss.ClassChanges.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        ClassChange Del = ss.ClassChanges.Find(Class_ID);
                        ss.ClassChanges.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.ClassChanges;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdClassChange.ItemsSource = searchresult.ToList();
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdClassChange, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة تبدلات الصنف  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
