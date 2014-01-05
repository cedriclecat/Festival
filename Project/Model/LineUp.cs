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
    class LineUp : ObservableObject
    {
        public String ID { get; set; }
        private DateTime _date;
        public DateTime Date 
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        private String _from;
        public String From 
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        
        }
        private String _until;
        public String Until 
        {
            get
            {
                return _until;
            }
            set
            {
                _until = value;
                OnPropertyChanged("Until");
            }
        }
    
        private Band _band;
        public Band Band 
        {
            get
            {
                return _band;
            }
            set
            {
                _band = value;
                OnPropertyChanged("BandName");
            }
        }
        private Stage _stage;
        public Stage Stage
        {
            get
            {
                return _stage;
            }
            set
            {
                _stage = value;
                OnPropertyChanged("StageName");
            }
        }

        public static ObservableCollection<LineUp> GetLineUp()
        {
            ObservableCollection<LineUp> lijst = new ObservableCollection<LineUp>();

            String sSQL = "SELECT * FROM lineup";

            DbDataReader reader = Database.GetData(sSQL);

            //genres ophalen
            ObservableCollection<Genre> tempGenres = GetGenres();

            //bands ophalen
            ObservableCollection<Band> tempBands = GetBands(tempGenres);
            
            //stages ophalen
            ObservableCollection<Stage> tempStages = GetStage();

            while (reader.Read())
            {
                LineUp newLineUp = new LineUp();
                newLineUp.ID = reader["ID"].ToString();   
                String date = reader["Date"].ToString();
               
                newLineUp.Date = DateTime.Parse(date);
                newLineUp.From = reader["StartTime"].ToString();
                newLineUp.Until = reader["EndTime"].ToString();
               
                int stageID = int.Parse(reader["StageID"].ToString());
                newLineUp.Stage = tempStages[stageID - 1];

                int bandID = int.Parse(reader["BandID"].ToString());
                newLineUp.Band = tempBands[bandID - 1];

                lijst.Add(newLineUp);
               
            }

           
            return lijst;
        }
        public static ObservableCollection<Band> GetBands(ObservableCollection<Genre> tempGenres)
        {
            ObservableCollection<Band> lijst = new ObservableCollection<Band>();

            String sSQL = "SELECT * FROM bands";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Band tempBand = new Band();
                tempBand.ID = int.Parse(reader["ID"].ToString());
                tempBand.Name = reader["Name"].ToString();
                if (reader["Twitter"] != null)
                {
                    tempBand.Twitter = reader["Twitter"].ToString();
                }
                else tempBand.Description = "Twitter";
                
                if (reader["Facebook"] != null)
                {
                    tempBand.Facebook = reader["Facebook"].ToString();
                }
                else tempBand.Description = "No Facebook given";
               
                if (reader["Description"] != null)
                {
                    tempBand.Description = reader["Description"].ToString();
                }
                else tempBand.Description = "No Description given";
                tempBand.Picture = reader["Picture"].ToString();
                int genreID = int.Parse(reader["GenreID"].ToString());                
                tempBand.Genre = tempGenres[genreID-1];

                lijst.Add(tempBand);
            }
            return lijst;
        }
        public static ObservableCollection<Genre> GetGenres()
        {
            ObservableCollection<Genre> lijst = new ObservableCollection<Genre>();
            String sSQL = "SELECT * FROM genres";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Genre tempGenre = new Genre();
                tempGenre.ID = int.Parse(reader["ID"].ToString());
                tempGenre.Name = reader["Name"].ToString();
                tempGenre.Picture = reader["Picture"].ToString();
                lijst.Add(tempGenre);
            }

            return lijst;
        }

        public static ObservableCollection<Stage> GetStage()
        {
            ObservableCollection<Stage> stages = new ObservableCollection<Stage>();

            String sSQL = "SELECT * FROM Stages";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Stage tempstage = new Stage();
                tempstage.ID = reader["ID"].ToString();
                tempstage.Name = reader["Name"].ToString();
                tempstage.Picture = reader["Picture"].ToString();
                stages.Add(tempstage);
            }
            return stages;
        }
        public override string ToString()
        {
            return this.Date + "\t" + this.From + "\t" + this.Until + "\t" + this.Stage.Name + "\t" + this.Band.Name + "\t"+this.Band.Genre.Name;
                
        }

        public static void SaveLinups(LineUp temp)
        {
            string sql = "UPDATE lineup SET Date=@Date,StartTime=@StartTime,EndTime=@EndTime,BandID=@BandID,StageID=@StageID where ID=@ID";
            ModifyDatabase(sql, temp);
        }
        public static void AddLinups(LineUp temp)
        {
            string sql = "INSERT INTO lineup (Date,StartTime,EndTime,BandID,StageID) VALUES(@Date,@StartTime,@EndTime,@BandID,@StageID)";
            ModifyDatabase(sql, temp);
        }
        public static void DeleteLineup(LineUp temp)
        {
            string sql = "DELETE FROM lineup where ID=@ID";
            ModifyDatabase(sql, temp);
        }

        private static void ModifyDatabase(string sql, LineUp temp)
        {
            DbParameter id = Database.AddParameter("@ID", temp.ID);
            DbParameter date = Database.AddParameter("@Date", temp.Date);
            DbParameter starttime = Database.AddParameter("@StartTime", temp.From);
            DbParameter endtime = Database.AddParameter("@EndTime", temp.Until);
            DbParameter bandid = Database.AddParameter("@BandID", temp.Band.ID);
            DbParameter stageid = Database.AddParameter("@StageID", temp.Stage.ID);

            Database.ModifyData(sql, id, date, starttime, endtime, bandid, stageid);
        }
    }
}
