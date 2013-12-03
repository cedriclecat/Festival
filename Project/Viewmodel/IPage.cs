using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Viewmodel
{
    //elke view model-klasse zal deze interface moeten implementeren.
    //zo kan ik later een lijst van objecten van klassen gaan bijhouden waarvan de klasse
    //deze interface implementeerd. Deze lisjt zit in de klasse applicationVM
    interface IPage
    {
        //een property in steken. Elke klass emoet deze property gaan uitwerken.
        string Name { get; }

    }
}
