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
    /// Логика взаимодействия для TrainingChangeWindow.xaml
    /// </summary>
    public partial class TrainingChangeWindow : Window
    {
        private Training _training;
        public TrainingChangeWindow(Training training)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows)
            {
                if (item is TrainingWindow)
                {
                    this.Owner = item as Window;
                }
            }

            if (training is null)
            {
                _training = training = new Training();
            }
            else
            {
                _training = training;
            }
            this.DataContext = _training;
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
                    db.Training.AddOrUpdate(_training);
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

            if (_training != null)
            {
                if (_training.GroupID <= 0)
                {
                    errors.AppendLine("Поле ID группы не может быть пустым");
                }
                if (_training.SpaceID <= 0)
                {
                    errors.AppendLine("Поле ID помещения не может быть пустым");
                }
                if (string.IsNullOrEmpty(_training.TimeTraining))
                {
                    errors.AppendLine("Поле Время тренировки не может быть пустым!");
                }
                
            }
            return errors;
        }
    }
}
