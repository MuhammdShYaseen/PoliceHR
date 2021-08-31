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
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace PoliceHR.Views
{
    /// <summary>
    /// Interaction logic for HomePg.xaml
    /// </summary>
    public partial class HomePg : Page
    {
        string AccessPath="";
        AddActiveRec Rec = new AddActiveRec();
        public HomePg()
        {
            InitializeComponent();
            lbxSearchOption.Text = "الرقم العسكري";
           /* try
            {
            var HR = new SubjectivityContext();
            GrdElmnts.ItemsSource = HR.Elements.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void MIdetils_Click(object sender, RoutedEventArgs e)
        {
            if (ElementMN == 0) { return; }
            using (SubjectivityContext ss = new SubjectivityContext())
            {
                if (ss.Elements.Count() != 0)
                {
                    DetailsElementInfo DE = new DetailsElementInfo(ElementMN);
                    DE.Show();
                }
            }
        }

        private void MIEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ElementMN == 0) { return; }
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3 || Rolee.CheckUserRolee() == 0)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (SubjectivityContext ss = new SubjectivityContext())
            {
                if (ss.Elements.Count() != 0)
                {
                    EditElement ED = new EditElement(ElementMN);
                    ED.Show();
                }
            }
        }
        int ElementMN;
        private void GrdElmnts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdElmnts.SelectedItems.Count > 0)
            {
                _ = new Element();
                foreach (var obj in GrdElmnts.SelectedItems)
                {
                    Element member = obj as Element;
                    str = member.ElmId;
                }
            }
            else
            {

            }
            ElementMN = str;
        }

        private void MIDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ElementMN == 0){return;}
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee()==3 || Rolee.CheckUserRolee()==0)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (SubjectivityContext ss = new SubjectivityContext())
            {
                if (ss.Elements.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                      Element Del = ss.Elements.Find(ElementMN);
                      ss.Elements.Remove(Del);
                      ss.SaveChanges();
                      MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                      GrdElmnts.ItemsSource = ss.Elements.ToList();
                    }
                }
                
            }
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
                var table = HR.Elements;
                switch (lbxSearchOption.Text)
                {
                    case "الاسم":
                        var searchresult = table.Where(c => c.ElmName.Contains(txtSearch.Text));
                        GrdElmnts.ItemsSource = searchresult.ToList();
                            if (searchresult.Count() == 0) { MessageBox.Show("لا يوجد نتائج لعرضها ل " + lbxSearchOption.Text+" "+ ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                        break;
                    case "الكنية":
                        var searchresult2 = table.Where(c => c.ElmLastName.Contains(txtSearch.Text));
                        GrdElmnts.ItemsSource = searchresult2.ToList();
                            if (searchresult2.Count() == 0) { MessageBox.Show("لا يوجد نتائج لعرضها ل " + lbxSearchOption.Text + "  " + ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            break;
                    case "الرقم العسكري":
                        var searchresult3 = table.Where(c => c.ElmMNumber.Contains(txtSearch.Text));
                        GrdElmnts.ItemsSource = searchresult3.ToList();
                            if (searchresult3.Count() == 0) { MessageBox.Show("لا يوجد نتائج بحث لعرضها ل " + lbxSearchOption.Text + " " + ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            break;
                    case "مكان الولادة":
                        var searchresult4 = table.Where(c => c.ElmBrithLocation.Contains(txtSearch.Text));
                        GrdElmnts.ItemsSource = searchresult4.ToList();
                            if (searchresult4.Count() == 0) { MessageBox.Show("لا يوجد نتائج بحث لعرضها ل " + lbxSearchOption.Text + " " + ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            break;
                    case "الخانة":
                        var searchresult5 = table.Where(c => c.ElmBlockNumber.Contains(txtSearch.Text));
                        GrdElmnts.ItemsSource = searchresult5.ToList();
                            if (searchresult5.Count() == 0) { MessageBox.Show("لا يوجد نتائج بحث لعرضها ل " + lbxSearchOption.Text + " " + ",," + txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            break;
                    case "الرقم الوطني":
                        var searchresult6 = table.Where(c => c.ElmPersonalIdNumber.Contains(txtSearch.Text));
                        GrdElmnts.ItemsSource = searchresult6.ToList();
                            if (searchresult6.Count() == 0) { MessageBox.Show("لا يوجد نتائج بحث لعرضها ل " + lbxSearchOption.Text + " " +",,"+ txtSearch.Text + ",,", "البحث", MessageBoxButton.OK, MessageBoxImage.Information,MessageBoxResult.None, MessageBoxOptions.RightAlign); }
                            break;
                }
                    Rec.AddActiveRecc("البحث عن   :" + txtSearch.Text +" خيار البحث " + lbxSearchOption.Text);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void MIPrintDoc_Click(object sender, RoutedEventArgs e)
        {
            PrintDoc.PrintDocView ss = new PrintDoc.PrintDocView(ElementMN);
            ss.Show();
        }

        private void MIAddToAccess_Click(object sender, RoutedEventArgs e)
        {
            if (AccessPath == "")
            {
                ExportToAccess ss = new ExportToAccess();
                AccessPath = ss.OpenDB();
            }
        }
    }
}
