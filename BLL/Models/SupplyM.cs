using BLL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SupplyM : IModel<Supply>
    {
        private int reagentId;
        public int ReagentId { get => reagentId; set { reagentId = value; OnPropertyChanged(); } }
        private int? supplierId;
        public int? SupplierId { get => supplierId; set { supplierId = value; OnPropertyChanged(); } }
        //private int reagentNumber;
        //public int ReagentNumber { get => reagentNumber; set { reagentNumber = value;OnPropertyChanged(); } }
        //private DateTime date_Begin;
        //public DateTime Date_Begin { get => date_Begin; set { date_Begin = value; OnPropertyChanged(); } }
        //private DateTime date_End;
        //public DateTime Date_End { get => date_End; set { date_End = value; OnPropertyChanged(); } }
        private DateTime date_Production;
        public DateTime DateProduction { get => date_Production; set { date_Production = value; OnPropertyChanged(); } }
        private DateTime date_StartUse;
        public DateTime DateStartUse { get => date_StartUse; set { date_StartUse = value; OnPropertyChanged(); } }
        private DateTime dateExpiration;
        public DateTime DateExpiration { get => dateExpiration; set { dateExpiration = value; OnPropertyChanged(); } }
        private DateTime dateUnWrite;
        public DateTime DateUnWrite { get => dateUnWrite; set { dateUnWrite = value; OnPropertyChanged(); } }
        private bool active;
        public bool Active { get => active; set { active = value; OnPropertyChanged(); } }
        private SupplStates state;
        public SupplStates State
        {
            get => state; set
            {
                state = value;
                string str;
                switch (state)
                {
                    case SupplStates.Active: str = "Активно"; break;
                    case SupplStates.SoonToWriteOff: str = "Скоро протухнет"; break;
                    case SupplStates.ToWriteOff: str = "На списание"; break;
                    case SupplStates.WriteOff: str = "Списано"; break;
                    default: str = "Ошибка"; break;
                }
                RusState = str;
                ShortName = DateExpiration.ToString("dd.MM.yy ") + RusState;

                OnPropertyChanged();
            }
        }

        private decimal countMas;
        public decimal CountMas { get => countMas; set { countMas = value; OnPropertyChanged(); OnPropertyChanged("CountVolum"); } }
        private decimal density;
        public decimal Density { get => density; set { if (value > 0) { density = value; OnPropertyChanged(); } } }
        public decimal CountVolum { get => CountMas / Density; set { CountMas = value * Density; OnPropertyChanged(); OnPropertyChanged("CountMas"); } }
        public string RusState { get; set; }
        public string ShortName { get; set; }

        public SupplyM() { Density = 1; Active = true; }
        public SupplyM(Supply s)
        {
            setfromDal(s);
        }


        internal override void setfromDal(Supply s)
        {
            Id = s.Id;
            ReagentId = s.ReagentId;
            SupplierId = s.SupplierId;
            DateProduction = s.Date_Production;
            DateStartUse = s.Date_StartUse;
            DateExpiration = s.Date_Expiration;
            DateUnWrite = s.Date_UnWrite;
            Active = s.Active;
            //State = (SupplStates)s.State;
            CountMas = s.Count;
            Density = s.Density;
            if (Active)
            {
                if (DateTime.Today < DateExpiration.AddMonths(-1))
                    State = SupplStates.Active;
                else if (DateTime.Today <= DateExpiration)
                    State = SupplStates.SoonToWriteOff;
                else State = SupplStates.ToWriteOff;
            }
            else State = SupplStates.WriteOff;

        }

        internal override void updDal(Supply item)
        {
            item.Id = Id;
            item.ReagentId = ReagentId;
            item.SupplierId = SupplierId;
            item.Date_Production = DateProduction;
            item.Date_StartUse = DateStartUse;
            item.Date_Expiration = DateExpiration;
            item.Date_UnWrite = DateUnWrite;
            item.Active = Active;
            //item.State = (byte)State;
            item.Count = CountMas;
            item.Density = Density;
        }

        public override bool Validate()
        {
            return (ReagentId > 0 && SupplierId > 0);
        }
    }

    public enum SupplStates : byte
    {
        Active = 1,
        SoonToWriteOff = 2,
        ToWriteOff = 3,
        WriteOff = 4
    }
}
