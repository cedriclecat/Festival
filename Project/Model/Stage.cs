using Labo06_1.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    class Stage
    {
        public String ID { get; set; }
        public String Name { get; set; }

        public static void SaveStage(Stage geselecteerd)
        {
            string sql = "INSERT INTO stages (Name) VALUES (@Name)";
            ModifyDatabase(sql, geselecteerd);
        }

        public static void AddStage(Stage temp)
        {
            string sql = "UPDATE stages SET Name=@Name where ID=@ID";
            ModifyDatabase(sql, temp);
        }

        private static void ModifyDatabase(string sql, Stage temp)
        {
            DbParameter id = Database.AddParameter("@ID", temp.ID);
            DbParameter name = Database.AddParameter("@Name", temp.Name);

            Database.ModifyData(sql, id, name);
        }
    }

     
}
