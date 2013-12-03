using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Viewmodel
{
    //dit is de viemodel klasse die hoort bij de view 'homepage'
    class HomePageVM : ObservableObject, IPage
    {


        public string Name
        {
            get { return "HomePage"; } // unieke naam!
        }
    }
}
