using PoliceHR.Models;
using PoliceHR.ViewModels.SqLiteViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PoliceHR.Views.HomePgs
{
    /// <summary>
    /// Interaction logic for ServerInfo.xaml
    /// </summary>
    public partial class ServerInfo : Page
    {
        string ConnStr = "";
        public ServerInfo()
        {
            InitializeComponent();
            var SLite = new SqLiteVm();
            var UserPss = SLite.UsersSource();
            if(UserPss.Count != 0)
            {
                foreach (var xx in UserPss)
                {
                    txtUserName.Text = xx.User_Name;
                    txtUPassword.Password = xx.Password;
                }
            }
            try
            {
                using (SubjectivityContext ss = new SubjectivityContext())
                {
                    bool CanConn = ss.Database.CanConnect();
                    if (CanConn == true)
                    {
                        StkServer.IsEnabled=false;
                        BtnCheckConnString.Background = Brushes.Green;
                    }
                    else
                    {
                        StkServer.IsEnabled = true;
                    }
                }
            }
            catch
            {
                StkServer.IsEnabled = true;
            }
        }

        private void BtnSaveConnString_Click(object sender, RoutedEventArgs e)
        {
            var Sqlite = new SqLiteVm();
            if (Sqlite.ConnStringSource() == "")
            {
                ConnStr = "Server=" + txtServerName.Text + ";Database=Subjectivity;Integrated Security=False;User ID=" + txtLoginName.Text + ";password=" + txtPassword.Password;
                Sqlite.InsertConnString(ConnStr);
            }
            else
            {
                ConnStr = "Server=" + txtServerName.Text + ";Database=Subjectivity;Integrated Security=False;User ID=" + txtLoginName.Text + ";password=" + txtPassword.Password;
                Sqlite.UpdateConnString(ConnStr, 1);
            }
        }

        private void BtnCheckConnString_Click(object sender, RoutedEventArgs e)
        {
            var Sqlite = new SqLiteVm();
            if(Sqlite.ConnStringSource() == "")
            {
                ConnStr = "Server=" + txtServerName.Text + ";Database=Subjectivity;Integrated Security=False;User ID=" + txtLoginName.Text + ";password=" + txtPassword.Password;
                Sqlite.InsertConnString(ConnStr);
            }
            else
            {
                ConnStr = "Server=" + txtServerName.Text + ";Database=Subjectivity;Integrated Security=False;User ID=" + txtLoginName.Text + ";password=" + txtPassword.Password;
                Sqlite.UpdateConnString(ConnStr,1);
            }
            using (SubjectivityContext ss = new SubjectivityContext())
            {
                bool CanConn = ss.Database.CanConnect();
                if (CanConn == true)
                {
                    BtnCheckConnString.Background = Brushes.Green;
                }
                else
                {
                    BtnCheckConnString.Background = Brushes.Red;
                }
                
            }
        }

        private void BtnCheckUser_Click(object sender, RoutedEventArgs e)
        {
            var HR = new SubjectivityContext();
            var table = HR.Users;
            var searchresult = table.Where(c => c.UserName.Equals(txtUserName.Text) && c.Password.Equals(txtUPassword.Password));
            if(searchresult.Count() != 0)
            {
                BtnCheckUser.Background = Brushes.Green;
            }
            else
            {
                BtnCheckUser.Background = Brushes.Red;
            }
        }

        private void BtnSaveUserAndPass_Click(object sender, RoutedEventArgs e)
        {
            var SLite = new SqLiteVm();
            var HR = new SubjectivityContext();
            var table = HR.Users;
            var Usource = SLite.UsersSource();
            var searchresult = table.Where(c => c.UserName.Equals(txtUserName.Text) && c.Password.Equals(txtUPassword.Password));
            if (searchresult.Count() != 0)
            {
                BtnCheckUser.Background = Brushes.Green;
                foreach (var ss in searchresult)
                {
                    if (Usource.Count == 0)
                    {
                        SLite.InsertLogins(ss.UserName, ss.Password);
                    }
                    else
                    {
                        SLite.UpdateLogins(ss.UserName, ss.Password,1);
                    }
                    
                }
            }
        }
    }
}
