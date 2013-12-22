using Labo06_1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Model
{
    class Settings
    {
        public int ID { get; set; }
        public DateTime Startdate { get; set; }
        public int AvailableTickets {get;set;}

        public static Settings GetSettings()
        {
            Settings temp = new Settings();

            String sSQL = "SELECT * FROM settings";

            DbDataReader reader = Database.GetData(sSQL);

            while (reader.Read())
            {
                temp.ID = Int32.Parse(reader["ID"].ToString());
                temp.AvailableTickets = Int32.Parse(reader["AvailableTickets"].ToString());
                string tempdate = reader["StartDate"].ToString();
                temp.Startdate = DateTime.Parse(tempdate);
               
            }
            return temp;
        }

        public static void BuyTicket(int amount)
        {

        }

        public static void SaveSettings(Settings temp)
        {
            string sql = "UPDATE settings SET StartDate=@StartDate,AvailableTickets=@AvailableTickets";
            ModifieDatabase(sql, temp);
        }
        public static void ModifieDatabase(string sql, Settings temp)
        {
            DbParameter id = Database.AddParameter("@ID", temp.ID);
            DbParameter availableTickets = Database.AddParameter("@AvailableTickets", temp.AvailableTickets);
            DbParameter startdate = Database.AddParameter("@StartDate", temp.Startdate);

            Database.ModifyData(sql, id, startdate, availableTickets);
        }
    }
}
