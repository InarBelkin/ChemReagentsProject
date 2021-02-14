using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.SolutionsPage
{
    class SolutionVM :INotifyPropertyChanged
    {
        IDBCrud dbOp;
        IReportServ rep;
        public SolutionVM(IDBCrud cr, IReportServ report)
        {
            dbOp = cr;
            rep = report;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<SolutionM> SolutionList
        {
            get
            {
                var ret = dbOp.Solutions.GetList();
                foreach (var s in ret)
                {
                    s.PropertyChanged += Solut_PropertyChanged;
                }
                ret.CollectionChanged += Solut_CollectionChanged;
                return ret;
            }
        }

        private void Solut_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Solut_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
