using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.Paintes;
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

namespace PoliceHR.Views
{
    /// <summary>
    /// Interaction logic for Palantes.xaml
    /// </summary>
    public partial class Palantes : Page
    {
        int Elm_ID2;
        public Palantes(int ELM_ID)
        {
            InitializeComponent();
            try
            {
              Elm_ID2 = ELM_ID;
              var HR = new SubjectivityContext();
              var table = HR.Penalties;
              var searchresult = table.Where(c => c.ElmId.Equals(ELM_ID));
              GrdPlts.ItemsSource = searchresult.ToList();
            }catch (Exception ex)
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
                if (ss.Penalties.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Penalty Del = ss.Penalties.Find(PinID);
                        ss.Penalties.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Penalties;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdPlts.ItemsSource = searchresult.ToList();
                    }
                }

            }
        }
        private void BtnAddBlts_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 )
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var Plts = new AddPaltss(Elm_ID2);
            Plts.Show();
        }

        private void GrdPlts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (PinID != 0)
            {
                var EdPen = new EditPen(PinID);
                EdPen.Show();
            }
        }
        int PinID;
        private void GrdPlts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdPlts.SelectedItems.Count > 0)
            {
                Penalty member = new Penalty();
                foreach (var obj in GrdPlts.SelectedItems)
                {
                    member = obj as Penalty;
                    str = member.PenaltiesId;
                }
            }
            else
            {

            }
            PinID = str;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdPlts, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة العقوبات  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
