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

namespace PoliceHR.Views.Decorationss
{
    /// <summary>
    /// Interaction logic for AddDecoration.xaml
    /// </summary>
    public partial class AddDecoration : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Elmid2;
        public AddDecoration(int Elmid)
        {
            InitializeComponent();
            Elmid2 = Elmid;
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
        ConvertImage Conv = new ConvertImage();
        private void BtnAddDec_Click(object sender, RoutedEventArgs e)
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
            try
            {
             using (SubjectivityContext HR = new SubjectivityContext())
            {
                object SS = new Decoration { ElmId = Elmid2, DecorationsName = txtDecorationsName.Text, OrderImage = Conv.getJPGFromImageControl(Immg), OrderDate = txtOrderDate.SelectedDate, OrderNumber = txtOrderNumber.Text, Reason = txtReason.Text, Source = txtSource.Text };
                HR.Add(SS);
                HR.SaveChanges();
                this.IsEnabled = true;
            }
                Rec.AddActiveRecc("اضافة وسام او مكافئة  :" + " للعنصر : " + Rec.ELmMNimber(Elmid2));
            }
            catch(Exception ex)
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
        }
        BitmapImage Immg;
        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            var SS = new OpenImageFromPc();
            Immg=SS.OpenFromPC();
            ImgElementImage.Source = Immg;
        }
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(Immg));
            ss.Show();
        }
    }
}
