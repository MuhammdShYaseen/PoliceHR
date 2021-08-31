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

namespace PoliceHR.Views.Decorationss
{
    /// <summary>
    /// Interaction logic for EditDecoration.xaml
    /// </summary>
    public partial class EditDecoration : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        ConvertImage Conv = new ConvertImage();
        int Dec_ID2;
        int Elm_ID2;
        public EditDecoration(int Dec_ID)
        {
            InitializeComponent();
            
            Dec_ID2 = Dec_ID;
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var table = HR.Decorations;
                    var searchresult = table.Where(c => c.DecorationId.Equals(Dec_ID2));
                    foreach (var ss in searchresult)
                    {
                        txtDecorationsName.Text = ss.DecorationsName;
                        txtOrderDate.SelectedDate = ss.OrderDate;
                        txtOrderNumber.Text = ss.OrderNumber;
                        txtReason.Text = ss.Reason;
                        txtSource.Text = ss.Source;
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
            var SS = new OpenImageFromPc();
            Immg = SS.OpenFromPC();
            ImgElementImage.Source = Immg;
        }

        private void BtnEditDec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var SS = HR.Decorations.First(a => a.DecorationId == Dec_ID2);
                    SS.DecorationsName = txtDecorationsName.Text;
                    SS.OrderDate =  txtOrderDate.SelectedDate;
                    SS.OrderNumber  =txtOrderNumber.Text;
                    SS.Reason = txtReason.Text;
                    SS.Source = txtSource.Text;
                    if (Immg != null) { SS.OrderImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل الوسام المكافئة رقم :" + Dec_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
    }
}
