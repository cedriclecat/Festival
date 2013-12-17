using Labo06_1.Model;
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

        public static void SaveTicket(Ticket geselecteerd)
        {
            String sql = "UPDATE tickets SET TicketHolder=@TicketHolder,TicketHolderEmail=@TicketHolderEmail,Amount=@Amount,Day1=@Day1,Day2=@Day2,Day3=@Day3,TicketType=@TicketType where ID=@ID";
            ModifyDatabase(sql, geselecteerd);


        }
        
        public static void AddTicket(Ticket newticket)
        {

            String sql = "INSERT INTO tickets (TicketHolder,TicketHolderEmail,Amount,Day1,Day2,Day3,TicketType) VALUES(@TicketHolder,@TicketHolderEmail,@Amount,@Day1,@Day2,@Day3,@TicketType)";
            ModifyDatabase(sql, newticket);
        }

        public static void DeleteTicket(Ticket temp)
        {
            String sql = "DELETE FROM tickets where ID=@ID";
            ModifyDatabase(sql, temp);
        }

        private static void ModifyDatabase(string sql, Ticket temp)
        {
            DbParameter id = Database.AddParameter("@ID", temp.ID);
            DbParameter ticketholder = Database.AddParameter("@TicketHolder", temp.Ticketholder);
            DbParameter ticketholderemail = Database.AddParameter("@TicketHolderEmail", temp.TicketHolderEmail);
            DbParameter amount = Database.AddParameter("@Amount", temp.Amount);
            DbParameter day1 = Database.AddParameter("@Day1", temp.Day1);
            DbParameter day2 = Database.AddParameter("@Day2", temp.Day2);
            DbParameter day3 = Database.AddParameter("@Day3", temp.Day3);
            DbParameter tickettype = Database.AddParameter("@TicketType", temp.TicketType.ID);
            Database.ModifyData(sql,id, ticketholder, ticketholderemail, amount, day1, day2, day3,tickettype);
        }
    }
}
