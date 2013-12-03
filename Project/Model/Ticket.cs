﻿using Labo06_1.Model;
using Project.Viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Project.Model
{
    class Ticket : ObservableObject
    {
        public String ID { get; set; }
        private String _ticketholder;
        public String Ticketholder 
        {
            get
            {
                return _ticketholder;
            }
            set
            {
                _ticketholder = value;
                OnPropertyChanged("Ticketholder");
            }
        
        }
        private String _ticketholderemail;
        public String TicketHolderEmail
        {
            get
            {
                return _ticketholderemail;
            }
            set
            {
                _ticketholderemail = value;
                OnPropertyChanged("TicketHolderEmail");
            }
        }
        private TicketType _ticketType;
        public TicketType TicketType 
        {
            get
            {
                return _ticketType;
            }
            set
            {
                _ticketType = value;
                OnPropertyChanged("TicketType");
            }
        }
        private int _amount;
        public int Amount 
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }
        private bool _day1;
        public bool Day1
        {
            get
            {
                return _day1;
            }
            set
            {
                _day1 = value;
            }
        }
        private bool _day2;
        public bool Day2
        {
            get
            {
                return _day2;
            }
            set
            {
                _day2 = value;
            }
        }
        private bool _day3;
        public bool Day3
        {
            get
            {
                return _day3;
            }
            set
            {
                _day3 = value;
            }
        }
        public static ObservableCollection<Ticket> GetTicketPersons()
        {

            ObservableCollection<Ticket> lijst = new ObservableCollection<Ticket>();

            String sSQL = "SELECT * FROM tickets";
            DbDataReader reader = Database.GetData(sSQL);

            ObservableCollection<TicketType> tempTypes = GetTicketType();

            while (reader.Read())
            {
                Ticket tempticket = new Ticket();

                tempticket.ID = reader["ID"].ToString();
                tempticket.Ticketholder = reader["TicketHolder"].ToString();
                tempticket.TicketHolderEmail = reader["TicketHolderEmail"].ToString();
                tempticket.Amount = int.Parse(reader["Amount"].ToString());

                if (reader["Day1"].ToString() == "1")
                {
                    tempticket.Day1 = true;
                }
                else tempticket.Day1 = false;

                if (reader["Day2"].ToString() == "1")
                {
                    tempticket.Day2 = true;
                }
                else tempticket.Day2 = false;

                if (reader["Day3"].ToString() == "1")
                {
                    tempticket.Day3 = true;
                }
                else tempticket.Day3 = false;
                
                int typeindex = int.Parse(reader["TicketType"].ToString());
                tempticket.TicketType = tempTypes[typeindex - 1];

                lijst.Add(tempticket);
            }
            return lijst;
            //XmlDocument doc = new XmlDocument();
            ////doc.Load("C:/Users/Lecat/Dropbox/nmct personal/prgramming/Project/Project/bin/Debug/Tickets.xml");
            //doc.Load("Tickets.xml");
            //XmlNodeList lokalelijst = doc.GetElementsByTagName("tickets");
            //for (int i = 1; i < lokalelijst.Count; ++i)
            //{
            //    Ticket ticket = new Ticket();
            //    ticket.ID = lokalelijst[i].Attributes["id"].InnerText;
            //    ticket.Ticketholder = lokalelijst[i].Attributes["ticketholder"].InnerText;
            //    ticket.TicketHolderEmail = lokalelijst[i].Attributes["ticketholderemail"].InnerText;
            //    int temp;
            //    Int32.TryParse(lokalelijst[i].Attributes["amount"].InnerText,out temp);
            //    ticket.Amount = temp;

            //    string tempid = lokalelijst[i].Attributes["typeid"].InnerText;
            //    string tempname = lokalelijst[i].Attributes["typename"].InnerText;
            //    string typeprice = lokalelijst[i].Attributes["typeprice"].InnerText;
            //    double tempdoubleprice;
            //    double.TryParse(typeprice, out tempdoubleprice);
            //    TicketType temptype = new TicketType(tempid, tempname, tempdoubleprice);
            //    ticket.TicketType = temptype;
            //   // string tempid = lokalelijst[i].Attributes["typeid"].InnerText;
            //   // string tempname = lokalelijst[i].Attributes["typename"].InnerText;
            //    //ContactpersonType temp = new ContactpersonType(tempid, tempname);
            //    //contact.JobRole = temp;
            //    //adding the contactpersontype
            //    lijst.Add(ticket);
            //}
            //return lijst;
        }

        public override string ToString()
        {
            return this.ID + "\t" + this.Ticketholder + "\t" + this.TicketHolderEmail + "\t" + this.TicketType.ToString() + "\t" + this.Amount;
        }
        public static ObservableCollection<TicketType> GetTicketType()
        {
            ObservableCollection<TicketType> lijst = new ObservableCollection<TicketType>();

            String sSQL = "SELECT * FROM ticket_type";

            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                TicketType temptype = new TicketType();
                temptype.ID = reader["ID"].ToString();
                temptype.Name = reader["Name"].ToString();
                temptype.Price = int.Parse(reader["Price"].ToString());
                lijst.Add(temptype);
               
            }
            return lijst;
        }
    }
}
