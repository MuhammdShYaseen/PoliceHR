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

namespace PoliceHR.Views.Martyrss
{
    /// <summary>
    /// Interaction logic for MartyrView.xaml
    /// </summary>
    public partial class MartyrView : Page
    {
        int Elm_ID2;
        public MartyrView(int Elm_ID)
        {
            InitializeComponent(); 
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Martyrs;
                var searchresult = table.Where(c => c.ElementId.Equals(Elm_ID));
                GrdMartyr.ItemsSource = searchresult.ToList();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (GrdMartyr.Items.Count == 0)
            {
                BtnAddEdit.IsEnabled = true;
            }
            else
            {
                BtnAddEdit.IsEnabled = false;
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
                if (ss.Martyrs.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Martyr Del = ss.Martyrs.Find(Mart_ID);
                        ss.Martyrs.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Martyrs;
                        var searchresult = table.Where(c => c.ElementId.Equals(Elm_ID2));
                        GrdMartyr.ItemsSource = searchresult.ToList();
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
            var SS = new MartyrAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdMartyr_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Mart_ID != 0)
            {
                var SS = new MartyrAddEdit(0, Mart_ID);
                SS.Show();
            }
        }
        int Mart_ID;
        private void GrdMartyr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdMartyr.SelectedItems.Count > 0)
            {
                Martyr member = new Martyr();
                foreach (var obj in GrdMartyr.SelectedItems)
                {
                    member = obj as Martyr;
                    str = member.MartyrId;
                }
            }
            else
            {

            }
            Mart_ID = str;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdMartyr, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة واقعة الاستشهاد  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
