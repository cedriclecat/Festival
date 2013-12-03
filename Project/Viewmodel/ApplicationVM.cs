using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Viewmodel
{
    class ApplicationVM : ObservableObject
    {
        public ApplicationVM()
        {
            _pages = new ObservableCollection<IPage>();
            /*hieronder voegen we al een eerste IPage-object toe
             * bij nieuwe pages moet deze lijst aangevuld worden met telkens
             * de bijhorende viewmodel-klasse*/
            _pages.Add(new HomePageVM());
            _pages.Add(new ContactsVM());
            _pages.Add(new LineUpVM());
            _pages.Add(new TicketsVM());
            _pages.Add(new SettingsVM());
            _pages.Add(new LineUpSettingsVM());

            //default zetten we de currentpage in op eerste Ipage (hier HomePage)
            _currentPage = Pages[0];
        }

        private IPage _currentPage;
        public IPage CurrentPage
        {
            get
            {
                return _currentPage; //huidige pagina
            }
            set
            {
                _currentPage = value;
                //ik maak aan de buitenwereld bekend dat er de property "currentpage"
                //gewijzigd is. Eventuele controls die er aan gebind zijn, worden nu 
                //aangepast.
                OnPropertyChanged("CurrentPage");
            }
        }
        //bijhouden van de verschillende IPages

        private ObservableCollection<IPage> _pages;
        public ObservableCollection<IPage> Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                _pages = value;
                OnPropertyChanged("Pages");
            }
        }
        /*onderstaande komt uit cursus
         * deze 2 methodes worden gebruikt door de buttons(op mainwindow.xaml)
         * en kan het juite commando activeren. Hier is dat het wisselen van Page*/
        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }
        private void ChangePage(IPage page)
        {
            CurrentPage = page;
        }
    }
}
