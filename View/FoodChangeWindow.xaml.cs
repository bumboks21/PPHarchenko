using SchoolApplication.DbEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для FoodChangeWindow.xaml
    /// </summary>
    public partial class FoodChangeWindow : Window
    {
        private Food _food;
        public FoodChangeWindow(Food food)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows)
            {
                if (item is FoodWindow)
                {
                    this.Owner = item as Window;
                }
            }

            if (food is null)
            {
                _food = food = new Food();
            }
            else
            {
                _food = food;
            }
            this.DataContext = _food;
        }


        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new HorseSchoolPPEntities())
            {
                try
                {

                    var validateRes = ValidateEntity();
                    if (validateRes.Length > 0)
                    {
                        MessageBox.Show(validateRes.ToString(), "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    db.Food.AddOrUpdate(_food);
                    db.SaveChanges();
                    MessageBox.Show("данные успешно сохранены", "успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    (Owner as ApplicationWindow)?.Refresh();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private StringBuilder ValidateEntity()
        {
            var errors = new StringBuilder();

            if (_food != null)
            {
                if (string.IsNullOrEmpty(_food.FoodName))
                {
                    errors.AppendLine("Поле Название корма не может быть пустым!");
                }
            }
            return errors;
        }
    }
}
