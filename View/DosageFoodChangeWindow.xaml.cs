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
    /// Логика взаимодействия для DosageFoodChangeWindow.xaml
    /// </summary>
    public partial class DosageFoodChangeWindow : Window
    {
        private DosageFood _dosageFood;
        public DosageFoodChangeWindow(DosageFood dosageFood)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows)
            {
                if (item is DosageFoodWindow)
                {
                    this.Owner = item as Window;
                }
            }

            if (dosageFood is null)
            {
                _dosageFood = dosageFood = new DosageFood();
            }
            else
            {
                _dosageFood = dosageFood;
            }
            this.DataContext = _dosageFood;
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
                    db.DosageFood.AddOrUpdate(_dosageFood);
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

            if (_dosageFood != null)
            {
                if (string.IsNullOrEmpty(_dosageFood.DosageQuantity))
                {
                    errors.AppendLine("Поле Дозировка корма не может быть пустым!");
                }
            }
            return errors;
        }
    
    }
}
