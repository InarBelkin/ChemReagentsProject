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
        }

        public ObservableCollection<Reagent> ReagentList
        {
            get
            {
                var l = new ObservableCollection<Reagent>(db.Reagents.GetList());
                l.CollectionChanged += ReagListChanged;
                return l;
            }
        }

        private void ReagListChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Reagent newreag  = e.NewItems[0] as Reagent;
                    db.Reagents.Create(newreag);
                    break;

            }
        }
        
        private RelayCommand _saveAll;

        public RelayCommand SaveAll => _saveAll ??= new RelayCommand(obj =>
        {
            int a = db.Save();
        });
    }
}