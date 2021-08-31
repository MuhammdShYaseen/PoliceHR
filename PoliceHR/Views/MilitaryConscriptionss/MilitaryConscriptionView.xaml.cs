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

namespace PoliceHR.Views.MilitaryConscriptionss
{
    /// <summary>
    /// Interaction logic for MilitaryConscriptionView.xaml
    /// </summary>
    public partial class MilitaryConscriptionView : Page
    {
        int Elm_ID2;
        public MilitaryConscriptionView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.MilitaryConscriptions;
                var searchresult = table.Where(c => c.ElementId.Equals(Elm_ID));
                GrdMilittary.ItemsSource = searchresult.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (GrdMilittary.Items.Count == 0)
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
                if (ss.MilitaryConscriptions.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        MilitaryConscription Del = ss.MilitaryConscriptions.Find(Military_ID);
                        ss.MilitaryConscriptions.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.MilitaryConscriptions;
                        var searchresult = table.Where(c => c.ElementId.Equals(Elm_ID2));
                        GrdMilittary.ItemsSource = searchresult.ToList();
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
            var SS = new MilitaryConscriptionAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdMilittary_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Military_ID != 0)
            {
                var SS = new MilitaryConscriptionAddEdit(0, Military_ID);
                SS.Show();
            }
        }
        int Military_ID;
        private void GrdMilittary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdMilittary.SelectedItems.Count > 0)
            {
                MilitaryConscription member = new MilitaryConscription();
                foreach (var obj in GrdMilittary.SelectedItems)
                {
                    member = obj as MilitaryConscription;
                    str = member.MilitaryConscriptionId;
                }
            }
            else
            {

            }
            Military_ID = str;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdMilittary, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة الخدمة الالزامية  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
