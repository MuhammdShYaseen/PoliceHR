using Microsoft.Win32;
using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.ImagesViewer;
using PoliceHR.Views.Units;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PoliceHR.Views
{
    /// <summary>
    /// Interaction logic for EditElement.xaml
    /// </summary>
    public partial class EditElement : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int ELM_ID2;
        int ServiceSS;
        int RankDD;
        public EditElement(int ELM_ID)
        {
            InitializeComponent();
            ELM_ID2 = ELM_ID;


            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    List<Models.Rank> Rs = new List<Models.Rank>(HR.Ranks);
                    txtRank.ItemsSource = Rs;
                    txtRank.DisplayMemberPath = "RankName";
                    txtRank.SelectedValuePath = "RankId";
                    List<ServiceState> Rs1 = new List<ServiceState>(HR.ServiceStates);
                    txtServicState.ItemsSource = Rs1;
                    txtServicState.DisplayMemberPath = "ServiceState1";
                    txtServicState.SelectedValuePath = "ServiceStateId";
                    List<Unite> Un = new List<Unite>(HR.Unites);
                    txtunits.ItemsSource = Un;
                    txtunits.DisplayMemberPath = "UnitName";
                    txtunits.SelectedValuePath = "UnitId";
                    List<City> CY = new List<City>(HR.Cities);
                    txtCity.ItemsSource = CY;
                    txtCity.DisplayMemberPath = "Cities";
                    txtCity.SelectedValuePath = "CityId";
                }
            
            using (SubjectivityContext HR = new SubjectivityContext())
            {
                    var table = HR.Elements;
                    var searchresult = table.Where(c => c.ElmId.Equals(ELM_ID2));
                foreach(var ss in searchresult)
                    {
                        txtName.Text = ss.ElmName;
                        txtLastN.Text = ss.ElmLastName;
                        txtFatherN.Text = ss.ElmFather;
                        txtMother.Text = ss.ElmMother;
                        txtLocation.Text = ss.ElmBrithLocation;
                        txtBrith.SelectedDate = ss.ElmBrithDate;
                        txtBlock.Text = ss.ElmBlockNumber;
                        txtIDNumber.Text = ss.ElmPersonalIdNumber;
                        txtIdType.Text = ss.ElmIdType == true ? "جديدة" : "قديمة";
                        txtGender.Text = ss.ElmGendar == true ? "ذكر" : "انثى";
                        txtIsRes.Text = ss.ElmIsRecruiter == true ? "مجند" : "متطوع";
                        if (ss.ElmFamilyState == 0) { txtFamilyState.Text = "عازب"; } else if (ss.ElmFamilyState==1) { txtFamilyState.Text = "متزوج"; } else { txtFamilyState.Text = "مطلق"; }
                        RankDD = (int)ss.ElmRank;
                        txtCity.SelectedValue = (int)ss.CityId;
                        txtunits.SelectedValue = (int)ss.UnitId;
                        txtResNoteN.Text = ss.ElmNoteMNumber.ToString();
                        txtRdev.Text = ss.ElmRecruitmentDivision;
                        txtServiceStart.SelectedDate = ss.ElmStartDate;
                        txtAdress.Text = ss.ElmAdress;
                        txtMNumber.Text = ss.ElmMNumber;
                        ImgElementImage.Source = ConvertByteArrayToBitMapImage(ss.ElmPhoto);
                        ServiceSS = (int)ss.ElmServiceState;
                        Immg = Conv.ConvertByteArrayToBitMapImage(ss.ElmPhoto);
                    }
            }
            using (var HR = new SubjectivityContext())
            {
                var table = HR.Ranks;
                var searchresult = table.Where(c => c.RankId.Equals(RankDD));
                foreach (var ss in searchresult)
                {
                    txtRank.Text = ss.RankName;
                }
            }
            using (var HR = new SubjectivityContext())
            {
                    Microsoft.EntityFrameworkCore.DbSet<ServiceState> table = HR.ServiceStates;
                var searchresult = table.Where(c => c.ServiceStateId.Equals(ServiceSS));
                foreach (var ss in searchresult)
                {
                    txtServicState.Text = ss.ServiceState1;
                }
            } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            try
            {
            BitmapImage img = new BitmapImage();
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = memStream;
                img.EndInit();
                img.Freeze();
            }
            return img;
            }
            catch
            {
                return null;
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

            CloseButton.Background = (Brush)(new BrushConverter().ConvertFrom("#FF222831"));
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Background = Brushes.Red;
        }
        BitmapImage Immg;
        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                ImgElementImage.Source = new BitmapImage(fileUri);
                Immg = new BitmapImage(fileUri);
            }
        }
        ConvertImage Conv = new ConvertImage();
        private void BtnAddELM_Click(object sender, RoutedEventArgs e)
        {
            
            int F_State;
            if (txtFamilyState.Text == "عازب")
            {
                F_State = 0;
            }
            else if (txtFamilyState.Text == "متزوج")

            {
                F_State = 1;
            }
            else
            {
                F_State = 2;
            }
            bool GNder;
            if (txtGender.Text == "ذكر")
            {
                GNder = true;
            }
            else
                GNder = false;

            bool IdTp;
            if (txtIdType.Text == "جديدة")
            {
                IdTp = true;
            }
            else
                IdTp = false;

            bool isRes;
            if (txtIsRes.Text == "مجند")
            {
                isRes = true;
            }
            else
                isRes = false;
            foreach (var ctl in GrdTexts.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    if (((TextBox)ctl).Text == String.Empty)
                    {
                        MessageBox.Show("هناك حقول فارغة يرجى ملئها أولا ثم اعد المحاولة", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                        BtnAddELM.Content = "اضافة";
                        this.IsEnabled = true;
                        return;
                    }
                }
            }
            using (var HR = new SubjectivityContext())
            {
                var SS = HR.Elements.First(a => a.ElmId == ELM_ID2);
                SS.ElmName = txtName.Text;
                SS.ElmAdress = txtAdress.Text;
                SS.ElmBlockNumber = txtBlock.Text;
                SS.ElmBrithDate = txtBrith.SelectedDate;
                SS.ElmBrithLocation = txtLocation.Text;
                SS.ElmFamilyState = F_State;
                SS.ElmFather = txtFatherN.Text;
                SS.ElmGendar = GNder;
                SS.ElmIdType = IdTp;
                SS.ElmIsRecruiter = isRes;
                SS.ElmLastName = txtLastN.Text;
                SS.ElmMNumber = txtMNumber.Text;
                SS.ElmMother = txtMother.Text;
                SS.ElmNoteMNumber = int.Parse(txtResNoteN.Text);
                SS.ElmPersonalIdNumber = txtIDNumber.Text;
                if (Immg != null) { SS.ElmPhoto = Conv.getJPGFromImageControl(Immg); }
                SS.ElmRank = int.Parse(txtRank.SelectedValue.ToString());
                SS.ElmRecruitmentDivision = txtRdev.Text;
                SS.ElmServiceState = int.Parse(txtServicState.SelectedValue.ToString());
                SS.ElmStartDate = txtServiceStart.SelectedDate;
                SS.CityId = (int)txtCity.SelectedValue;
                SS.UnitId = (int)txtunits.SelectedValue;
                HR.SaveChanges();
            }
            Rec.AddActiveRecc("تعديل بيانات العنصر رقم العسكري :"  +txtMNumber.Text);
            MessageBox.Show("تم التعديل بنجاح !!");
            this.Close();
        }
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            ImgViewer ss = new ImgViewer(Conv.getJPGFromImageControl(Immg));
            ss.Show();
        }

        private void BtnAddUnit_Click(object sender, RoutedEventArgs e)
        {
            var ss = new AddUnit();
            ss.ShowDialog();
        }
    }
}
