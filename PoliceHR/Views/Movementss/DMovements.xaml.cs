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

namespace PoliceHR.Views.Movementss
{
    /// <summary>
    /// Interaction logic for DMovements.xaml
    /// </summary>
    public partial class DMovements : Page
    {
        int Elm_ID2;
        public DMovements(int ELM_ID)
        {
            InitializeComponent();
            Elm_ID2 = ELM_ID;
            try
            {
              var HR = new SubjectivityContext();
              var table = HR.PoliceServicesMovements;
              var searchresult = table.Where(c => c.ElmId.Equals(ELM_ID));
              GrdMovement.ItemsSource = searchresult.ToList();
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
                if (ss.PoliceServicesMovements.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PoliceServicesMovement Del = ss.PoliceServicesMovements.Find(MoveID);
                        ss.PoliceServicesMovements.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.PoliceServicesMovements;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdMovement.ItemsSource = searchresult.ToList();
                    }
                }

            }
        }
        int MoveID;
        private void GrdMovement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdMovement.SelectedItems.Count > 0)
            {
                PoliceServicesMovement member = new PoliceServicesMovement();
                foreach (var obj in GrdMovement.SelectedItems)
                {
                    member = obj as PoliceServicesMovement;
                    str = member.PoliceServicesId;
                }
            }
            else
            {

            }
            MoveID = str;
        }

        private void GrdMovement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MoveID != 0)
            {
                var EdPen = new EditMovement(MoveID);
                EdPen.Show();
            }
        }

        private void BtnAddMovement_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var SS = new AddMovement(Elm_ID2);
            SS.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdMovement, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة التنقلات  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
