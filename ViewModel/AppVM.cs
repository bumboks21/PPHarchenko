﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SchoolApplication.DbEntity;

namespace SchoolApplication.ViewModel
{
    public class AppVM : BaseVM
    {
        private InfoSportsman _selectedItem;
        private ObservableCollection<InfoSportsman> _infoSportsman;
        private ObservableCollection<InfoCoach> _infoCoach;
        private ObservableCollection<InfoGroup> _infoGroup;
        private ObservableCollection<DosageFood> _dosageFood;
        private ObservableCollection<Feeding> _feeding;
        private ObservableCollection<Food> _food;
        private ObservableCollection<Horse> _horse;
        private ObservableCollection<Training> _training;

        public ObservableCollection<InfoSportsman> InfoSportsman
        {
            get => _infoSportsman;
            set
            {
                _infoSportsman = value;
                OnPropertyChanged(nameof(InfoSportsman));
            }
        }

        public ObservableCollection<InfoCoach> InfoCoach
        {
            get => _infoCoach;
            set
            {
                _infoCoach = value;
                OnPropertyChanged(nameof(InfoCoach));
            }
        }

        public ObservableCollection<InfoGroup> InfoGroup
        {
            get => _infoGroup;
            set
            {
                _infoGroup = value;
                OnPropertyChanged(nameof(InfoGroup));
            }
        }

        public ObservableCollection<DosageFood> DosageFood
        {
            get => _dosageFood;
            set
            {
                _dosageFood = value;
                OnPropertyChanged(nameof(DosageFood));
            }
        }

        public ObservableCollection<Feeding> Feeding
        {
            get => _feeding;
            set
            {
                _feeding = value;
                OnPropertyChanged(nameof(Feeding));
            }
        }

        public ObservableCollection<Food> Food
        {
            get => _food;
            set
            {
                _food = value;
                OnPropertyChanged(nameof(Food));
            }
        }

        public ObservableCollection<Horse> Horse
        {
            get => _horse;
            set
            {
                _horse = value;
                OnPropertyChanged(nameof(Horse));
            }
        }

        public ObservableCollection<Training> Training
        {
            get => _training;
            set
            {
                _training = value;
                OnPropertyChanged(nameof(Training));
            }
        }

        public InfoSportsman SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public AppVM()
        {
            InfoSportsman = new ObservableCollection<InfoSportsman>();
            InfoCoach = new ObservableCollection<InfoCoach>();
            InfoGroup = new ObservableCollection<InfoGroup>();
            DosageFood = new ObservableCollection<DosageFood>();
            Feeding = new ObservableCollection<Feeding>();
            Food = new ObservableCollection<Food>();
            Horse = new ObservableCollection<Horse>();
            Training = new ObservableCollection<Training>();

            LoadData();
        }

        public void LoadData()
        {
            if (InfoSportsman.Count != 0)
            {
                InfoSportsman.Clear();
            }

            var result = DbStorage.DB_s.InfoSportsman.ToList();
            result.ForEach(elem => InfoSportsman?.Add(elem));
            if (InfoCoach.Count != 0)
            {
                InfoCoach.Clear();
            }

            var result1 = DbStorage.DB_s.InfoCoach.ToList();
            result1.ForEach(elem => InfoCoach?.Add(elem));
            if (InfoGroup.Count != 0)
            {
                InfoGroup.Clear();
            }

            var result2 = DbStorage.DB_s.InfoGroup.ToList();
            result2.ForEach(elem => InfoGroup?.Add(elem));
            if (DosageFood.Count != 0)
            {
                DosageFood.Clear();
            }

            var result3 = DbStorage.DB_s.DosageFood.ToList();
            result3.ForEach(elem => DosageFood?.Add(elem));
            if (Feeding.Count != 0)
            {
                Feeding.Clear();
            }

            var result4 = DbStorage.DB_s.Feeding.ToList();
            result4.ForEach(elem => Feeding?.Add(elem));
            if (Food.Count != 0)
            {
                Food.Clear();
            }

            var result5 = DbStorage.DB_s.Food.ToList();
            result5.ForEach(elem => Food?.Add(elem));
            if (Horse.Count != 0)
            {
                Horse.Clear();
            }

            var result6 = DbStorage.DB_s.Horse.ToList();
            result6.ForEach(elem => Horse?.Add(elem));
            if (Training.Count != 0)
            {
                Training.Clear();
            }

            var result7 = DbStorage.DB_s.Training.ToList();
            result7.ForEach(elem => Training?.Add(elem));

        }

        public void DeleteSelectedItem()
        {
            if (!(SelectedItem is null))
            {
                using (var db = new HorseSchoolPPEntities())
                {
                    var result = MessageBox.Show("Вы действительно хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var ItemForDelete = db.InfoSportsman.Where(elem => elem.SportsmanID == SelectedItem.SportsmanID).FirstOrDefault();
                            db.InfoSportsman.Remove(ItemForDelete);
                            db.SaveChanges();
                            LoadData();
                            MessageBox.Show("Данные успешно удалены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Удаление завершено успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }
    }
}
