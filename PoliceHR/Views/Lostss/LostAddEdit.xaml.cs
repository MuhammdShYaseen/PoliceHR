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

namespace PoliceHR.Views.Lostss
{
    /// <summary>
    /// Interaction logic for LostAddEdit.xaml
    /// </summary>
    public partial class LostAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Lost_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public LostAddEdit(int Elm_ID,int Lost_ID)
        {
            InitializeComponent();
            Lost_ID2 = Lost_ID;
            Elm_ID2 = Elm_ID;
            if (Lost_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Losts;
                        var searchresult = table.Where(c => c.LostId.Equals(Lost_ID));
                        foreach (var ss in searchresult)
                        {
                            txtDetails.Text = ss.Description;
                            txtLocation.Text = ss.Location;
                            txtLostDate.SelectedDate = ss.LostDate;
                            txtOrderNum.Text = ss.OrderNumber;
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
            if (Lost_ID2 != 0)
            {
                EditLost(Lost_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddLost(Elm_ID2);
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
        private void AddLost(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Lost { ElementId = Element_ID, Description = txtDetails.Text, OrderImage = Conv.getJPGFromImageControl(Immg), OrderDate = txtOrderDate.SelectedDate, OrderNumber = txtOrderNum.Text, LostDate = txtOrderDate.SelectedDate, Location = txtLocation.Text };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة فقدان  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditLost(int Lostss_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Losts.First(a => a.LostId == Lostss_ID);
                   ss.Description = txtDetails.Text;
                   ss.Location = txtLocation.Text;
                   ss.LostDate = txtLostDate.SelectedDate;
                   ss.OrderNumber = txtOrderNum.Text;
                   ss.OrderDate = txtOrderDate.SelectedDate;

                    if (Immg != null) { ss.OrderImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل فقدان رقم :" + Lost_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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