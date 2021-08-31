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

namespace PoliceHR.Views.PoliceStatess
{
    /// <summary>
    /// Interaction logic for PliceStateAddEdit.xaml
    /// </summary>
    public partial class PliceStateAddEdit : Window
    {
        AddActiveRec Rec = new AddActiveRec();
        int PoliceState_ID2;
        int Elm_ID2;
        ConvertImage Conv = new ConvertImage();
        public PliceStateAddEdit(int Elm_ID,int PoliceState_ID)
        {
            InitializeComponent();
            PoliceState_ID2 = PoliceState_ID;
            Elm_ID2 = Elm_ID;
            if (PoliceState_ID2 != 0)
            {
                try
                {
                    using (var HR = new SubjectivityContext())
                    {
                        var table = HR.PoliceStates;
                        var searchresult = table.Where(c => c.PoliceStateId.Equals(PoliceState_ID));
                        foreach (var ss in searchresult)
                        {
                           
                            txtSeizureDate.SelectedDate = ss.SeizureDate;
                            txtSeizureNumber.Text = ss.SeizureNumber;
                            txtResult.Text = ss.Result;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.StateImage);
                            txtOrderDate.SelectedDate = ss.OrderDate;
                            txtOrderNumber.Text = ss.OrderNum;
                            Elm_ID2 = (int)ss.ElmId;
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.StateImage);
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
            if (PoliceState_ID2 != 0)
            {
                EditPoliceState(PoliceState_ID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddPoliceState(Elm_ID2);
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
        private void AddPoliceState(int Element_ID)
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new PoliceState { ElmId = Element_ID,  StateImage = Conv.getJPGFromImageControl(Immg), SeizureDate = txtSeizureDate.SelectedDate, SeizureNumber = txtSeizureNumber.Text, Result=txtResult.Text,OrderNum=txtOrderNumber.Text,OrderDate=txtOrderDate.SelectedDate };
                    HR.Add(SS);
                    HR.SaveChanges();
                    this.IsEnabled = true;
                }
                Rec.AddActiveRecc("اضافة ملاحقة مسلكية :"  + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditPoliceState(int State_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.PoliceStates.First(a => a.PoliceStateId == State_ID);

                    ss.Result = txtResult.Text;
                    ss.SeizureDate = txtSeizureDate.SelectedDate;
                    ss.SeizureNumber = txtSeizureNumber.Text;
                    ss.OrderDate = txtOrderDate.SelectedDate;
                    ss.OrderNum = txtOrderNumber.Text;
                    if (Immg != null) { ss.StateImage = Conv.getJPGFromImageControl(Immg); }
                    HR.SaveChanges();
                }
                Rec.AddActiveRecc("تعديل ملاحقة مسلكية رقم :" + PoliceState_ID2.ToString() + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
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
