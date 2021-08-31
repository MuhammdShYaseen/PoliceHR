using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.ImagesViewer;
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
using System.Windows.Shapes;

namespace PoliceHR.Views.Layoffss
{
    /// <summary>
    /// Interaction logic for LayoffAddEdit.xaml
    /// </summary>
    public partial class LayoffAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Layoff_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public LayoffAddEdit(int Elm_ID,int Layoff_ID)
        {
            InitializeComponent();
            Layoff_ID2 = Layoff_ID;
            Elm_ID2 = Elm_ID;
            if (Layoff_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Layoffs;
                        var searchresult = table.Where(c => c.LayoffsId.Equals(Layoff_ID));
                        foreach (var ss in searchresult)
                        {
                            txtOrderDate.SelectedDate = ss.OrderDate;
                            txtSeizureDate.SelectedDate = ss.SeizureDate;
                            txtSeizureNumber.Text = ss.SeizureNumber;
                            txtOrderNumber.Text = ss.OrderNumber;
                            txtReson.Text = ss.Reason;
                            ImgSeizure.Source = Conv.ConvertByteArrayToBitMapImage(ss.SeizureImage);
                            ImgOrder.Source = Conv.ConvertByteArrayToBitMapImage(ss.OrderImage);
                            ImmgSeizure = Conv.ConvertByteArrayToBitMapImage(ss.SeizureImage);
                            ImmgOrder = Conv.ConvertByteArrayToBitMapImage(ss.OrderImage);
                            Elm_ID2 = (int)ss.ElmId;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
        BitmapImage ImmgOrder;
        private void BtnSelectOrderPhoto_Click(object sender, RoutedEventArgs e)
        {
            var SS = new OpenImageFromPc();
            ImmgOrder = SS.OpenFromPC();
            ImgOrder.Source = ImmgOrder;
        }
        BitmapImage ImmgSeizure;
        private void BtnSelectSeizurePhoto_Click(object sender, RoutedEventArgs e)
        {
            var SS = new OpenImageFromPc();
            ImmgSeizure = SS.OpenFromPC();
            ImgSeizure.Source = ImmgSeizure;
        }
        private void BtnAddEditV_Click(object sender, RoutedEventArgs e)
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
            if (Layoff_ID2 != 0)
            {
                EditLayoff(Layoff_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddLayoff(Elm_ID2);
                this.IsEnabled = true;
                foreach (var ctl in GrdTexts.Children)
                {
                    try
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = String.Empty;
                    }
                    catch { }

                }
            }
        }
        private void AddLayoff(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Layoff { ElmId = Element_ID, Reason = txtReson.Text, SeizureImage = Conv.getJPGFromImageControl(ImmgSeizure), SeizureDate = txtSeizureDate.SelectedDate, SeizureNumber = txtSeizureNumber.Text, OrderDate = txtOrderDate.SelectedDate, OrderNumber = txtOrderNumber.Text, OrderImage = Conv.getJPGFromImageControl(ImmgOrder)};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc(" اضافة تسريح  " + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditLayoff(int LayoffsId)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Layoffs.First(a => a.LayoffsId == LayoffsId);

                  ss.OrderDate = txtOrderDate.SelectedDate;
                  ss.SeizureDate = txtSeizureDate.SelectedDate;
                  ss.SeizureNumber = txtSeizureNumber.Text;
                  ss.OrderNumber = txtOrderNumber.Text;
                  ss.Reason = txtReson.Text;

                    if (ImmgSeizure != null) { ss.SeizureImage = Conv.getJPGFromImageControl(ImmgSeizure); }
                    if (ImmgOrder != null) { ss.OrderImage = Conv.getJPGFromImageControl(ImmgOrder); }
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل تسريح رقم :" + Layoff_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnShowSeizurePhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(ImmgSeizure));
            ss.Show();
        }
        private void BtnShowOrderPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(ImmgOrder));
            ss.Show();
        }
    }

}
