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

namespace PoliceHR.Views.Convalescencess
{
    /// <summary>
    /// Interaction logic for ConvalescencesAddEdit.xaml
    /// </summary>
    public partial class ConvalescencesAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        ConvertImage Conv = new ConvertImage();
        int Elm_ID2, Convc_ID2;
        public ConvalescencesAddEdit(int Elm_ID,int Convc_ID)
        {
            InitializeComponent();
            Convc_ID2 = Convc_ID;
            Elm_ID2 = Elm_ID;
            if (Convc_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Convalescences;
                        var searchresult = table.Where(c => c.ConvalescenceId.Equals(Convc_ID));
                        foreach (var ss in searchresult)
                        {
                            if (ss.IsReview == true) { txtIsReview.Text = "نعم"; } else { txtIsReview.Text = "لا"; };
                            txtStartDate.SelectedDate = ss.StartDate;
                            txtsEndDate.SelectedDate = ss.EndDate;
                            txtReason.Text = ss.Reason;
                            txtMedicReport.Text = ss.MedicReport;
                            Elm_ID2 = (int)ss.ElmId;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.MedicReportImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.MedicReportImage);
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
            if (Convc_ID2 != 0)
            {
                EditConvc(Convc_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddConvc(Elm_ID2);
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
        private void AddConvc(int Element_ID)
        {
            bool IsRev = false;
            if (txtIsReview.Text == "نعم") { IsRev = true; }
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Convalescence { ElmId = Element_ID, Reason = txtReason.Text, MedicReportImage = Conv.getJPGFromImageControl(Immg),  StartDate = txtStartDate.SelectedDate,  EndDate = txtsEndDate.SelectedDate,  IsReview = IsRev,  MedicReport = txtMedicReport.Text };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة النقاهة " +  " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditConvc(int Convecs_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Convalescences.First(a => a.ConvalescenceId == Convecs_ID);

                   ss.StartDate = txtStartDate.SelectedDate ;
                   ss.EndDate = txtsEndDate.SelectedDate ;
                   ss.Reason = txtReason.Text;
                   ss.MedicReport = txtMedicReport.Text;
                    if (txtIsReview.Text == "نعم") { ss.IsReview = true; } else { ss.IsReview = false; }
                    if (Immg != null) { ss.MedicReportImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل النقاهة رقم :" + Convc_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
