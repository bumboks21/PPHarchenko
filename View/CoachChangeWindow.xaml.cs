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
    /// Логика взаимодействия для CoachChangeWindow.xaml
    /// </summary>
    public partial class CoachChangeWindow : Window
    {
        private InfoCoach _infoCoach;
        public CoachChangeWindow(InfoCoach infoCoach)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows)
            {
                if (item is CoachWindow)
                {
                    this.Owner = item as Window;
                }
            }

            if (infoCoach is null)
            {
                _infoCoach = infoCoach = new InfoCoach();
            }
            else
            {
                _infoCoach = infoCoach;
            }
            this.DataContext = _infoCoach;
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
                    db.InfoCoach.AddOrUpdate(_infoCoach);
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

            if (_infoCoach != null)
            {
                if (string.IsNullOrEmpty(_infoCoach.CoachSurname))
                {
                    errors.AppendLine("Поле Фамилия тренера не может быть пустым!");
                }
                if (string.IsNullOrEmpty(_infoCoach.CoachName))
                {
                    errors.AppendLine("Поле Имя тренера не может быть пустым!");
                }
                if (string.IsNullOrEmpty(_infoCoach.CoachPatronymic))
                {
                    errors.AppendLine("Поле Отчество тренера не может быть пустым!");
                }
                if (_infoCoach.CoachAge <= 0)
                {
                    errors.AppendLine("Поле Возраст тренера не может быть пустым");
                }
                if (string.IsNullOrEmpty(_infoCoach.CoachEmail))
                {
                    errors.AppendLine("Поле Эл.почта не может быть пустым!");
                }
                if (string.IsNullOrEmpty(_infoCoach.CoachPhone))
                {
                    errors.AppendLine("Поле Телефон тренера не может быть пустым!");
                }
            }
            return errors;
        }
    }
}
