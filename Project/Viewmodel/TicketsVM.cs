using GalaSoft.MvvmLight.Command;
using Project.Model;
using Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            CreateDeleteCommand();
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
                OnPropertyChanged("Tickets");
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

        private TicketType _geselecteerdeTicketType;
        public TicketType GeselecteerdeTicketType
        {
            get
            {
                return _geselecteerdeTicketType;
            }
            set
            {
                _geselecteerdeTicketType = value;
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
            if (GeselecteerdeTicketType == null)
            {
                MessageBox.Show("Please select a TicketType");
            }
            else
            {
                GeselecteerdTicket.TicketType = GeselecteerdeTicketType;
                Ticket.SaveTicket(GeselecteerdTicket);
                Tickets = Ticket.GetTicketPersons();
            }
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
            if (GeselecteerdeTicketType == null)
            {
                MessageBox.Show("Please select a TicketType");
            }
            else
            {
                Ticket temp = new Ticket();
                temp.Ticketholder = param[0].ToString();
                temp.TicketHolderEmail = param[1].ToString();
                temp.Amount = int.Parse(param[2].ToString());
                temp.Day1 = Convert.ToBoolean(param[3]);
                temp.Day2 = Convert.ToBoolean(param[4]);
                temp.Day3 = Convert.ToBoolean(param[5]);
                temp.TicketType = GeselecteerdeTicketType;

                Ticket.AddTicket(temp);
                Tickets = Ticket.GetTicketPersons();
            }
        }

        public ICommand DeleteCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteDeleteCommand()
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
        private void CreateDeleteCommand()
        {
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            //object[] ipv object omdat het multibinding is
        }

        public void ExecuteDeleteCommand()
        {

            Ticket.DeleteTicket(GeselecteerdTicket);
            Tickets = Ticket.GetTicketPersons();
        }
    }
}
