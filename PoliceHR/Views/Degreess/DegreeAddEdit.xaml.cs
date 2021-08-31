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

namespace PoliceHR.Views.Degreess
{
    /// <summary>
    /// Interaction logic for DegreeAddEdit.xaml
    /// </summary>
    public partial class DegreeAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Degree_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public DegreeAddEdit(int Elm_ID, int Degree_ID)
        {
            InitializeComponent();
            Degree_ID2 = Degree_ID;
            Elm_ID2 = Elm_ID;
            if (Degree_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Dgrees;
                        var searchresult = table.Where(c => c.DgreeId.Equals(Degree_ID));
                        foreach (var ss in searchresult)
                        {
                            txtDgreeName.Text = ss.DgreeName;
                            txtDgreeDuration.Text = ss.DgreeDuration;
                            txtGraduationDate.SelectedDate = ss.GraduationDate;
                            txtStartDate.SelectedDate = ss.StartDate;
                            Elm_ID2 = (int)ss.ElmId;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.CertifiedGraduatedImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.CertifiedGraduatedImage);
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
            if (Degree_ID2 != 0)
            {
                EditDgree(Degree_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddDgree(Elm_ID2);
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
        private void AddDgree(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Dgree { ElmId = Element_ID,  DgreeDuration = txtDgreeDuration.Text, CertifiedGraduatedImage = Conv.getJPGFromImageControl(Immg),  DgreeName = txtDgreeName.Text,  GraduationDate = txtGraduationDate.SelectedDate, StartDate = txtStartDate.SelectedDate};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة شهادة  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditDgree(int Deg_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Dgrees.First(a => a.DgreeId == Deg_ID);
                    ss.DgreeName = txtDgreeName.Text;
                    ss.DgreeDuration = txtDgreeDuration.Text;
                    ss.GraduationDate = txtGraduationDate.SelectedDate;
                    ss.StartDate = txtStartDate.SelectedDate;
                    if (Immg != null) { ss.CertifiedGraduatedImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل شهادة رقم :" + Degree_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
