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

namespace PoliceHR.Views.Martyrss
{
    /// <summary>
    /// Interaction logic for MartyrAddEdit.xaml
    /// </summary>
    public partial class MartyrAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Mart_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public MartyrAddEdit(int Elm_ID,int Mart_ID)
        {
            InitializeComponent();
            Mart_ID2 = Mart_ID;
            Elm_ID2 = Elm_ID;
            if (Mart_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Martyrs;
                        var searchresult = table.Where(c => c.MartyrId.Equals(Mart_ID));
                        foreach (var ss in searchresult)
                        {
                            txtLocation.Text = ss.Location;
                            txtMartnrDate.SelectedDate = ss.MartyrDate;
                            txtOrderNum.Text = ss.OrderNumber;
                            txtOrderDate.SelectedDate = ss.OrderDate;
                            txtReson.Text = ss.Reason;
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
            if (Mart_ID2 != 0)
            {
                EditMartyrs(Mart_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else if(MCount() == 0)
            {
                AddMartyr(Elm_ID2);
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
            } else if (MCount() > 0)
            {
                MessageBox.Show("لا يمكن اضافة اكثر من واقعة شهادة واحدة فقط لكل عنصر", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.IsEnabled = true;
                return;
            }
        }
        private void AddMartyr(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Martyr { ElementId = Element_ID, Location = txtLocation.Text, OrderImage = Conv.getJPGFromImageControl(Immg), MartyrDate = txtMartnrDate.SelectedDate, OrderNumber = txtOrderNum.Text, Reason = txtReson.Text,OrderDate=txtOrderDate.SelectedDate};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة  واقعة استشهاد :"  + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditMartyrs(int MartyrId)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Martyrs.First(a => a.MartyrId == MartyrId);

                   ss.Location = txtLocation.Text;
                   ss.MartyrDate = txtMartnrDate.SelectedDate;
                   ss.OrderNumber = txtOrderNum.Text;
                   ss.OrderDate = txtOrderDate.SelectedDate;
                   ss.Reason = txtReson.Text;
                    if (Immg != null) { ss.OrderImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل على واقعة استشهاد رقم :" + MartyrId.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
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
        private int MCount()
        {
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Martyrs;
                var searchresult = table.Where(c => c.ElementId.Equals(Elm_ID2));
                int SearchCount = searchresult.Count();
                return SearchCount;
            }
            catch
            {
                return -1;
            }
        }
    }
}
