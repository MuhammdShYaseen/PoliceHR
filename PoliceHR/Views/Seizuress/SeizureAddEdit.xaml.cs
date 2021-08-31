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

namespace PoliceHR.Views.Seizuress
{
    /// <summary>
    /// Interaction logic for SeizureAddEdit.xaml
    /// </summary>
    public partial class SeizureAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int Seizure_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public SeizureAddEdit(int Elm_ID,int Seizure_ID)
        {
            InitializeComponent();
            Seizure_ID2= Seizure_ID;
            Elm_ID2 = Elm_ID;
            if (Seizure_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Seizures;
                        var searchresult = table.Where(c => c.SeizureId.Equals(Seizure_ID));
                        foreach (var ss in searchresult)
                        {
                            txtConclusion.Text = ss.Conclusion;
                            txtSeizureDate.SelectedDate = ss.SeizureDate;
                            txtSeizureNumber.Text = ss.SeizureNumber;
                            txtSeizureUnit.Text = ss.SeizureUnit;
                            txtSeizureWriter.Text = ss.SeizureWriter;
                            Elm_ID2 = (int)ss.ElmId;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.SeizureImage);
                            Immg= Conv.ConvertByteArrayToBitMapImage(ss.SeizureImage);
                        }
                    }
                    Rec.AddActiveRecc("تعديل الضبط رقم :" + Seizure_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
            if (Seizure_ID2 != 0)
            {
                EditSeizure(Seizure_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddSeizure(Elm_ID2);
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
        private void AddSeizure(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Seizure { ElmId = Element_ID,  Conclusion = txtConclusion.Text, SeizureImage = Conv.getJPGFromImageControl(Immg), SeizureDate = txtSeizureDate.SelectedDate,  SeizureNumber = txtSeizureNumber.Text,  SeizureUnit = txtSeizureUnit.Text,  SeizureWriter = txtSeizureWriter.Text };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة الضبط رقم :"  + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditSeizure(int Seizuree_ID )
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Seizures.First(a => a.SeizureId == Seizuree_ID);

                   ss.Conclusion = txtConclusion.Text;
                   ss.SeizureDate = txtSeizureDate.SelectedDate;
                   ss.SeizureNumber = txtSeizureNumber.Text;
                   ss.SeizureUnit = txtSeizureUnit.Text;
                   ss.SeizureWriter = txtSeizureWriter.Text;
                   if (Immg != null) { ss.SeizureImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                MessageBox.Show("تم التعديل بنجاح !!");
                Rec.AddActiveRecc("تعديل الضبط رقم :" + Seizuree_ID.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
