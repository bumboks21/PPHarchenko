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
    /// Логика взаимодействия для HorseChangeWindow.xaml
    /// </summary>
    public partial class HorseChangeWindow : Window
    {
        private Horse _horse;
        public HorseChangeWindow(Horse horse)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows)
            {
                if (item is HorseWindow)
                {
                    this.Owner = item as Window;
                }
            }

            if (horse is null)
            {
                _horse = horse = new Horse();
            }
            else
            {
                _horse = horse;
            }
            this.DataContext = _horse;
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
                    db.Horse.AddOrUpdate(_horse);
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

            if (_horse != null)
            {
                if (string.IsNullOrEmpty(_horse.HorseName))
                {
                    errors.AppendLine("Поле Имя лошади не может быть пустым!");
                }
                if (_horse.HorseAge <= 0)
                {
                    errors.AppendLine("Поле Возраст лошади не может быть пустым");
                }
                if (_horse.SuitID <= 0)
                {
                    errors.AppendLine("Поле ID масти не может быть пустым");
                }
                if (_horse.BreedID <= 0)
                {
                    errors.AppendLine("Поле ID породы не может быть пустым");
                }
            }
            return errors;
        }
    }
}
