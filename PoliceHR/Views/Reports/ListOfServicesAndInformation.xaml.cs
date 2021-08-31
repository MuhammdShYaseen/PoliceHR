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

namespace PoliceHR.Views.Reports
{
    /// <summary>
    /// Interaction logic for ListOfServicesAndInformation.xaml
    /// </summary>
    public partial class ListOfServicesAndInformation : Window
    {
        public ListOfServicesAndInformation()
        {
            InitializeComponent();
        }
        private void BtnPrint1_Click(object sender, RoutedEventArgs e)
        {
            FirstPage.Print();
        }
    }
}
