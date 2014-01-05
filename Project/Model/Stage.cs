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
        public String Picture { get; set; }
        public static void SaveStage(Stage geselecteerd)
        {
            string sql = "UPDATE stages SET Name=@Name,Picture=@Picture where ID=@ID";
            ModifyDatabase(sql, geselecteerd);
            
        }

        public static void AddStage(Stage temp)
        {
            string sql = "INSERT INTO stages (Name,Picture) VALUES(@Name,@Picture)";
            ModifyDatabase(sql, temp);
        }

        private static void ModifyDatabase(string sql, Stage temp)
        {
            DbParameter id = Database.AddParameter("@ID", temp.ID);
            DbParameter name = Database.AddParameter("@Name", temp.Name);
            DbParameter picture = Database.AddParameter("@Picture", temp.Picture);
            Database.ModifyData(sql, id, name, picture);
        }
    }

     
}
