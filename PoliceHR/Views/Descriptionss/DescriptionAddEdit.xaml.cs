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

namespace PoliceHR.Views.Descriptionss
{
    /// <summary>
    /// Interaction logic for DescriptionAddEdit.xaml
    /// </summary>
    public partial class DescriptionAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        ConvertImage Conv = new ConvertImage();
        int Elm_ID2, Des_ID2;
        public DescriptionAddEdit(int Elm_ID, int Des_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            Des_ID2 = Des_ID;
            if (Des_ID != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.Descriptions;
                        var searchresult = table.Where(c => c.DescriptionsId.Equals(Des_ID));
                        foreach (var ss in searchresult)
                        {
                            txtDescriptionOfficer.Text = ss.DescriptionOfficer;
                            txtDescriptionDate.SelectedDate = ss.DescriptionDate;
                            txtAppearanceValue.SelectedItem = txtAppearanceValue.Items[(int)ss.AppearanceValue];
                            txtPhysicalValue.SelectedItem = txtPhysicalValue.Items[(int)ss.PhysicalValue];
                            txtFunctionalValue.SelectedItem = txtFunctionalValue.Items[(int)ss.FunctionalValue];
                            txtEthicsValue.SelectedItem = txtEthicsValue.Items[(int)ss.EthicsValue];
                            txtSpecialsValue.Text = ss.SpecialsValue.ToString();
                            txtOthers.Text = ss.Others;
                            Elm_ID2 = (int)ss.ElmId;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.DescriptionImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.DescriptionImage);
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
            if (Des_ID2 != 0)
            {
                EditDes(Des_ID2);
                this.IsEnabled = true;
                this.Close();

            }
            else
            {
                AddDes(Elm_ID2);
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
        private void AddDes(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Description { ElmId = Element_ID,  DescriptionOfficer = txtDescriptionOfficer.Text, DescriptionImage = Conv.getJPGFromImageControl(Immg), DescriptionDate = txtDescriptionDate.SelectedDate, EthicsValue = txtEthicsValue.SelectedIndex, AppearanceValue = txtAppearanceValue.SelectedIndex, FunctionalValue = txtFunctionalValue.SelectedIndex,  PhysicalValue = txtPhysicalValue.SelectedIndex, SpecialsValue = txtSpecialsValue.Text, Others = txtOthers.Text };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة الوصف رقم :"+ " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditDes(int DES_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Descriptions.First(a => a.DescriptionsId == DES_ID);

                   ss.DescriptionOfficer = txtDescriptionOfficer.Text;
                   ss.DescriptionDate = txtDescriptionDate.SelectedDate;
                   ss.AppearanceValue = txtAppearanceValue.SelectedIndex;
                   ss.PhysicalValue = txtPhysicalValue.SelectedIndex;
                   ss.FunctionalValue = txtFunctionalValue.SelectedIndex;
                   ss.EthicsValue = txtEthicsValue.SelectedIndex;
                   ss.SpecialsValue = txtSpecialsValue.Text ;
                   ss.Others = txtOthers.Text ;
                    
                   if (Immg != null) { ss.DescriptionImage = Conv.getJPGFromImageControl(Immg); }
                   HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل الوصف رقم :" + Des_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
