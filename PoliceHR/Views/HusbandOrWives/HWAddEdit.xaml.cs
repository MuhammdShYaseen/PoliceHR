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

namespace PoliceHR.Views.HusbandOrWives
{
    /// <summary>
    /// Interaction logic for HWAddEdit.xaml
    /// </summary>
    public partial class HWAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int HW_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public HWAddEdit(int Elm_ID,int HW_ID)
        {
            InitializeComponent();
            HW_ID2 = HW_ID;
            Elm_ID2 = Elm_ID;
            if (HW_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.WiveOrHusbands;
                        var searchresult = table.Where(c => c.WiveOrHusbandId.Equals(HW_ID));
                        foreach (var ss in searchresult)
                        {
                            txtHwName.Text = ss.WiveOrHusbandName;
                            txtLastName.Text = ss.WiveOrHusbandLastName;
                            txtLicNum.Text = ss.MarriageLicenseNumber;
                            txtMotherName.Text = ss.WiveOrHusbandMoherName;
                            txtFatherName.Text = ss.WiveOrHusbandFatherName;
                            txtIDNum.Text = ss.WiveOrHusbandPersonalIdNumber;
                            txtJob.Text = ss.WiveOrHusbandJob;
                            txtNationality.Text = ss.WiveOrHusbandNationality;
                            txtBlockNum.Text = ss.WiveOrHusbandBlockNumber;
                            txtBrithLocation.Text = ss.WiveOrHusbandBrithLocation;
                            txtBrithDate.SelectedDate = ss.WiveOrHusbandBrithDate;
                            txtContractDate.SelectedDate = ss.MarriageContractDate;
                            txtLicensDate.SelectedDate = ss.MarriageLicenseDate;
                            txtRegPoliceDate.SelectedDate = ss.RegistrationMarriagePoliceDate;
                            if (ss.IsRelationshipExist == true) {txtIsExist.Text="نعم";} else if(ss.IsRelationshipExist == false) {txtIsExist.Text="لا";}
                            if (ss.WiveOrHusbandIsAlive == true) {txtIsAlive.Text="نعم";}else if (ss.WiveOrHusbandIsAlive == false) {txtIsAlive.Text="لا";}
                            ImgCivilImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.WiveOrHusbandCivilRegistrationImage);
                            ImgLicencImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.MarriageLicenseImage);
                            ImmgLic = Conv.ConvertByteArrayToBitMapImage(ss.MarriageLicenseImage);
                            ImmgCivil = Conv.ConvertByteArrayToBitMapImage(ss.WiveOrHusbandCivilRegistrationImage);
                            Elm_ID2 = (int)ss.ElmId;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
        BitmapImage ImmgCivil;
        private void BtnSelectCivilPhoto_Click(object sender, RoutedEventArgs e)
        {
            var SS = new OpenImageFromPc();
            ImmgCivil = SS.OpenFromPC();
            ImgCivilImage.Source = ImmgCivil;
        }
        BitmapImage ImmgLic;
        private void BtnSelectLicencPhoto_Click(object sender, RoutedEventArgs e)
        {
            var SS = new OpenImageFromPc();
            ImmgLic = SS.OpenFromPC();
            ImgLicencImage.Source = ImmgLic;
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
            if (HW_ID2 != 0)
            {
                EditHw(HW_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddHW(Elm_ID2);
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
        private void AddHW(int Element_ID)
        {
            bool Life =true ;
            bool Exist =true ;
            if (txtIsAlive.Text == "نعم") { Life = true; } else if (txtIsAlive.Text == "لا") { Life = false; }
            if (txtIsExist.Text == "نعم") { Exist = true; } else if (txtIsExist.Text == "لا") { Exist = false; }
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new WiveOrHusband { ElmId = Element_ID, WiveOrHusbandBlockNumber = txtBlockNum.Text, MarriageLicenseImage = Conv.getJPGFromImageControl(ImmgLic), WiveOrHusbandCivilRegistrationImage = Conv.getJPGFromImageControl(ImmgCivil), MarriageContractDate = txtContractDate.SelectedDate, MarriageLicenseDate = txtLicensDate.SelectedDate, MarriageLicenseNumber = txtLicNum.Text, WiveOrHusbandBrithDate = txtBrithDate.SelectedDate, RegistrationMarriagePoliceDate=txtRegPoliceDate.SelectedDate, WiveOrHusbandJob=txtJob.Text,WiveOrHusbandLastName=txtLastName.Text, WiveOrHusbandFatherName=txtFatherName.Text, WiveOrHusbandName=txtHwName.Text,WiveOrHusbandMoherName=txtMotherName.Text, WiveOrHusbandBrithLocation=txtBrithLocation.Text,WiveOrHusbandPersonalIdNumber=txtIDNum.Text, IsRelationshipExist=Exist,WiveOrHusbandIsAlive=Life, WiveOrHusbandNationality=txtNationality.Text };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة زوج ة رقم :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditHw(int HusWife_ID)
        {
            try
            {
              using (var HR = new SubjectivityContext())
              {
               var ss = HR.WiveOrHusbands.First(a => a.WiveOrHusbandId == HusWife_ID);
                   ss.WiveOrHusbandName = txtHwName.Text ;
                   ss.WiveOrHusbandLastName = txtLastName.Text ;
                   ss.MarriageLicenseNumber = txtLicNum.Text ;
                   ss.WiveOrHusbandMoherName = txtMotherName.Text ;
                   ss.WiveOrHusbandFatherName = txtFatherName.Text ;
                   ss.WiveOrHusbandPersonalIdNumber = txtIDNum.Text ;
                   ss.WiveOrHusbandJob = txtJob.Text;
                   ss.WiveOrHusbandNationality = txtNationality.Text ;
                   ss.WiveOrHusbandBlockNumber = txtBlockNum.Text ;
                   ss.WiveOrHusbandBrithLocation = txtBrithLocation.Text ;
                   ss.WiveOrHusbandBrithDate = txtBrithDate.SelectedDate ;
                   ss.MarriageContractDate = txtContractDate.SelectedDate ;
                   ss.MarriageLicenseDate = txtLicensDate.SelectedDate ;
                    
                   ss.RegistrationMarriagePoliceDate = txtRegPoliceDate.SelectedDate ;
                    if (txtIsExist.Text == "نعم" ) { ss.IsRelationshipExist = true; }else if (txtIsExist.Text=="لا"){ ss.IsRelationshipExist = false; }
                    if (txtIsAlive.Text == "نعم" ) { ss.WiveOrHusbandIsAlive = true; }else if (txtIsAlive.Text == "لا" ) { ss.WiveOrHusbandIsAlive = false; }
                    if (ImmgCivil != null) { ss.WiveOrHusbandCivilRegistrationImage = Conv.getJPGFromImageControl(ImmgCivil); }
                    if (ImmgLic != null) { ss.MarriageLicenseImage = Conv.getJPGFromImageControl(ImmgLic); }
                    HR.SaveChanges();
              }
                Rec.AddActiveRecc("تعديل زوج ة رقم :" + HusWife_ID.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnShowLicencPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(ImmgLic));
            ss.Show();
        }
        private void BtnShowCivilPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(ImmgCivil));
            ss.Show();
        }
    }
}
