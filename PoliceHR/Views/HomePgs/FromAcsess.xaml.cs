using PoliceHR.ViewModels;
using PoliceHR.ViewModels.AccessViewModels;
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

namespace PoliceHR.Views.HomePgs
{
    /// <summary>
    /// Interaction logic for FromAcsess.xaml
    /// </summary>
    public partial class FromAcsess : Window
    {
        public FromAcsess()
        {
            InitializeComponent();
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
        private void BtnSelectAccess_Click(object sender, RoutedEventArgs e)
        {

            ImportFromAccess ss = new ImportFromAccess();
            ss.OpenDB(txtPath);
            if(txtPath.Text == string.Empty)
            {
                return;
            }

            AccVM Acc = new AccVM(txtPath.Text);
            if (Acc.ElementsResource() == null)
            {
                txtPath.Text = string.Empty;
                return;
            }
            BtnAsync.Content = " مزامنة بيانات " + "  " + Acc.ElementsResource().Count.ToString() + " عنصر ";
        }

        private void BtnAsync_Click(object sender, RoutedEventArgs e)
        {
            if (txtPath.Text == string.Empty)
            {
                ImportFromAccess Imp = new ImportFromAccess();
                Imp.OpenDB(txtPath);

            }
            else if (txtPath.Text != string.Empty)
            {
                ImportFromAccess ss = new ImportFromAccess();
                ss.AcyncFromAccessToSql(txtPath.Text, PrgAcync, BtnAsync, CloseButton, BtnSelectAccess);

            }
            
            
        }
        private void BtnTestData_Click(object sender, RoutedEventArgs e)
        {
            var Ds = new AccVM(txtPath.Text);
            AccessTestData s = new AccessTestData(Ds.BranchResource());
            s.Show();
        }
    }
}
