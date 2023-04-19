using SchoolApplication.ViewModel;
using System;
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
    /// Логика взаимодействия для SelectWindow.xaml
    /// </summary>
    public partial class SelectWindow : Window
    {
        public SelectWindow()
        {
            InitializeComponent();

            this.DataContext = new ViewModel.AuthVM();
        }


        private void Horse_Button_Click(object sender, RoutedEventArgs e)
        {
            var horseWindow = new HorseWindow();

            horseWindow.Show();
        }

        private void Sportsman_Button_Click(object sender, RoutedEventArgs e)
        {
            var app1Window = new ApplicationWindow();

            app1Window.Show();
        }

        private void Coach_Button_Click(object sender, RoutedEventArgs e)
        {
            var coachWindow = new CoachWindow();

            coachWindow.Show();
        }

        private void InfOfGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            var groupWindow = new GroupWindow();

            groupWindow.Show();
        }

        private void Training_Button_Click(object sender, RoutedEventArgs e)
        {
            var trainingWindow = new TrainingWindow();

            trainingWindow.Show();
        }

        private void Feeding_Button_Click(object sender, RoutedEventArgs e)
        {
            var feedingWindow = new FeedingWindow();

            feedingWindow.Show();
        }

        private void DosageFood_Button_Click(object sender, RoutedEventArgs e)
        {
            var dosagefoodWindow = new DosageFoodWindow();

            dosagefoodWindow.Show();
        }

        private void Food_Button_Click(object sender, RoutedEventArgs e)
        {
            var foodWindow = new FoodWindow();

            foodWindow.Show();
        }
    }
}
