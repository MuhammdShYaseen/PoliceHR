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

namespace PoliceHR.Views
{
    /// <summary>
    /// Interaction logic for AddPaltss.xaml
    /// </summary>
    public partial class AddPaltss : Window
    {
        int Elmid2;
        public AddPaltss(int ElmId)
        {
            InitializeComponent();
            Elmid2 = ElmId;
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
        private void BtnAddPlts_Click(object sender, RoutedEventArgs e)
        {
            BtnAddPlts.Content = "يرجى الانتظار...";
            this.IsEnabled = false;
            int PenTp;
            if (txtPnType.Text == "حسم من الراتب")
            {
                PenTp = 0;
            }
            else if (txtPnType.Text == "توقيف بسيط")

            {
                PenTp = 1;
            }
            else
            {
                PenTp = 2;
            }
            foreach (var ctl in GrdTexts.Children)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    if (((TextBox)ctl).Text == String.Empty)
                    {
                        MessageBox.Show("هناك حقول فارغة يرجى ملئها أولا ثم اعد المحاولة", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                        BtnAddPlts.Content = "اضافة";
                        this.IsEnabled = true;
                        return;
                    }
                }
            }
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                  object SS = new Penalty { ElmId=Elmid2, PenaltieDate = txtPnDate.SelectedDate, PenaltieImage = Converters.getJPGFromImageControl(Immg), PenaltieNotes = txtPnDes.Text , Name=txtPnName.Text ,PenaltieNumber = txtPnNum.Text ,PenaltiesReason=txtReson.Text,PenaltieType= PenTp,Rank=txtRanks.Text};
                  HR.Add(SS);
                  HR.SaveChanges();
                }
                AddActiveRec Rec = new AddActiveRec();
                Rec.AddActiveRecc(" اضافة عقوبة  :" + " للعنصر : " + Rec.ELmMNimber(Elmid2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
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
            BtnAddPlts.Content = "اضافة";
        }
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Converters.getJPGFromImageControl(Immg));
            ss.Show();
        }
    }
}
