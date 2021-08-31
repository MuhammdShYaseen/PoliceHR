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

namespace PoliceHR.Views.Cycless
{
    /// <summary>
    /// Interaction logic for CycleAddEdit.xaml
    /// </summary>
    public partial class CycleAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        ConvertImage Conv = new ConvertImage();
        int Elm_ID2, Cycle_ID2;
        public CycleAddEdit(int Elm_ID,int Cycle_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            Cycle_ID2 = Cycle_ID;
            if (Cycle_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Cycles;
                        var searchresult = table.Where(c => c.CycleId.Equals(Cycle_ID));
                        foreach (var ss in searchresult)
                        {
                            txtCycleName.Text = ss.CycleName;
                            txtOrderDate.SelectedDate = ss.OrderDate;
                            txtOrderNumber.Text = ss.OrderNumber;
                            txtStartDate.SelectedDate = ss.CycleStartDate;
                            txtEndDate.SelectedDate = ss.CycleEndDate;
                            txtDetails.Text = ss.Descraption;
                            Elm_ID2 = (int)ss.ElmId;
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
            if (Cycle_ID2 != 0)
            {
                EditCycle(Cycle_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddCycle(Elm_ID2);
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
        private void AddCycle(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Cycle { ElmId = Element_ID, CycleName = txtCycleName.Text, OrderImage = Conv.getJPGFromImageControl(Immg), Descraption = txtDetails.Text, OrderDate = txtOrderDate.SelectedDate, OrderNumber = txtOrderNumber.Text, CycleEndDate = txtEndDate.SelectedDate, CycleStartDate=txtStartDate.SelectedDate };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة دورة  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditCycle(int Cyclee_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Cycles.First(a => a.CycleId == Cyclee_ID);
                    ss.CycleName = txtCycleName.Text;
                    ss.OrderDate = txtOrderDate.SelectedDate;
                    ss.OrderNumber = txtOrderNumber.Text;
                    ss.CycleStartDate = txtStartDate.SelectedDate;
                    ss.CycleEndDate = txtEndDate.SelectedDate;
                    ss.Descraption = txtDetails.Text;
                    if (Immg != null) { ss.OrderImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل دورة رقم :" + Cycle_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
