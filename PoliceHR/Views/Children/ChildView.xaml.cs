using Microsoft.Win32;
using PoliceHR.Models;
using PoliceHR.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
namespace PoliceHR.Views.Children
{
    /// <summary>
    /// Interaction logic for ChildView.xaml
    /// </summary>
    public partial class ChildView : Page
    {
        int Elm_ID2;
        public ChildView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Children;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID));
                GrdChild.ItemsSource = searchresult.ToList();
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
            var SS = new ChildAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdChild_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Child_ID != 0 || Child_ID != 0)
            {
                var SS = new ChildAddEdit(0, Child_ID);
                SS.Show();
            }

        }
        int Child_ID;
        private void GrdChild_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdChild.SelectedItems.Count > 0)
            {
                Child member = new Child();
                foreach (var obj in GrdChild.SelectedItems)
                {
                    member = obj as Child;
                    str = member.ChildId;
                }
            }
            else
            {

            }
            Child_ID = str;
        }

        private void BtnViewHw_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var SS = new HusbandOrWives.HWAddEdit(0, int.Parse(button.DataContext.ToString()));
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
                if (ss.Children.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Child Del = ss.Children.Find(Child_ID);
                        ss.Children.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Children;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdChild.ItemsSource = searchresult.ToList();
                    }
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdChild, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة الاولاد  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
