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

namespace PoliceHR.Views.Decorationss
{
    /// <summary>
    /// Interaction logic for DecorationView.xaml
    /// </summary>
    public partial class DecorationView : Page
    {
        int Elm_ID2;
        public DecorationView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Decorations;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID));
                GrdDec.ItemsSource = searchresult.ToList();
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
                if (ss.Decorations.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Decoration Del = ss.Decorations.Find(DecID);
                        ss.Decorations.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Decorations;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdDec.ItemsSource = searchresult.ToList();
                    }
                }

            }
        }

        private void GrdDec_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DecID != 0 || DecID != 0)
            {
                var SS = new EditDecoration(DecID);
                SS.Show();
            }
        }
        int DecID;
        private void GrdDec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdDec.SelectedItems.Count > 0)
            {
                Decoration member = new Decoration();
                foreach (var obj in GrdDec.SelectedItems)
                {
                    member = obj as Decoration;
                    str = member.DecorationId;
                }
            }
            else
            {

            }
            DecID = str;
        }

        private void BtnAddDec_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var SS = new AddDecoration(Elm_ID2);
            SS.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdDec, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة الاوسمة و المكافات  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
    }
}
