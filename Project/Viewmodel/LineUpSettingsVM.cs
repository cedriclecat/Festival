using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Viewmodel
{
    class LineUpSettingsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "LineUpSettings"; } // unieke naam!
        }
         public LineUpSettingsVM()
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

         private LineUp _geselecteerdeperformer;
         public LineUp GeselecteerderPerformer
         {
             get
             {
                 return _geselecteerdeperformer;
             }
             set
             {
                 _geselecteerdeperformer = value;
                 OnPropertyChanged("GeselecteerderPerformer");
             }
         }
    }
}
