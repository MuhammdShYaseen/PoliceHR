using PoliceHR.ViewModels;
using PoliceHR.Views.Reports;
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

namespace PoliceHR.Views.PrintDoc
{
    /// <summary>
    /// Interaction logic for PrintDocView.xaml
    /// </summary>
    public partial class PrintDocView : Window
    {
        int Elm_ID2;
        public PrintDocView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            GetElmPersonalInfo s = new GetElmPersonalInfo();
            txtCaption.Text += " " + s.ID_TO_NAME(Elm_ID);
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

        private void BtnInfoAndService_Click(object sender, RoutedEventArgs e)
        {
            ListOfServicesAndInformation ss = new ListOfServicesAndInformation();
            ss.Show();
        }
    }
}
