using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
        }

        private Contactperson _selection;
        public Contactperson Selection
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                OnPropertyChanged("Selection");
                // OnPropertyChanged("Contacts");

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
    }
}
