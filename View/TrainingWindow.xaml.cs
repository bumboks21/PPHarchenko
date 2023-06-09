﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SchoolApplication.View
{
    /// <summary>
    /// Логика взаимодействия для TrainingWindow.xaml
    /// </summary>
    public partial class TrainingWindow : Window
    {
        public TrainingWindow()
        {
            InitializeComponent();

            this.DataContext = new ViewModel.AppVM();
        }

        private void BtnAddData_Click(object sender, RoutedEventArgs e)
        {
            var AddWindowsTraining = new TrainingChangeWindow(null);
            AddWindowsTraining.Show();
        }

        private void BtnEditData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
