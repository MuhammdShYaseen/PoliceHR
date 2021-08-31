using Microsoft.Win32;
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

namespace PoliceHR.Views.Movementss
{
    /// <summary>
    /// Interaction logic for EditMovement.xaml
    /// </summary>
    public partial class EditMovement : Window
    {
        ConvertImage Conv = new ConvertImage();
        int moveID2;
        int Elm_ID2;
        public EditMovement(int moveID)
        {
            InitializeComponent();
            moveID2 = moveID;
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var table = HR.PoliceServicesMovements;
                    var searchresult = table.Where(c => c.PoliceServicesId.Equals(moveID2));
                    foreach (var ss in searchresult)
                    {
                        txtServiceName.Text = ss.ServiceName;
                        txtStateCity.Text = ss.StateCity;
                        txtEndDate.SelectedDate = ss.EndDate;
                        txtStartDate.SelectedDate = ss.StartDate;
                        txtOrderDate.SelectedDate = ss.OrderDate;
                        txtOrderNum.Text = ss.OrdarNumber;
                        txtOrderSource.Text = ss.OrderSource;
                        txtPoliceUnitName.Text = ss.PoliceUnitName;
                        Elm_ID2 = (int)ss.ElmId;
                        ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.OrderImage);
                        Immg = Conv.ConvertByteArrayToBitMapImage(ss.OrderImage);
                    }
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

        private void BtnAddMove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              using (var HR = new SubjectivityContext())
              {
                     var SS = HR.PoliceServicesMovements.First(a => a.PoliceServicesId == moveID2);
                     SS.PoliceUnitName = txtServiceName.Text;
                     SS.StateCity = txtStateCity.Text;
                     SS.EndDate = txtEndDate.SelectedDate;
                     SS.StartDate = txtStartDate.SelectedDate;
                     SS.OrderDate = txtOrderDate.SelectedDate;
                     SS.OrdarNumber = txtOrderNum.Text;
                     SS.OrderSource = txtOrderSource.Text;
                     SS.PoliceUnitName = txtPoliceUnitName.Text;

                     if (Immg != null) { SS.OrderImage = Conv.getJPGFromImageControl(Immg);}

                HR.SaveChanges();
              }
                AddActiveRec Rec = new AddActiveRec();
                Rec.AddActiveRecc(" تعديل تنقل  :" + moveID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
               this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(Immg));
            ss.Show();
        }
    }
}
