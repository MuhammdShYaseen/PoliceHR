using Microsoft.Win32;
using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.ImagesViewer;
using PoliceHR.Views.Units;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Interaction logic for AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        public AddElement()
        {
            InitializeComponent();
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
            {
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    this.DragMove();
                }
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

        private void BtnAddELM_Click(object sender, RoutedEventArgs e)
        {
            BtnAddELM.Content = "يرجى الانتظار...";
            this.IsEnabled = false;
            int F_State;
            if (txtFamilyState.Text == "عازب")
            {
                F_State = 0;
            } else if (txtFamilyState.Text == "متزوج")
               
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
            } else
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
                        if(((TextBox)ctl).Text == String.Empty)
                        {
                        MessageBox.Show("هناك حقول فارغة يرجى ملئها أولا ثم اعد المحاولة", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                        BtnAddELM.Content = "اضافة";
                        this.IsEnabled = true;
                        return;
                        }
                    }          
            }
            try
            {
              using (SubjectivityContext HR = new SubjectivityContext())
              {
                object ELM = new Element {ElmName=txtName.Text, ElmAdress=txtAdress.Text,ElmBlockNumber=txtBlock.Text,ElmBrithDate=txtBrith.SelectedDate,ElmBrithLocation=txtLocation.Text,ElmFamilyState=F_State,ElmFather=txtFatherN.Text,ElmGendar= GNder,ElmIdType= IdTp,ElmIsRecruiter= isRes,ElmLastName=txtLastN.Text,ElmMNumber=txtMNumber.Text,ElmMother=txtMother.Text,ElmNoteMNumber=int.Parse(txtResNoteN.Text),ElmPersonalIdNumber=txtIDNumber.Text,ElmPhoto= getJPGFromImageControl(Immg), ElmRank=int.Parse (txtRank.SelectedValue.ToString()),ElmRecruitmentDivision=txtRdev.Text,ElmServiceState= int.Parse(txtServicState.SelectedValue.ToString()),ElmStartDate= txtServiceStart.SelectedDate, UnitId = (int)txtunits.SelectedValue, CityId = (int)txtCity.SelectedValue };
                HR.Add(ELM);
                    HR.SaveChanges();
              }
                Rec.AddActiveRecc(" اضافة العنصر رقم :"+ " الرقم العسكري : " + txtMNumber.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            foreach (var ctl in GrdTexts.Children)
            {
                try
                {
                    if (ctl.GetType() == typeof(TextBox))
                        ((TextBox)ctl).Text = String.Empty;
                }
                catch { }
                
            }
            this.IsEnabled = true;
            BtnAddELM.Content = "اضافة";
        }
        BitmapImage Immg; 
        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               OpenFileDialog openFileDialog = new OpenFileDialog();
               if (openFileDialog.ShowDialog() == true)
               {
                Uri fileUri = new Uri(openFileDialog.FileName);
                ImgElementImage.Source = new BitmapImage(fileUri);
                Immg = new BitmapImage(fileUri);
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public byte[] getJPGFromImageControl(BitmapImage imageC)
        {
            try
            {
                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageC));
                encoder.Save(memStream);
                return memStream.ToArray();
            }
            catch { return null; }
            
        }
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            ImgViewer ss = new ImgViewer(getJPGFromImageControl(Immg));
            ss.Show();
        }

        private void BtnAddUnit_Click(object sender, RoutedEventArgs e)
        {
            AddUnit ss = new AddUnit();
            ss.Show();
        }
    }
}
