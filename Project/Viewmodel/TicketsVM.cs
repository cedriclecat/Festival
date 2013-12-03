using Project.Model;
using Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Viewmodel
{
    class TicketsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Tickets"; } // unieke naam!
        }

        public TicketsVM()
        {
            //data ophalen
            _tickets = Ticket.GetTicketPersons();
        }

        private ObservableCollection<Ticket> _tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get
            {
                return _tickets;
            }
            set
            {
                _tickets = value;
            }
        }

        private Ticket _geselecteerdTicket;
        public Ticket GeselecteerdTicket
        {
            get
            {
                return _geselecteerdTicket;
            }
            set
            {

                _geselecteerdTicket = value;
                OnPropertyChanged("GeselecteerdTicket");
               
            }
        }
    }
}
