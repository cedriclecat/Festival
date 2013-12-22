using Labo06_1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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

        public override string ToString()
        {
            return this.Name + "\t" + this.Facebook + "\t" + this.Twitter + "\t" + this.Description + "\t" +this.Genre.Name; 
        }

        public static void AddBand(Band temp)
        {
            string sql = "INSERT INTO bands (Name,Twitter,Facebook,Description,GenreID) VALUES(@Name,@Twitter,@Facebook,@Description,@GenreID)";
            ModifyDatabase(sql, temp);
        }

        public static void SaveBand(Band temp)
        {
            string sql = "UPDATE bands T Name=@Name,Twitter=@Twitter,Facebook=@Facebook,Description=@Description,GenreID=@GenreID where ID=@ID";
            ModifyDatabase(sql, temp);
        }
        private static void ModifyDatabase(string sql, Band temp)
        {
            DbParameter id = Database.AddParameter("@ID", temp.ID);
            DbParameter name = Database.AddParameter("@Name", temp.Name);
            DbParameter twitter = Database.AddParameter("@Twitter", temp.Twitter);
            DbParameter facebook = Database.AddParameter("@Facebook", temp.Facebook);
            DbParameter description = Database.AddParameter("@Description", temp.Description);
            DbParameter genreID = Database.AddParameter("@GenreID", temp.Genre.ID);

            Database.ModifyData(sql, id, name, twitter, facebook, description, genreID);
        }
    }
}
