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

namespace PoliceHR.Views.Desertionss
{
    /// <summary>
    /// Interaction logic for DesertionAddEdit.xaml
    /// </summary>
    public partial class DesertionAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Deser_ID2, Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public DesertionAddEdit(int Elm_ID,int Deser_ID)
        {
            InitializeComponent();
            Deser_ID2 = Deser_ID;
            Elm_ID2 = Elm_ID;
            if (Deser_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Desertions;
                        var searchresult = table.Where(c => c.DesertionId.Equals(Deser_ID));
                        foreach (var ss in searchresult)
                        {
                            txtDesertion.SelectedDate = ss.DesertionDate;
                            txtRejoinDate.SelectedDate = ss.Rejoin;
                            txtTelegramDate.SelectedDate = ss.TelegramDate;
                            txtTelegramNum.Text = ss.TelegramNumber;
                            Elm_ID2 = (int)ss.ElementId;
                            txtTelegramRejoinNum.Text = ss.RejoinTelegram;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.TelegramImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.TelegramImage);
                            ImgRejoin.Source = Conv.ConvertByteArrayToBitMapImage(ss.RejoinImage);
                            Immg2 = Conv.ConvertByteArrayToBitMapImage(ss.RejoinImage);
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
            if (Deser_ID2 != 0)
            {
                EditDess(Deser_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddDess(Elm_ID2);
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
        private void AddDess(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Desertion { ElementId = Element_ID,  DesertionDate = txtDesertion.SelectedDate, TelegramImage = Conv.getJPGFromImageControl(Immg), RejoinImage = Conv.getJPGFromImageControl(Immg2),  Rejoin = txtRejoinDate.SelectedDate, TelegramNumber = txtTelegramNum.Text,  TelegramDate = txtTelegramDate.SelectedDate,RejoinTelegram=txtTelegramRejoinNum.Text};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة الفرار رقم :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditDess(int Dess_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Desertions.First(a => a.DesertionId == Dess_ID);
                    ss.DesertionDate = txtDesertion.SelectedDate;
                    ss.Rejoin = txtRejoinDate.SelectedDate;
                    ss.TelegramDate = txtTelegramDate.SelectedDate;
                    ss.TelegramNumber = txtTelegramNum.Text;
                    ss.RejoinTelegram = txtTelegramRejoinNum.Text;
                    if (Immg != null) { ss.TelegramImage = Conv.getJPGFromImageControl(Immg); }
                    if (Immg2 != null) { ss.RejoinImage = Conv.getJPGFromImageControl(Immg2); }
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل الفرار رقم :" + Deser_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        BitmapImage Immg2;
        private void BtnSelectPhotoRejoin_Click(object sender, RoutedEventArgs e)
        {
            var SS = new OpenImageFromPc();
            Immg2 = SS.OpenFromPC();
            ImgRejoin.Source = Immg2;
        }

        private void BtnShowPhotoRejoin_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(Immg2));
            ss.Show();
        }

        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(Immg));
            ss.Show();
        }
    }
}
