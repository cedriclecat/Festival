using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    class ContactpersonType
    {
         
        public int ID { get; set; }
        private String _name;
        public String Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        

        public override string ToString()
        {
            return this.ID + "\t" + this.Name;
        }
    }
}
