namespace DAL.Tables
{
    public class Supplier : BaseDBModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
    }
}