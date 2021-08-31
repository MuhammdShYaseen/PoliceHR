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

namespace PoliceHR.Views.DeficitRatioss
{
    /// <summary>
    /// Interaction logic for DeficitRatioAddEdit.xaml
    /// </summary>
    public partial class DeficitRatioAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Elm_ID2, Retio2_ID;
        public DeficitRatioAddEdit(int Elm_ID, int Retio_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            Retio2_ID = Retio_ID;
            if (Retio_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.DeficitRations;
                        var searchresult = table.Where(c => c.DeficitRatio.Equals(Retio_ID));
                        foreach (var ss in searchresult)
                        {
                            txtOrderDate.SelectedDate = ss.DeficitRatioOrderDate;
                            txtOrderResult.Text = ss.DeficitRatioOrderResult;
                            txtPercentage.Text = ss.DeficitRatioPercentage.ToString();
                            txtOrderNumber.Text = ss.OrderNum;
                            Elm_ID2 = (int)ss.ElmId;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.DeficitRatioOrderImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.DeficitRatioOrderImage);
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
        ConvertImage Conv = new ConvertImage();
        private void BtnShowPhoto_Click(object sender, RoutedEventArgs e)
        {
            var ss = new ImgViewer(Conv.getJPGFromImageControl(Immg));
            ss.Show();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
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
            if (Retio2_ID != 0)
            {
                EditRotio(Retio2_ID);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddRotio(Elm_ID2);
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
        private void AddRotio(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new DeficitRation { ElmId = Element_ID,  DeficitRatioOrderDate = txtOrderDate.SelectedDate,  DeficitRatioOrderImage = Conv.getJPGFromImageControl(Immg),  DeficitRatioPercentage = int.Parse(txtPercentage.Text),  DeficitRatioOrderResult = txtOrderResult.Text,  OrderNum = txtOrderNumber.Text};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة نسبة عجز  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditRotio(int Rat_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                  var ss = HR.DeficitRations.First(a => a.DeficitRatio == Rat_ID);
                  ss.DeficitRatioOrderDate = txtOrderDate.SelectedDate;
                  ss.DeficitRatioOrderResult = txtOrderResult.Text;
                  ss.DeficitRatioPercentage = int.Parse(txtPercentage.Text);
                  ss.OrderNum = txtOrderNumber.Text;
                  if (Immg != null) { ss.DeficitRatioOrderImage = Conv.getJPGFromImageControl(Immg); }
                  HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل نسبة العجز رقم :" + Retio2_ID.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
                MessageBox.Show("تم التعديل بنجاح !!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
