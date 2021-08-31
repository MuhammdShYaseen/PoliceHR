﻿using PoliceHR.Models;
using PoliceHR.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PoliceHR.Views.Convalescencess
{
    /// <summary>
    /// Interaction logic for ConvalescencesView.xaml
    /// </summary>
    public partial class ConvalescencesView : Page
    {
        int Elm_ID2;
        public ConvalescencesView(int Elm_ID)
        {
            InitializeComponent();
            Elm_ID2 = Elm_ID;
            try
            {
                var HR = new SubjectivityContext();
                var table = HR.Convalescences;
                var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID));
                GrdCovc.ItemsSource = searchresult.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDataGrid P = new PrintDataGrid();
            P.PrintData(GrdCovc, DokGrid, Elm_ID2, txtName);
            AddActiveRec Rec = new AddActiveRec();
            Rec.AddActiveRecc("طباعة نقاهات  :" + " للعنصر : " + Rec.ELmMNimber(Elm_ID2));
        }
        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var SS = new ConvalescencesAddEdit(Elm_ID2, 0);
            SS.Show();
        }

        private void GrdCovc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Convc_ID != 0 || Convc_ID != 0)
            {
                var SS = new ConvalescencesAddEdit(0, Convc_ID);
                SS.Show();
            }
        }
        int Convc_ID;
        private void GrdCovc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int str = 0;
            if (GrdCovc.SelectedItems.Count > 0)
            {
                Convalescence member = new Convalescence();
                foreach (var obj in GrdCovc.SelectedItems)
                {
                    member = obj as Convalescence;
                    str = member.ConvalescenceId;
                }
            }
            else
            {

            }
            Convc_ID = str;
        }
        private void MIDelete_Click(object sender, RoutedEventArgs e)
        {
            CheckUserRole Rolee = new CheckUserRole();
            if (Rolee.CheckUserRolee() == 4 || Rolee.CheckUserRolee() == 3)
            {
                MessageBox.Show("لا تمتلك الصلاحيات اللازمة للقيام بهذا العمل", "الصلاحيات", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (SubjectivityContext ss = new SubjectivityContext())
            {
                if (ss.Convalescences.Count() != 0)
                {
                    if (MessageBox.Show("هل تريد فعلا الحذف ؟؟ لايمكن التراجع عن ذلك مستقبلا !", "Exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Convalescence Del = ss.Convalescences.Find(Convc_ID);
                        ss.Convalescences.Remove(Del);
                        ss.SaveChanges();
                        MessageBox.Show("تم الحذف بنجاح قم باعادة تحميل النافذة لمشاهدة النتائج", "الحذف", MessageBoxButton.OK, MessageBoxImage.Information);
                        var table = ss.Convalescences;
                        var searchresult = table.Where(c => c.ElmId.Equals(Elm_ID2));
                        GrdCovc.ItemsSource = searchresult.ToList();
                    }
                }

            }
        }
    }
}