using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views;
using PoliceHR.Views.GeneralDocs;
using PoliceHR.Views.HomePgs;
using PoliceHR.Views.LoadingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PoliceHR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool StateClosed = true;
        public MainWindow()
        {
            InitializeComponent();
            
            try
            {
                using (SubjectivityContext ss = new SubjectivityContext())
                {
                    bool CanConn = ss.Database.CanConnect();
                    if (CanConn == false)
                    {
                        FrmNavication.Navigate(new ServerInfo());
                    }
                    else
                    {
                        FrmNavication.Navigate(new HomePg());
                    }
                }
            }
            catch
            {
                FrmNavication.Navigate(new ServerInfo());
            }
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                BtnTextActivitis.Text = "سجل النشاطات";
                BtnTextPrint.Text = "ارشيف الوثائق";
                BtnTextAbout.Text = "حول البرنامج";
                BtnTextMain.Text = "الصفحة الرئيسية";
                BtnTextLogin.Text = "بيانات الاتصال بالسيرفر";
                BtnTextFromAccess.Text = "استيراد من ملف Access";
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
                BtnTextActivitis.Text = "";
                BtnTextPrint.Text = "";
                BtnTextAbout.Text = "";
                BtnTextMain.Text = "";
                BtnTextLogin.Text = "";
                BtnTextFromAccess.Text = "";
            }

            StateClosed = !StateClosed;
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
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
                    Application.Current.MainWindow.DragMove();
                }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BtnMainPage.Background = Brushes.DarkGray;
        }

        private void BtnMainPage_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new HomePg());
            BtnMainPage.Background = Brushes.DarkGray;
            BtnActivites.Background = Brushes.Transparent;
            BtnPrint.Background = Brushes.Transparent;
            BtnAbout.Background = Brushes.Transparent;
            BtnLogin.Background = Brushes.Transparent;
            BtnFromAccess.Background = Brushes.Transparent;
        }



        private void BtnActivites_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3 || Rolee.CheckUserRolee() == 2 || Rolee.CheckUserRolee() == 0)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            FrmNavication.Navigate(new Activites());
            BtnMainPage.Background = Brushes.Transparent;
            BtnActivites.Background = Brushes.DarkGray;
            BtnPrint.Background = Brushes.Transparent;
            BtnAbout.Background = Brushes.Transparent;
            BtnLogin.Background = Brushes.Transparent;
            BtnFromAccess.Background = Brushes.Transparent;
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new Printers());
            BtnMainPage.Background = Brushes.Transparent;
            BtnActivites.Background = Brushes.Transparent;
            BtnPrint.Background = Brushes.DarkGray;
            BtnAbout.Background = Brushes.Transparent;
            BtnLogin.Background = Brushes.Transparent;
            BtnFromAccess.Background = Brushes.Transparent;
        }

        private void addRowButton_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole ss = new CheckUserRole();
            if ( ss.CheckUserRolee() == 4 || ss.CheckUserRolee() == 0)
            {
                _ = MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var p = FrmNavication.Content as Page;
            if (p.Title=="Printers")
            {
                GenDocAddEdit AddDoc = new GenDocAddEdit(0);
                AddDoc.Show();
            }
            else if (p.Title=="HomePg")
            {
                AddElement addElement = new AddElement();
                addElement.Show();
            }
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {

            CloseButton.Background = (Brush)(new BrushConverter().ConvertFrom("#FF222831"));
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Background = Brushes.Red;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new ServerInfo());
            BtnMainPage.Background = Brushes.Transparent;
            BtnActivites.Background = Brushes.Transparent;
            BtnPrint.Background = Brushes.Transparent;
            BtnAbout.Background = Brushes.Transparent;
            BtnLogin.Background = Brushes.DarkGray;
            BtnFromAccess.Background = Brushes.Transparent;
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            FrmNavication.Navigate(new AboutUs());
            BtnMainPage.Background = Brushes.Transparent;
            BtnActivites.Background = Brushes.Transparent;
            BtnPrint.Background = Brushes.Transparent;
            BtnLogin.Background = Brushes.Transparent;
            BtnAbout.Background = Brushes.DarkGray;
            BtnFromAccess.Background = Brushes.Transparent;
        }

        private void BtnFromAccess_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() != 1)
            {
                MessageBox.Show("يرجى تسجيل الدخول بحساب مسؤول أولا", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            FromAcsess ss = new FromAcsess();
            ss.ShowDialog();
        }
    }
}
