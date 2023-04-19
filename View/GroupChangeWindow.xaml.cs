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
    /// Логика взаимодействия для GroupChangeWindow.xaml
    /// </summary>
    public partial class GroupChangeWindow : Window
    {
        private InfoGroup _infoGroup;
        public GroupChangeWindow(InfoGroup infoGroup)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows)
            {
                if (item is GroupWindow)
                {
                    this.Owner = item as Window;
                }
            }

            if (infoGroup is null)
            {
                _infoGroup = infoGroup = new InfoGroup();
            }
            else
            {
                _infoGroup = infoGroup;
            }
            this.DataContext = _infoGroup;
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
                    db.InfoGroup.AddOrUpdate(_infoGroup);
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

            if (_infoGroup != null)

            {
                if (_infoGroup.GroupID <= 0)
                {
                    errors.AppendLine("Поле ID Группы не может быть пустым");
                }
                if (_infoGroup.SportsmanID <= 0)
                {
                    errors.AppendLine("Поле ID Спортсмена корма не может быть пустым");
                }
                if (_infoGroup.HorseID <= 0)
                {
                    errors.AppendLine("Поле ID Лошади не может быть пустым");
                }
                if (_infoGroup.DivisionOfTheGroupID <= 0)
                {
                    errors.AppendLine("Поле ID Подразделения группы не может быть пустым");
                }

            }
            return errors;
        }
    }
}
