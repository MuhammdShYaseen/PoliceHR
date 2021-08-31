using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.ImagesViewer;
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
using System.Windows.Shapes;

namespace PoliceHR.Views.Children
{
    /// <summary>
    /// Interaction logic for ChildAddEdit.xaml
    /// </summary>
    public partial class ChildAddEdit : Window
    {
        int Child_ID2;
        int Elm_ID2;
        AddActiveRec Rec = new AddActiveRec();
        ConvertImage Conv = new ConvertImage();
        public ChildAddEdit(int Elm_ID,int Child_ID)
        {
            InitializeComponent();
            Child_ID2 = Child_ID;
            Elm_ID2 = Elm_ID;
            if (Child_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Children;
                        var searchresult = table.Where(c => c.ChildId.Equals(Child_ID));
                        foreach (var ss in searchresult)
                        {
                            txtBrithLocation.Text = ss.ChildBrithLocation;
                            txtBrithDate.SelectedDate = ss.ChildBrithDate;
                            if (ss.Gender == true) { txtGender.Text = "ذكر"; }else if (ss.Gender == false) { txtGender.Text = "انثى"; }
                            if (ss.IsAlive == true) { txtIsAlive.Text = "نعم"; } else if (ss.IsAlive == false) { txtIsAlive.Text = "لا"; }
                            txtIDNum.Text = ss.ChildPersonalIdNumber;
                            txtName.Text = ss.ChildName;
                            Elm_ID2 = (int)ss.ElmId;
                            HW_ID = (int)ss.WiveOrHusbandId;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.CivilRegistrationImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.CivilRegistrationImage);
                        }
                    }
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.WiveOrHusbands;
                        var searchresult = table.Where(c => c.WiveOrHusbandId.Equals(HW_ID));
                        foreach (var ss in searchresult)
                        {
                            txtSelectedMF.Text = ss.WiveOrHusbandName + " " + ss.WiveOrHusbandFatherName + " " + ss.WiveOrHusbandLastName;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.WiveOrHusbands;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                GrdHw.ItemsSource = searchresult.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    this.DragMove();
                }
        }
        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                // MaxButton.Content = "1";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                // MaxButton.Content = "2";
            }
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {

            CloseButton.Background = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#FF222831"));
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Background = System.Windows.Media.Brushes.Red;
        }
        BitmapImage Immg;
        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            var SS = new OpenImageFromPc();
            Immg = SS.OpenFromPC();
            ImgElementImage.Source = Immg;
        }
        private void BtnAddEditV_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            foreach (var ctl in GrdTexts.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    if (((TextBox)ctl).Text == String.Empty)
                    {
                        MessageBox.Show("هناك حقول فارغة يرجى ملئها أولا ثم اعد المحاولة", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                        this.IsEnabled = true;
                        return;
                    }
                }
            }
            if (Child_ID2 != 0)
            {
                EditChild(Child_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddChild(Elm_ID2);
                this.IsEnabled = true;
                foreach (var ctl in GrdTexts.Children)
                {
                    try
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = String.Empty;
                    }
                    catch { }

                }
            }
        }
        private void AddChild(int Element_ID)
        {
            bool Genderr = false;
            bool IsAlivee = false;
            if (txtGender.Text == "ذكر") { Genderr = true; }else if (txtGender.Text == "انثى") { Genderr = false; }
            if (txtIsAlive.Text == "نعم") { IsAlivee = true; }else if (txtIsAlive.Text == "لا") { IsAlivee = false; }
            if (HW_ID == 0)
            {
                this.IsEnabled = true;
                MessageBox.Show("قم باختيار والد ة للولد لايمكن ان يكون فارغا","خطأ",MessageBoxButton.OK,MessageBoxImage.Error); ;
                return;
            }
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Child { ElmId = Element_ID, WiveOrHusbandId = HW_ID, CivilRegistrationImage = Conv.getJPGFromImageControl(Immg), ChildBrithDate = txtBrithDate.SelectedDate, ChildBrithLocation = txtBrithLocation.Text, ChildName = txtName.Text, ChildPersonalIdNumber = txtIDNum.Text, Gender = Genderr,IsAlive=IsAlivee};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc(" اضافة الولد :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditChild(int Childd_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Children.First(a => a.ChildId == Childd_ID);
                    ss.ChildBrithLocation = txtBrithLocation.Text ;
                    ss.ChildBrithDate = txtBrithDate.SelectedDate ;
                    if (txtGender.Text == "ذكر" ) { ss.Gender = true; } else if (txtGender.Text == "انثى") {ss.Gender = false; }
                    if (txtIsAlive.Text == "نعم" ) {ss.IsAlive = true ; } else if (ss.IsAlive == false) { txtIsAlive.Text = "لا"; }
                    ss.ChildPersonalIdNumber = txtIDNum.Text ;
                    ss.ChildName = txtName.Text;
                    if (Immg != null) { ss.CivilRegistrationImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc(" تعديل الولد رقم :" + Child_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(Immg));
            ss.Show();
        }

        private void GrdHw_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (var HR = new SubjectivityContext())
            {
                var table = HR.WiveOrHusbands;
                var searchresult = table.Where(c => c.WiveOrHusbandId.Equals(HW_ID));
                foreach (var ss in searchresult)
                {
                    txtSelectedMF.Text = ss.WiveOrHusbandName+ " " + ss.WiveOrHusbandFatherName + " " + ss.WiveOrHusbandLastName;
                }
            }
        }
        int HW_ID;
        private void GrdHw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdHw.SelectedItems.Count > 0)
            {
                WiveOrHusband member = new WiveOrHusband();
                foreach (var obj in GrdHw.SelectedItems)
                {
                    member = obj as WiveOrHusband;
                    str = member.WiveOrHusbandId;
                }
            }
            else
            {

            }
            HW_ID = str;
        }
    }
}
