using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Project.Model;
using Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            CreatePrintCommand();
            CreateSearchCommand();
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
            if (GeselecteerdeTicketType != null)
            {
                GeselecteerdTicket.TicketType = GeselecteerdeTicketType;
            }
            else
            {                
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


        public ICommand PrintCommand
        {
            get;
            internal set;
        }
        public bool CanExecutePrintCommand()
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
        private void CreatePrintCommand()
        {
            PrintCommand = new RelayCommand(ExecutePrintCommand, CanExecutePrintCommand);
            //object[] ipv object omdat het multibinding is
        }

        public void ExecutePrintCommand()
        {
            PrintTicket(GeselecteerdTicket);
        }

        public void PrintTicket(Ticket printticket)
        {
            

              string filename = "../../../printouts/"+GeselecteerdTicket.Ticketholder + ".docx";
              File.Copy("template.docx", filename, true);
              WordprocessingDocument newdow = WordprocessingDocument.Open(filename, true);
              Dictionary<string, BookmarkStart> bookmarks = new Dictionary<string, BookmarkStart>();
              foreach (BookmarkStart bms in newdow.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
              {
                  bookmarks[bms.Name] = bms;
              }
              int totalprice = GeselecteerdTicket.Amount * GeselecteerdTicket.TicketType.Price;
              //bookmarks["Name"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(GeselecteerdTicket.Ticketholder)), bookmarks["Naam"]);
              bookmarks["Name"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(GeselecteerdTicket.Ticketholder)), bookmarks["Name"]);
              bookmarks["Email"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(GeselecteerdTicket.TicketHolderEmail)), bookmarks["Email"]);
              bookmarks["TicketType"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(GeselecteerdTicket.TicketType.Name)), bookmarks["TicketType"]);
              bookmarks["Amount"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(GeselecteerdTicket.Amount.ToString())), bookmarks["Amount"]);
              bookmarks["Price"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(totalprice.ToString())), bookmarks["Price"]);

              newdow.Close();
            }

        public ICommand SearchCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteSearchCommand(object param)
        {
            return true;
        }
        private void CreateSearchCommand()
        {
            SearchCommand = new RelayCommand<object>(ExecuteSearchCommand, CanExecuteSearchCommand);
            //object[] ipv object omdat het multibinding is
        }

        public void ExecuteSearchCommand(object param)
        {
            ObservableCollection<Ticket> Tickets = Ticket.GetTicketPersons();
            ObservableCollection<Ticket> temp = new ObservableCollection<Ticket>();
            string search = param.ToString();
            foreach(Ticket singleticket in Tickets)
            {
                string[] names = singleticket.Ticketholder.Split(' ');
                if (search == names[0] || search == names[1] || search == singleticket.TicketHolderEmail)
                {
                    temp.Add(singleticket);
                }
            }

            _tickets.Clear();
            _tickets = temp;
            Tickets = temp;
        }
        
    }
}
