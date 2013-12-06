using GalaSoft.MvvmLight.Command;
using Project.Model;
using Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            _ticketTypes = Ticket.GetTicketType();
            CreateAddCommand();
            CreateSaveCommand();
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

        private ObservableCollection<TicketType> _ticketTypes;
        public ObservableCollection<TicketType> TicketTypes
        {
            get
            {
                return _ticketTypes;
            }
            set
            {
                _ticketTypes = value;
                OnPropertyChanged("TicketTypes");
            }
        }
        public ICommand SaveCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteSaveCommand()
        {

            if (GeselecteerdTicket != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateSaveCommand()
        {
            SaveCommand = new RelayCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
        }

        public void ExecuteSaveCommand()
        {
            //hier komt de method
            Ticket.SaveTicket(GeselecteerdTicket);
        }

        public ICommand AddCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteAddCommand(object[] param)
        {
            return true;
        }
        private void CreateAddCommand()
        {
            AddCommand = new RelayCommand<object[]>(ExecuteAddCommand, CanExecuteAddCommand);
            //object[] ipv object omdat het multibinding is
        }

        public void ExecuteAddCommand(object[] param)
        {
            //hier komt de method
            //nog aan te passen

            Ticket temp = new Ticket();

            Ticket.AddTicket(temp);
            Tickets = Ticket.GetTicketPersons();          
            
        }
    }
}
