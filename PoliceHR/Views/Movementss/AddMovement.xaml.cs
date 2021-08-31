using Microsoft.Win32;
using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.ImagesViewer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PoliceHR.Views.Movementss
{
    /// <summary>
    /// Interaction logic for AddMovement.xaml
    /// </summary>
    public partial class AddMovement : Window
    {
        int Elm_ID2;
        public AddMovement(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
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
        ConvertImage Converters = new ConvertImage();
        private void BtnAddMove_Click(object sender, RoutedEventArgs e)
        {
            
            this.IsEnabled = false;
            foreach (var ctl in GrdTexts.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    if (((TextBox)ctl).Text == String.Empty)
                    {
                        MessageBox.Show("هناك حقول فارغة يرجى ملئها أولا ثم اعد المحاولة", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                        BtnAddMove.Content = "اضافة";
                        this.IsEnabled = true;
                        return;
                    }
                }
            }
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new PoliceServicesMovement { ElmId = Elm_ID2,ServiceName=txtServiceName.Text, OrderImage = Converters.getJPGFromImageControl(Immg), StateCity = txtStateCity.Text, OrdarNumber = txtOrderNum.Text, OrderSource = txtOrderSource.Text,  EndDate = txtEndDate.SelectedDate, PoliceUnitName = txtPoliceUnitName.Text, StartDate = txtStartDate.SelectedDate,OrderDate=txtOrderDate.SelectedDate };
                    HR.Add(SS);
                    HR.SaveChanges();
                }
                AddActiveRec Rec = new AddActiveRec();
                Rec.AddActiveRecc(" اضافة تنقل  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
        }
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Converters.getJPGFromImageControl(Immg));
            ss.Show();
        }
    }
}
