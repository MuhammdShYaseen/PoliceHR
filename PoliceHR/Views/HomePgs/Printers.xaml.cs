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

namespace PoliceHR.Views
{
    /// <summary>
    /// Interaction logic for Printers.xaml
    /// </summary>
    public partial class Printers : Page
    {
        public Printers()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    CheckUserRole Rolee = new CheckUserRole();
                    if (Rolee.CheckUserRolee() == 0)
                    {
                        MessageBox.Show("يرجى تسجيل الدخول اولا", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var HR = new SubjectivityContext();
                    var table = HR.Docs;
                    switch (lbxSearchOption.Text)
                    {
                        case "اسم الوثيقة":
                            var searchresult = table.Where(c => c.DocName.Contains(txtSearch.Text));
                            GrdDoc.ItemsSource = searchresult.ToList();
                            if (searchresult.Count() == 0) { MessageBox.Show("لا يوجد نتائج لعرضها ل " + lbxSearchOption.Text + " " + ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            break;
                        case "تفاصيل الوثيقة":
                            var searchresult2 = table.Where(c => c.DocInfo.Contains(txtSearch.Text));
                            GrdDoc.ItemsSource = searchresult2.ToList();
                            if (searchresult2.Count() == 0) { MessageBox.Show("لا يوجد نتائج لعرضها ل " + lbxSearchOption.Text + "  " + ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            break;
                    }
                    var Rec = new AddActiveRec();
                    Rec.AddActiveRecc("البحث عن   :" + txtSearch.Text + " خيار البحث " + lbxSearchOption.Text);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void MIDelete_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3 || Rolee.CheckUserRolee() == 0)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (SubjectivityContext ss = new SubjectivityContext())
            {
                if (ss.Docs.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "الحذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Doc Del = ss.Docs.Find(Doc_ID);
                        ss.Docs.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Docs;
                        var searchresult = table.Where(c => c.DocId.Equals(Doc_ID));
                        GrdDoc.ItemsSource = searchresult.ToList();
                    }
                }

            }
        }
        private void GrdDoc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3 || Rolee.CheckUserRolee() == 0)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Doc_ID != 0)
            {
                var SS = new GeneralDocs.GenDocAddEdit(Doc_ID);
                SS.Show();
            }
        }
        int Doc_ID;
        private void GrdDoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdDoc.SelectedItems.Count > 0)
            {
                _ = new Doc();
                foreach (var obj in GrdDoc.SelectedItems)
                {
                    Doc member = obj as Doc;
                    str = member.DocId;
                }
            }
            else
            {

            }
            Doc_ID = str;
        }
    }
}
