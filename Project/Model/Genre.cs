using Labo06_1.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    class Genre
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Picture { get; set; }
        public static void AddGenre(Genre temp)
        {
            string sql = "INSERT INTO genres (Name,Pictures) VALUES(@Name,@Pictures)";
            ModifyDatabase(sql, temp);
            
        }

        public static void SaveGenre(Genre temp)
        {
            string sql = "UPDATE genres SET Name=@Name,Picture=@Picture where ID=@ID";
            ModifyDatabase(sql, temp);
        }

        private static void ModifyDatabase(string sql, Genre temp)
        {
            DbParameter id = Database.AddParameter("@ID", temp.ID);
            DbParameter name = Database.AddParameter("@Name", temp.Name);
            DbParameter picture = Database.AddParameter("@Picture", temp.Picture);
            Database.ModifyData(sql, id, name,picture);
        }
    }
}
