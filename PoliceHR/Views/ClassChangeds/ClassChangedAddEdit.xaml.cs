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

namespace PoliceHR.Views.ClassChangeds
{
    /// <summary>
    /// Interaction logic for ClassChangedAddEdit.xaml
    /// </summary>
    public partial class ClassChangedAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        ConvertImage Conv = new ConvertImage();
        int Elm_ID2, Class_ID2;
        public ClassChangedAddEdit(int Elm_ID, int Class_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            Class_ID2 = Class_ID;
            if (Class_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.ClassChanges;
                        var searchresult = table.Where(c => c.ClassChangesId.Equals(Class_ID));
                        foreach (var ss in searchresult)
                        {

                            txtStartDate.SelectedDate = ss.StartDate;
                            txtEndDate.SelectedDate = ss.EndDate;
                            txtClassName.Text = ss.ClassName;
                            txtOrderNumber.Text = ss.OrderNumber;
                            txtOrderDate.SelectedDate = ss.OrderDate;
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
            if (Class_ID2 != 0)
            {
                EditClass(Class_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddClass(Elm_ID2);
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
        private void AddClass(int Element_ID)
        {
            
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new ClassChange { ElmId = Element_ID, ClassName = txtClassName.Text, OrderImage = Conv.getJPGFromImageControl(Immg), StartDate = txtStartDate.SelectedDate, EndDate = txtEndDate.SelectedDate,  OrderNumber = txtOrderNumber.Text,OrderDate=txtOrderDate.SelectedDate };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc(" اضافة صنف :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditClass(int ClssChange_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                   var ss = HR.ClassChanges.First(a => a.ClassChangesId == ClssChange_ID);

                   ss.StartDate = txtStartDate.SelectedDate;
                   ss.EndDate = txtEndDate.SelectedDate;
                   ss.ClassName = txtClassName.Text;
                   ss.OrderNumber = txtOrderNumber.Text;

                    if (Immg != null) { ss.OrderImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc(" تعديل الصنف رقم :" + Class_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
