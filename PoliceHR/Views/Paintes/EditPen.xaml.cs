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

namespace PoliceHR.Views.Paintes
{
    /// <summary>
    /// Interaction logic for EditPen.xaml
    /// </summary>
    public partial class EditPen : Window
    {
        ConvertImage Conv = new ConvertImage();
        int Pen_ID2;
        int Elm_ID2;
        public EditPen(int Pen_ID)
        {
            InitializeComponent();
            Pen_ID2 = Pen_ID;
            try
            { 
                using (var HR = new SubjectivityContext())
                {
                   var table = HR.Penalties;
                    var searchresult = table.Where(c => c.PenaltiesId.Equals(Pen_ID2));
                   foreach (var ss in searchresult)
                   {
                        txtPnDate.SelectedDate = ss.PenaltieDate;
                        txtPnDes.Text = ss.PenaltieNotes;
                        txtPnName.Text = ss.Name;
                        txtPnNum.Text = ss.PenaltieNumber;
                        txtRanks.Text = ss.Rank;
                        txtReson.Text = ss.PenaltiesReason;
                        Elm_ID2 = (int)ss.ElmId;
                    ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.PenaltieImage);
                    if (ss.PenaltieType == 0) { txtPnType.Text = "حسم من الراتب"; } else if (ss.PenaltieType == 1) { txtPnType.Text = "توقيف بسيط"; }else { txtPnType.Text = "توقيف شديد"; }
                    Immg = Conv.ConvertByteArrayToBitMapImage(ss.PenaltieImage);
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
        private void BtnEditPlts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               using (var HR=new SubjectivityContext())
               {
                var SS = HR.Penalties.First(a => a.PenaltiesId == Pen_ID2);
                SS.PenaltieDate=txtPnDate.SelectedDate;
                if (Immg != null) { SS.PenaltieImage = Conv.getJPGFromImageControl(Immg); }
                SS.Name = txtPnName.Text;
                SS.PenaltieNotes = txtPnDes.Text;
                SS.PenaltieNumber = txtPnNum.Text;
                SS.PenaltiesReason = txtReson.Text;
                SS.Rank = txtRanks.Text;
                if(txtPnType.Text== "حسم من الراتب") { SS.PenaltieType = 0; }else if(txtPnType.Text== "توقيف بسيط") { SS.PenaltieType = 1; } else { SS.PenaltieType = 2; }
                HR.SaveChanges();
               }
                AddActiveRec Rec = new AddActiveRec();
                Rec.AddActiveRecc("تعديل عقوبة رقم :" + Pen_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
