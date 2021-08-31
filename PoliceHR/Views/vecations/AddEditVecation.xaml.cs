using PoliceHR.Models;
using PoliceHR.ViewModels;
using PoliceHR.Views.ImagesViewer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

namespace PoliceHR.Views.vecations
{
    /// <summary>
    /// Interaction logic for AddEditVecation.xaml
    /// </summary>
    public partial class AddEditVecation : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Elm_ID2;
        int Vec_ID2;
        ConvertImage Conv = new ConvertImage();
        public AddEditVecation(int Elm_ID,int Vec_ID)
        {
            InitializeComponent();
           // VacationType conv = new VacationType();
            Vec_ID2 = Vec_ID;
            Elm_ID2 = Elm_ID;
            if (Vec_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Vacations;
                        var searchresult = table.Where(c => c.VacationsId.Equals(Vec_ID));
                        foreach (var ss in searchresult)
                        {
                            txtSource.Text = ss.Source;
                            txtVacationsLocation.Text = ss.VacationsLocation;
                            txtVacationsNumber.Text = ss.VacationsNumber;
                            txtEndDate.SelectedDate = ss.EndDate;
                            txtStartDate.SelectedDate = ss.StartDate;
                            txtVacationsDate.SelectedDate = ss.VacationsDate;
                            txtVcType.SelectedItem = txtVcType.Items[(int)ss.VacationType];
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.VacationsImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.VacationsImage);
                        }
                    }

                }
                catch(Exception ex)
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
            if (Vec_ID2 != 0)
            {
                EditVec(Vec_ID2);
                this.IsEnabled = true;
                this.Close();

            }
            else
            {
                AddVec(Elm_ID2);
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
        private void AddVec(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Vacation { ElmId = Element_ID, Source = txtSource.Text, VacationsImage = Conv.getJPGFromImageControl(Immg), VacationsDate = txtVacationsDate.SelectedDate, StartDate = txtStartDate.SelectedDate, EndDate = txtEndDate.SelectedDate, VacationsLocation = txtVacationsLocation.Text, VacationsNumber = txtVacationsNumber.Text,VacationType=txtVcType.SelectedIndex};
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة اجازة  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditVec(int Vecation_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                   var ss = HR.Vacations.First(a => a.VacationsId == Vecation_ID);
                   ss.Source = txtSource.Text;
                   ss.VacationsLocation = txtVacationsLocation.Text;
                   ss.VacationsNumber = txtVacationsNumber.Text;
                   ss.EndDate = txtEndDate.SelectedDate;
                   ss.StartDate = txtStartDate.SelectedDate;
                   ss.VacationsDate = txtVacationsDate.SelectedDate;
                   ss.VacationType = txtVcType.SelectedIndex;
                   if (Immg != null) { ss.VacationsImage = Conv.getJPGFromImageControl(Immg); }
                   HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل اجازة رقم :" + Vecation_ID.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
