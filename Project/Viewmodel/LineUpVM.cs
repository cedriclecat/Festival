using GalaSoft.MvvmLight.Command;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project.Viewmodel
{
    class LineUpVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "LineUp"; } // unieke naam!
        }

        public LineUpVM()
        {
            _lineUps = LineUp.GetLineUp();
            _genres = LineUp.GetGenres();
            _bands = LineUp.GetBands(_genres);
            _stages = LineUp.GetStage();
            CreateFilterCommand();
        }

        private ObservableCollection<LineUp> _lineUps;
        public ObservableCollection<LineUp> LineUps
        {
            get
            {
                return _lineUps;
            }
            set
            {
                _lineUps = value;
                OnPropertyChanged("LineUps");
            }
        }

        private LineUp _selection;
        public LineUp Selection
        {
            get
            {
                return _selection;
            }
            set
            {
                //selection van comboboxes
                _selection = value;
                OnPropertyChanged("Selection");
                

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
            }
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
            }
        }

        public ICommand FilterCommand
        {
            get;
            internal set;
        }
        public bool CanxecuteFilterCommand(SelectionChangedEventArgs par)
        {
            if (par != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ExcecuteFilterCommand(SelectionChangedEventArgs par)
        {
            Genre selectedGenre = (Genre)par.AddedItems[0];
            ObservableCollection<LineUp> temp = new ObservableCollection<LineUp>();
            foreach (LineUp line in _lineUps)
            {
                if (selectedGenre.Name == line.Band.Genre.Name)
                {
                    temp.Add(line);
                }
            }
           
            _lineUps.Clear();
            LineUps = temp;
        }
        public void CreateFilterCommand()
        {
            FilterCommand = new RelayCommand<SelectionChangedEventArgs>(ExcecuteFilterCommand, CanxecuteFilterCommand);
        }
    }
}
