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

namespace PoliceHR.Views.GeneralDocs
{
    /// <summary>
    /// Interaction logic for GenDocAddEdit.xaml
    /// </summary>
    public partial class GenDocAddEdit : Window
    {
        int DocID2;
        ConvertImage Conv = new ConvertImage();
        public GenDocAddEdit(int DocID)
        {
            InitializeComponent();
            DocID2 = DocID;
            if (DocID2 != 0)
            {
                try
                {
                    using (SubjectivityContext HR = new SubjectivityContext())
                    {
                        var table = HR.Docs;
                        var searchresult = table.Where(c => c.DocId.Equals(DocID));
                        foreach (var ss in searchresult)
                        {
                            txtDocDuration.Text = ss.DocInfo;
                            txtDocName.Text = ss.DocName;
                            ImgElementImage.Source = Conv.ConvertByteArrayToBitMapImage(ss.DocImage);
                            Immg = Conv.ConvertByteArrayToBitMapImage(ss.DocImage);
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

            CloseButton.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FF222831");
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
                        _ = MessageBox.Show("هناك حقول فارغة يرجى ملئها أولا ثم اعد المحاولة", "لا يمكن الاضافة", MessageBoxButton.OK, MessageBoxImage.Warning);
                        this.IsEnabled = true;
                        return;
                    }
                }
            }
            if (DocID2 != 0)
            {
                EditDoc(DocID2);
                this.IsEnabled = true;
                this.Close();
            }
            else
            {
                AddDoc();
                this.IsEnabled = true;
                foreach (object ctl in GrdTexts.Children)
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
        private void AddDoc()
        {
            try
            {
                using (SubjectivityContext HR = new SubjectivityContext())
                {
                    object SS = new Doc { DocName = txtDocName.Text, DocImage = Conv.getJPGFromImageControl(Immg),DocInfo = txtDocDuration.Text};
                    _ = HR.Add(SS);
                    _ = HR.SaveChanges();
                    this.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
                this.IsEnabled = true;
            }
        }
        private void EditDoc(int Doc_ID)
        {
            try
            {
                using (var HR = new SubjectivityContext())
                {
                    var ss = HR.Docs.First(a => a.DocId == Doc_ID);
                   ss.DocInfo = txtDocDuration.Text;
                   ss.DocName = txtDocName.Text;
                    if (Immg != null) { ss.DocImage = Conv.getJPGFromImageControl(Immg); }
                    _ = HR.SaveChanges();
                }
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
