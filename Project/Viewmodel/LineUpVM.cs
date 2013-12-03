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

       
    }
}
