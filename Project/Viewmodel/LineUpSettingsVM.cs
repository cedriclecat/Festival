using GalaSoft.MvvmLight.Command;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project.Viewmodel
{
    class LineUpSettingsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "LineUpSettings"; } // unieke naam!
        }
         public LineUpSettingsVM()
        {
            _lineUps = LineUp.GetLineUp();
             
            _bands = LineUp.GetBands(LineUp.GetGenres());
            _stages = LineUp.GetStage();
            CreateSaveCommand();
            CreateResetCommand();
            CreateDeleteCommand();
            CreateAddCommand();
        }

         private ObservableCollection<LineUp> _lineUps;
         public ObservableCollection<LineUp> LineUps
         {
             get
             {
                 return _lineUps;
             }
             set
             {
                 _lineUps = value;
                 OnPropertyChanged("LineUps");
             }
         }

         private LineUp _geselecteerdeperformer;
         public LineUp GeselecteerderPerformer
         {
             get
             {
                 return _geselecteerdeperformer;
             }
             set
             {
                 _geselecteerdeperformer = value;
                 OnPropertyChanged("GeselecteerderPerformer");
             }
         }


         private ObservableCollection<Band> _bands;
         public ObservableCollection<Band> Bands
         {
             get
             {
                 return _bands;
             }
             set
             {
                 _bands = value;
                 OnPropertyChanged("Bands");
             }
         }

         private ObservableCollection<Stage> _stages;
         public ObservableCollection<Stage> Stages
         {
             get
             {
                 return _stages;
             }
             set
             {
                 _stages = value;
                 OnPropertyChanged("Stages");
             }
         }

         private Stage _selectedStage;
         public Stage SelectedStage
         {
             get
             {
                 return _selectedStage;

             }
             set
             {
                 _selectedStage = value;
                 FilterStages(_selectedStage);
                 OnPropertyChanged("SelectedStage");
             }
         }

         private Band _selectedNewBand;
         public Band SelectedNewBand
         {
             get
             {
                 return _selectedNewBand;
             }
             set
             {
                 _selectedNewBand = value;
                 OnPropertyChanged("SelectedNewBand");
             }
         }

         private Stage _selectedNewStage;
         public Stage SelectedNewStage
         {
             get
             {
                 return _selectedNewStage;
             }
             set
             {
                 _selectedNewStage = value;
                 OnPropertyChanged("SelectedNewStage");
             }
         }


         public void FilterStages(Stage stages)
         {
             LineUps = LineUp.GetLineUp();
             ObservableCollection<LineUp> temp = new ObservableCollection<LineUp>();
             foreach (LineUp line in LineUps)
             {
                 if (stages.Name == line.Stage.Name)
                 {
                     temp.Add(line);
                 }
             }
             LineUps = temp;
         }

         public ICommand ResetCommand
         {
             get;
             internal set;
         }
         public bool CanExecuteResetCommand()
         {
             return true;
         }
         private void CreateResetCommand()
         {
             ResetCommand = new RelayCommand(ExecuteResetCommand, CanExecuteResetCommand);
         }

         public void ExecuteResetCommand()
         {
             LineUps = LineUp.GetLineUp();
         }


         public ICommand SaveCommand
         {
             get;
             internal set;
         }
         public bool CanExecuteSaveCommand()
         {
             return true;
         }
         private void CreateSaveCommand()
         {
             SaveCommand = new RelayCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
         }

         public void ExecuteSaveCommand()
         {
             LineUp temp = new LineUp();
             temp = GeselecteerderPerformer;
             if (_selectedNewBand != null)
             {
                 temp.Band = _selectedNewBand;
             }            
             if (_selectedNewStage != null)
             {
                 temp.Stage = _selectedNewStage;
             }
             LineUp.SaveLinups(temp);
             LineUps = LineUp.GetLineUp();
         }

         public ICommand AddCommand
         {
             get;
             internal set;
         }
         public bool CanExecuteAddCommand(object[] param)
         {
             if (SelectedNewBand != null && SelectedNewStage != null)
             {
                 return true;
             }
             else
             {
                 return false;
             }
         }
         private void CreateAddCommand()
         {
             AddCommand = new RelayCommand<object[]>(ExecuteAddCommand, CanExecuteAddCommand);
         }

         public void ExecuteAddCommand(object[] param)
         {
             LineUp temp = new LineUp();
             temp.Band = _selectedNewBand;
             temp.Stage = _selectedNewStage;
             temp.From = param[0].ToString();
             temp.Until = param[1].ToString();
             
             LineUp.AddLinups(temp);
             LineUps = LineUp.GetLineUp();
         }

         public ICommand DeleteCommand
         {
             get;
             internal set;
         }
         public bool CanExecuteDeleteCommand()
         {
             return true;
         }
         private void CreateDeleteCommand()
         {
             DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
         }

         public void ExecuteDeleteCommand()
         {
             
             LineUp.DeleteLineup(GeselecteerderPerformer);
             LineUps = LineUp.GetLineUp();
         }
    }
}
