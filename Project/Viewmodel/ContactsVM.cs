using GalaSoft.MvvmLight.Command;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Viewmodel
{
    class ContactsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Contacts"; } // unieke naam!
        }
        public ContactsVM()
        {
            //data ophalen
            CreateSaveCommand();
            CreateAddCommand();
            CreateDeleteCommand();
            _contacts = Contactperson.GetContactPersons();
        }

        //variabele om de listbox contacts aan te binden.
        private ObservableCollection<Contactperson> _contacts;
        public ObservableCollection<Contactperson> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
                
            }
        }

        

        private Contactperson _geselecteerdContact;
        public Contactperson GeselecteerdContact
        {
            get
            {
                return _geselecteerdContact;
            }
            set
            {
                _geselecteerdContact = value;
                OnPropertyChanged("GeselecteerdContact");
               // OnPropertyChanged("Contacts");
                
            }
        }

        //

        public ICommand SaveCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteSaveCommand()
        {

            if (GeselecteerdContact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateSaveCommand()
        {
            SaveCommand = new RelayCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
        }

        public void ExecuteSaveCommand()
        {
            //hier komt de method
            Contactperson.SaveContacts(GeselecteerdContact);
            Contacts = Contactperson.GetContactPersons();
        }




        public ICommand AddCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteAddCommand(object[] param)
        {
            return true;
        }
        private void CreateAddCommand()
        {
            AddCommand = new RelayCommand<object[]>(ExecuteAddCommand, CanExecuteAddCommand);
            //object[] ipv object omdat het multibinding is
        }

        public void ExecuteAddCommand(object[] param)
        {
            //hier komt de method
            Contactperson temp = new Contactperson();
            temp.Name = param[0].ToString();
            temp.Company = param[1].ToString();
            temp.City = param[2].ToString();
            temp.Email = param[3].ToString();
            temp.Phone = param[4].ToString();
            temp.Cellphone = param[5].ToString();
            ContactpersonType tempType = new ContactpersonType();
            tempType.Name = param[6].ToString();
            temp.JobRole = tempType;

            Contactperson.AddContact(temp);
            Console.WriteLine(param.ToString());
                Contacts = Contactperson.GetContactPersons();
        }


        public ICommand DeleteCommand
        {
            get;
            internal set;
        }
        public bool CanExecuteDeleteCommand()
        {
            if (GeselecteerdContact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateDeleteCommand()
        {
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            //object[] ipv object omdat het multibinding is
        }

        public void ExecuteDeleteCommand()
        {
           
            Contactperson.DeleteContact(GeselecteerdContact);

            Contacts = Contactperson.GetContactPersons();
        }
    }
}
