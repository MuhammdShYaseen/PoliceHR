using System;
using System.Collections.Generic;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace PoliceHR.ViewModels
{
    public class PrintDataGrid
    {
       public void PrintData (DataGrid Grd, StackPanel STK,int Elm_ID,TextBlock txt)
        {
            Brush Back = Grd.Background;
            Brush Fore = Grd.Foreground;
            Grd.Background = Brushes.White;
            Grd.Foreground = Brushes.Black;
            var ss = new GetElmPersonalInfo();
            txt.Text = ss.ID_TO_NAME(Elm_ID);
            txt.Visibility = Visibility.Visible;
            var pd = new PrintDialog();
            var result = pd.ShowDialog();
            if (result.HasValue && result.Value)
                pd.PrintVisual(STK, "طباعة");
            Grd.Background = Back;
            Grd.Foreground = Fore;
            txt.Visibility = Visibility.Hidden;
        }

    }
}
