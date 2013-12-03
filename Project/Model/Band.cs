﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    class Band
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Picture { get; set; }
        public String Description { get; set; }
        public String Twitter { get; set; }
        public String Facebook { get; set; }
        public Genre Genre { get; set; }
    }
}
