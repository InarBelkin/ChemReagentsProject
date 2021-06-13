using System.Collections.ObjectModel;
using System.Collections.Specialized;
using BLL.Interfaces;
using ChemReagentsProject.VVM.Additional;
using DAL.Tables;

namespace ChemReagentsProject.VVM.ReagentP
{
    public class ReagentVM : BaseVM
    {
        private IUnitOfWork db;
       

        public ReagentVM()
        {
            db = IoC.IoC.Get<IUnitOfWork>();
            OnPropertyChanged("CountChanges");
        }

        public ObservableCollection<Reagent> ReagentList
        {
            get
            {
                var l = new ObservableCollection<Reagent>(db.Reagents.GetList());
                foreach(var r in l)
                {
                    r.PropertyChanged += ReagPC;
                }
                l.CollectionChanged += ReagListChanged;
                return l;
            }
        }

        private void ReagPC(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("CountChanges");
        }

        private void ReagListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Reagent newreag  = e.NewItems[0] as Reagent;
                    db.Reagents.Create(newreag);
                    OnPropertyChanged("CountChanges");
                    break;

            }
        }
        
        private Reagent selectReag;
        public Reagent SelectReag
        {
            get => selectReag;
            set => selectReag = value;
        }

        public int CountChanges => db.CountChanges();

        private RelayCommand _saveAll;
        public RelayCommand SaveAll => _saveAll ??= new RelayCommand(obj =>
        {
            int a = db.Save();
            OnPropertyChanged("CountChanges");  
        });
    }
}