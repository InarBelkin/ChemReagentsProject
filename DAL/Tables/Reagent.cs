namespace DAL.Tables
{
    public class Reagent : BaseDBModel
    {
        private int _number;
        private string _name;
        private string _formula;
        private string _synonyms;
        private string _gost;
        private string _location;
        private bool _isWater;
        private bool _isAlwaysWater;
        private bool _isAccounted;

        public int Number { get => _number; set { _number = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Formula { get => _formula; set { _formula = value; OnPropertyChanged(); } }
        public string Synonyms { get => _synonyms; set { _synonyms = value; OnPropertyChanged(); } }
        public string GOST { get => _gost; set { _gost = value; OnPropertyChanged(); } }
        public string Location { get => _location; set { _location = value; OnPropertyChanged(); } }
        public bool isWater { get => _isWater; set { _isWater = value; OnPropertyChanged(); } }
        public bool isAlwaysWater { get => _isAlwaysWater; set { _isAlwaysWater = value; OnPropertyChanged(); } }
        public bool IsAccounted { get => _isAccounted; set { _isAccounted = value; OnPropertyChanged(); } }
    }
}
