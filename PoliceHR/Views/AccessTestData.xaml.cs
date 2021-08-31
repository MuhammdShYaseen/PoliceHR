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
    /// Interaction logic for AccessTestData.xaml
    /// </summary>
    public partial class AccessTestData : Window
    {
        public AccessTestData(System.Collections.IEnumerable DataS)
        {
            InitializeComponent();
            GrdAcc.ItemsSource = DataS;
        }
    }
}
