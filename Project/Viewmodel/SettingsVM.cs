using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Viewmodel
{
    class SettingsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Settings"; } // unieke naam!
        }
    }
}
