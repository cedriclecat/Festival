using GalaSoft.MvvmLight.Command;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Viewmodel
{
    class SettingsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Settings"; } // unieke naam!
           
        }

        public SettingsVM()
        {

            CreateSaveStageCommand();
            CreateAddStageCommand();
            CreateAddGenreCommand();
            CreateSaveGenreCommand();
            CreateAddBandCommand();
            CreateSaveBandCommand();

            _stages = LineUp.GetStage();
            _genres = LineUp.GetGenres();
            _bands = LineUp.GetBands(_genres);
           
        }

        private ObservableCollection<Stage> _stages;
        public ObservableCollection<Stage> Stages
        {
            get
            {
                return _stages;
            }
            set
            {
                _stages = value;
                OnPropertyChanged("Stages");
            }
        }

        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres
        {
            get
            {
                return _genres;
            }
            set
            {
                _genres = value;
                OnPropertyChanged("Genres");
            }
        }

        private ObservableCollection<Band> _bands;
        public ObservableCollection<Band> Bands
        {
            get
            {
                return _bands;
            }
            set
            {
                _bands = value;
                OnPropertyChanged("Bands");
            }
        }

        private Stage _geselecteerdeStage;
        public Stage GeselecteerdeStage
        {
            get
            {
                return _geselecteerdeStage;
            }
            set
            {
                _geselecteerdeStage = value;
                OnPropertyChanged("GeselecteerdeStage");
            }
        }

        private Genre _geselecteerdGenre;
        public Genre GeselecteerdGenre
        {
            get
            {
                return _geselecteerdGenre;
            }
            set
            {
                _geselecteerdGenre = value;
                OnPropertyChanged("GeselecteerdGenre");
            }
        }

        private Band _geselecteerdeBand;
        public Band GeselecteerdeBand
        {
            get
            {
                return _geselecteerdeBand;
            }
            set
            {
                _geselecteerdeBand = value;
                OnPropertyChanged("GeselecteerdeBand");
            }
        }

        //button commands
        public ICommand SaveStageCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteSaveStageCommand()
        {

            if (GeselecteerdeStage != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateSaveStageCommand()
        {
            SaveStageCommand = new RelayCommand(ExecuteSaveStageCommand, CanExecuteSaveStageCommand);
        }

        public void ExecuteSaveStageCommand()
        {
            Stage.SaveStage(GeselecteerdeStage);
            Stages = LineUp.GetStage();
        }


        public ICommand AddStageCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteAddStageCommand(object param)
        {
            return true;
        }
        private void CreateAddStageCommand()
        {
            SaveStageCommand = new RelayCommand<object>(ExecuteAddStageCommand, CanExecuteAddStageCommand);
        }

        public void ExecuteAddStageCommand(object param)
        {
            Stage.AddStage(GeselecteerdeStage);
            Stages = LineUp.GetStage();
        }




        public ICommand AddGenreCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteAddGenreCommand()
        {

            if (GeselecteerdeStage != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateAddGenreCommand()
        {
            SaveStageCommand = new RelayCommand(ExecuteAddGenreCommand, CanExecuteAddGenreCommand);
        }

        public void ExecuteAddGenreCommand()
        {
            Genre.AddGenre(GeselecteerdGenre);
            Genres = LineUp.GetGenres();
        }


        public ICommand SaveGenreCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteSaveGenreCommand()
        {

            if (GeselecteerdeStage != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateSaveGenreCommand()
        {
            SaveStageCommand = new RelayCommand(ExecuteSaveGenreCommand, CanExecuteSaveGenreCommand);
        }

        public void ExecuteSaveGenreCommand()
        {
            Genre.SaveGenre(GeselecteerdGenre);
            Genres = LineUp.GetGenres();
        }



        public ICommand SaveBandCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteSaveBandCommand()
        {

            if (GeselecteerdeStage != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateSaveBandCommand()
        {
            SaveStageCommand = new RelayCommand(ExecuteSaveBandCommand, CanExecuteSaveBandCommand);
        }

        public void ExecuteSaveBandCommand()
        {

        }



        public ICommand AddBandCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteAddBandCommand(object[] param)
        {

            if (GeselecteerdeStage != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateAddBandCommand()
        {
            SaveStageCommand = new RelayCommand<object[]>(ExecuteAddBandCommand, CanExecuteAddBandCommand);
        }

        public void ExecuteAddBandCommand(object[] param)
        {

        }
    }
}
