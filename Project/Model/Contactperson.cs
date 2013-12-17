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
    class Contactperson : ObservableObject
    {
        public String ID{get;set;}
           

        private String _name;
        public String Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private String _company;
        public String Company 
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
                OnPropertyChanged("Company");
            }
        }

        public ContactpersonType JobRole { get; set; }
        //private ContactpersonType _jobRole;
        //public ContactpersonType JobRole 
        //{
        //    get
        //    {
        //        return _jobRole;
        //    }
        //    set
        //    {
        //       // _jobRole.Name = value.Name;
        //       //OnPropertyChanged("JobRole.Name");
        //    }
        //}
        private String _city;
        public String City 
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }
        private String _email;
        public String Email 
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }
        private String _phone;
        public String Phone 
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }
        private String _cellphone;
        public String Cellphone 
        {
            get
            {
                return _cellphone;
            }
            set
            {
                _cellphone = value;
                OnPropertyChanged("Cellphone");
            }
        }

        public static ObservableCollection<Contactperson> GetContactPersons()
        {
            
                ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();

                String sSQL = "SELECT * FROM contacts";

                DbDataReader reader = Database.GetData(sSQL);

                ObservableCollection<ContactpersonType> tempTypes = GetContactTypes();
                while (reader.Read())
                {
                    Contactperson newPerson = new Contactperson();
                    newPerson.ID = reader["ID"].ToString();
                    newPerson.Name = reader["Name"].ToString();
                    newPerson.Company = reader["Company"].ToString();
                    newPerson.City = reader["City"].ToString();
                    newPerson.Email = reader["Email"].ToString();
                    newPerson.Phone = reader["Phone"].ToString();
                    newPerson.Cellphone = reader["Cellphone"].ToString();

                    int type = int.Parse(reader["Type"].ToString());

                    
                    newPerson.JobRole = tempTypes[type-1];

                    
                    lijst.Add(newPerson);
                }
                  //xml reading code
                //XmlDocument doc = new XmlDocument();
                ////doc.Load("C:/Users/Lecat/Dropbox/nmct personal/prgramming/Project/Project/bin/Debug/Contacts.xml");
                //doc.Load("Contacts.xml");
                //XmlNodeList lokalelijst = doc.GetElementsByTagName("contacts");
                //for (int i = 1; i < lokalelijst.Count; ++i)
                //{
                //    Contactperson contact = new Contactperson();
                //    contact.ID = lokalelijst[i].Attributes["id"].InnerText;
                //    contact.Name = lokalelijst[i].Attributes["naam"].InnerText;
                //    contact.Company = lokalelijst[i].Attributes["company"].InnerText;
                //    contact.City = lokalelijst[i].Attributes["city"].InnerText;
                //    contact.Email = lokalelijst[i].Attributes["email"].InnerText;
                //    contact.Phone = lokalelijst[i].Attributes["phone"].InnerText;
                //    contact.Cellphone = lokalelijst[i].Attributes["cellphone"].InnerText;

                //    string tempid = lokalelijst[i].Attributes["typeid"].InnerText;
                //    string tempname = lokalelijst[i].Attributes["typename"].InnerText;
                //    ContactpersonType temp = new ContactpersonType(tempid, tempname);
                //    contact.JobRole = temp;
                //    //adding the contactpersontype
                //    lijst.Add(contact);
                //}
                return lijst;     

             

        }
        public override string ToString()
        {
            return this.ID + "\t" + this.Name + "\t" + this.Company + "\t" + this.City + "\t" + this.Email + "\t" + this.Phone + "\t" + this.Cellphone + "\t" + this.JobRole.ToString();
        }

        public static void SaveContacts(Contactperson tempperson)
        {
            string sql = ("UPDATE contacts SET Name=@Name,Company=@Company,City=@City,Email=@Email,Phone=@Phone,Cellphone=@Cellphone,Type=@Type where ID=@ID");
            ModifyDatabase(sql, tempperson);
        }
        public static void AddContact(Contactperson tempperson)
        {
            ObservableCollection<ContactpersonType> temptypes = GetContactTypes();
           
            
            for (int i = 0; i < temptypes.Count(); ++i)
            {
                if (temptypes[i].Name == tempperson.JobRole.Name)
                {
                    tempperson.JobRole.ID = i+1;
                }
            }


            string sql = "INSERT INTO contacts (Name,Company,City,Email,Phone,Cellphone,Type) VALUES(@Name,@Company,@City,@Email,@Phone,@Cellphone,@Type)";
            ModifyDatabase(sql, tempperson);
        }

        public static void DeleteContact(Contactperson temp)
        {
            String sql = "DELETE FROM contacts where ID=@ID";
            ModifyDatabase(sql,temp);
        }

        private static void ModifyDatabase(string sql,Contactperson temp)
        {
            DbParameter id = Database.AddParameter("@ID", temp.ID);
            DbParameter name = Database.AddParameter("@Name", temp.Name);
            DbParameter company = Database.AddParameter("@Company", temp.Company);
            DbParameter city = Database.AddParameter("@City", temp.City);
            DbParameter email = Database.AddParameter("@Email", temp.Email);
            DbParameter phone = Database.AddParameter("@Phone", temp.Phone);
            DbParameter cellphone = Database.AddParameter("@CellPhone", temp.Cellphone);
            DbParameter type = Database.AddParameter("@Type", temp.JobRole.ID);

            Database.ModifyData(sql,id, name, company, city, email,phone,cellphone, type);
        }



        public static ObservableCollection<ContactpersonType> GetContactTypes()
        {
            ObservableCollection<ContactpersonType> lijst = new ObservableCollection<ContactpersonType>();
            String sSQL = "SELECT * FROM contact_type";

            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                ContactpersonType tempType = new ContactpersonType();
                tempType.ID = int.Parse(reader["ID"].ToString());
                tempType.Name = reader["Name"].ToString();

                lijst.Add(tempType);
            }


            return lijst;
        }
    }
}
