using Project.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    class TicketType : ObservableObject
    {
        public String ID { get; set; }
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
                OnPropertyChanged("Name");
            }
        }
        private int _price;
        public int Price 
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }
        public int AvailableTickets { get; set; }


        public override string ToString()
        {
            return this.ID+ "\t" +this.Name+ "\t" +this.Price;
        }
    }
}
