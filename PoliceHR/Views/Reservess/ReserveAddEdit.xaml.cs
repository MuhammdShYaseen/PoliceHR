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

namespace PoliceHR.Views.Reservess
{
    /// <summary>
    /// Interaction logic for ReserveAddEdit.xaml
    /// </summary>
    public partial class ReserveAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Reserve_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public ReserveAddEdit(int Elm_ID,int Reserve_ID)
        {
            InitializeComponent();
            Reserve_ID2 = Reserve_ID;
            Elm_ID2 = Elm_ID;
            if (Reserve_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Reserves;
                        var searchresult = table.Where(c => c.ReserveId.Equals(Reserve_ID));
                        foreach (var ss in searchresult)
                        {
                            txtEndDate.SelectedDate = ss.EndDate;
                            txtstartDate.SelectedDate = ss.StartDate;
                            txtOrderNum.Text = ss.OrderNum;
                            txtOrderDate.SelectedDate = ss.OrderDate;
                            Elm_ID2 = (int)ss.ElementId;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.OrderImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.OrderImage);
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
        BitmapImage Immg;
        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            var SS = new OpenImageFromPc();
            Immg = SS.OpenFromPC();
            ImgElementImage.Source = Immg;
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
            if (Reserve_ID2 != 0)
            {
                EditReserve(Reserve_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddReserve(Elm_ID2);
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
        private void AddReserve(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Reserve { ElementId = Element_ID, StartDate = txtstartDate.SelectedDate, OrderImage = Conv.getJPGFromImageControl(Immg), OrderDate = txtOrderDate.SelectedDate, OrderNum = txtOrderNum.Text,  EndDate = txtEndDate.SelectedDate };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة احتفاظ  :"  + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditReserve(int Reservee_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Reserves.First(a => a.ReserveId == Reservee_ID);
                    ss.EndDate = txtEndDate.SelectedDate;
                    ss.StartDate = txtstartDate.SelectedDate;
                    ss.OrderNum = txtOrderNum.Text;
                    ss.OrderDate = txtOrderDate.SelectedDate;
                    if (Immg != null) { ss.OrderImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل احتفاظ رقم  :" + Reservee_ID.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(Immg));
            ss.Show();
        }
    }
}
