using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChemReagentsProject.Annotations;

namespace ChemReagentsProject.VVM.Additional
{
    public abstract class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        protected virtual void OnPropertyChangedSeveral(params string[] propertyNames)
        {
            foreach (var s in propertyNames)
            {   
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
            }
        }
    }
}