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
                //newLineUp.Date = DateTime.ParseExact(date,"dd/MM/yyyy HH:mm:tt",null);
                newLineUp.Date = DateTime.Parse(date);
                newLineUp.From = reader["StartTime"].ToString();
                newLineUp.Until = reader["EndTime"].ToString();
               
                int stageID = int.Parse(reader["StageID"].ToString());
                newLineUp.Stage = tempStages[stageID - 1];

                int bandID = int.Parse(reader["BandID"].ToString());
                newLineUp.Band = tempBands[bandID - 1];

                lijst.Add(newLineUp);
               
            }

            //XmlDocument doc = new XmlDocument();
            //doc.Load("lineup.xml");
            ////doc.Load("C:/Users/Lecat/Dropbox/nmct personal/prgramming/Project/Project/bin/Debug/lineup.xml");
            //XmlNodeList lokalelijst = doc.GetElementsByTagName("lineup");
            //for (int i = 1; i < lokalelijst.Count; ++i)
            //{
            //    LineUp linup = new LineUp();
            //    linup.ID = lokalelijst[i].Attributes["id"].InnerText;
            //    linup.From = lokalelijst[i].Attributes["starttime"].InnerText;
            //    linup.Until = lokalelijst[i].Attributes["endtime"].InnerText;
            //    linup.BandName = lokalelijst[i].Attributes["band"].InnerText;
            //    linup.StageName = lokalelijst[i].Attributes["stage"].InnerText;
            //    String date;
            //    date = lokalelijst[i].Attributes["date"].InnerText;

            //    //date.Split(".");
            //    lijst.Add(linup);
            //}
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
                stages.Add(tempstage);
            }
            return stages;
        }
        public override string ToString()
        {
            return this.Date + "\t" + this.From + "\t" + this.Until + "\t" + this.Stage.Name + "\t" + this.Band.Name + "\t"+this.Band.Genre.Name;
                
        }
    }
}
