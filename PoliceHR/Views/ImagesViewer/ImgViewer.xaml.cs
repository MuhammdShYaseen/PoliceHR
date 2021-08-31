using Microsoft.Win32;
using PoliceHR.ViewModels;
using System;
using System.Collections.Generic;
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

namespace PoliceHR.Views.ImagesViewer
{
    /// <summary>
    /// Interaction logic for ImgViewer.xaml
    /// </summary>
    public partial class ImgViewer : Window
    {
        BitmapImage ImgToPrint = new BitmapImage();
        public ImgViewer(byte[] ImageToView)
        {
            InitializeComponent();
            ConvertImage Conv = new ConvertImage();
            ImgFullImage.Source = Conv.ConvertByteArrayToBitMapImage(ImageToView);
            ImgToPrint= Conv.ConvertByteArrayToBitMapImage(ImageToView);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawingVisual vis = new DrawingVisual();
            
            using (DrawingContext dc = vis.RenderOpen())
            {
                if (ImgToPrint != null)
                {
                    dc.DrawImage(ImgToPrint, new Rect { Width = ImgToPrint.Width, Height = ImgToPrint.Height });
                }
            }

            PrintDialog pdialog = new PrintDialog();
            if (pdialog.ShowDialog() == true)
            {
                pdialog.PrintVisual(vis, "طباعة الصورة");
            }
        }

        private void BtnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (ImgToPrint != null)
            {
               SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "JPeg Image|*.jpg";
                saveFileDialog1.Title = "حفظ الصورة بأسم";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    fs.Close();
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(ImgToPrint));
                    using (FileStream stream = new FileStream(fs.Name, FileMode.Create))
                        encoder.Save(stream);
                }
            }
        }
    }
}
