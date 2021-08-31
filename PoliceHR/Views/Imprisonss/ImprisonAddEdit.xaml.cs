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

namespace PoliceHR.Views.Imprisonss
{
    /// <summary>
    /// Interaction logic for ImprisonAddEdit.xaml
    /// </summary>
    public partial class ImprisonAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Imprison_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public ImprisonAddEdit(int Elm_ID,int Imprison_ID)
        {
            InitializeComponent();
            Imprison_ID2 = Imprison_ID;
            Elm_ID2 = Elm_ID;
            if (Imprison_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Imprisons;
                        var searchresult = table.Where(c => c.ImprisonId.Equals(Imprison_ID));
                        foreach (var ss in searchresult)
                        {
                            txtReson.Text = ss.Reason;
                            txtOrderNum.Text = ss.OrderNumber;
                            txtImpDate.SelectedDate = ss.ImprisonDate;
                            txtEntityImp.Text = ss.ImprisonEntity;
                            txtEndDate.SelectedDate = ss.EndDate;
                            Elm_ID2 = (int)ss.ElementId;
                            txtOrderEndNum.Text = ss.EndOrderNum;
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
            if (Imprison_ID2 != 0)
            {
                EditImp(Imprison_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddImp(Elm_ID2);
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
        private void AddImp(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Imprison { ElementId = Element_ID, Reason = txtReson.Text, OrderImage = Conv.getJPGFromImageControl(Immg), ImprisonDate = txtImpDate.SelectedDate, OrderNumber = txtOrderNum.Text, ImprisonEntity = txtEntityImp.Text, EndDate = txtEndDate.SelectedDate,EndOrderNum=txtOrderEndNum.Text };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة توقيف رقم :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditImp(int Impp_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Imprisons.First(a => a.ImprisonId == Impp_ID);
                    ss.Reason = txtReson.Text;
                    ss.OrderNumber = txtOrderNum.Text;
                    ss.ImprisonDate = txtImpDate.SelectedDate;
                    ss.ImprisonEntity = txtEntityImp.Text;
                    ss.EndDate = txtEndDate.SelectedDate;
                    ss.EndOrderNum = txtOrderEndNum.Text;
                    if (Immg != null) { ss.OrderImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل توقيف رقم :" + Impp_ID.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
    }

}