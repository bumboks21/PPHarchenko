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
    /// Логика взаимодействия для FeedingChangeWindow.xaml
    /// </summary>
    public partial class FeedingChangeWindow : Window
    {
        private Feeding _feeding;
        public FeedingChangeWindow(Feeding feeding)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows)
            {
                if (item is FeedingWindow)
                {
                    this.Owner = item as Window;
                }
            }

            if (feeding is null)
            {
                _feeding = feeding = new Feeding();
            }
            else
            {
                _feeding = feeding;
            }
            this.DataContext = _feeding;
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
                    db.Feeding.AddOrUpdate(_feeding);
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

            if (_feeding != null)

            {
                if (_feeding.HorseID <= 0)
                {
                    errors.AppendLine("Поле ID Лошади не может быть пустым");
                }
                if (_feeding.DosageFoodID <= 0)
                {
                    errors.AppendLine("Поле ID Дозировки корма не может быть пустым");
                }
                if (_feeding.FoodID <= 0)
                {
                    errors.AppendLine("Поле ID Корма не может быть пустым");
                }
                 
                if (string.IsNullOrEmpty(_feeding.TimeFeeding))
                {
                    errors.AppendLine("Поле Время кормления не может быть пустым!");
                }
            }
            return errors;
        }
    }
}
