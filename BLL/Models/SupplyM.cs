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
        private int supplierId;
        public int SupplierId { get => supplierId; set { supplierId = value; OnPropertyChanged(); } }
        //private int reagentNumber;
        //public int ReagentNumber { get => reagentNumber; set { reagentNumber = value;OnPropertyChanged(); } }
        private DateTime date_Begin;
        public DateTime Date_Begin { get => date_Begin; set { date_Begin = value; OnPropertyChanged(); } }
        private DateTime date_End;
        public DateTime Date_End { get => date_End; set { date_End = value; OnPropertyChanged(); } }
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
                ShortName = Date_End.ToString("dd.MM.yy ") + RusState;

                OnPropertyChanged();
            }
        }
        private bool unpacked;
        public bool Unpacked { get => unpacked; set { unpacked = value; OnPropertyChanged(); } }
        private decimal countMas;
        public decimal CountMas { get => countMas; set { countMas = value; OnPropertyChanged(); OnPropertyChanged("CountVolum"); } }
        private decimal density;
        public decimal Density { get => density; set { density = value; OnPropertyChanged(); } }
        public decimal CountVolum { get => CountMas / Density; set { CountMas = value * Density; OnPropertyChanged(); OnPropertyChanged("CountMas"); } }
        public string RusState { get; set; }
        public string ShortName { get; set; }

        public SupplyM() { Density = 1; }
        public SupplyM(Supply s)
        {
            setfromDal(s);
        }


        internal override void setfromDal(Supply s)
        {
            Id = s.Id;
            ReagentId = s.ReagentId;
            SupplierId = s.SupplierId;
            Date_Begin = s.Date_Begin;
            Date_End = s.Date_End;
            State = (SupplStates)s.State;
            Unpacked = s.Unpacked;
            CountMas = s.Count;
            Density = s.Density;
            if (State != SupplStates.WriteOff)
            {
                if (Date_End < DateTime.Today)
                {
                    State = SupplStates.ToWriteOff;
                }
                else
                {
                    if (Date_End < DateTime.Today.AddMonths(1))
                    {
                        State = SupplStates.SoonToWriteOff;
                    }
                    else
                    {
                        State = SupplStates.Active;
                    }
                }

            }
        }

        internal override void updDal(Supply item)
        {
            item.Id = Id;
            item.ReagentId = ReagentId;
            item.SupplierId = SupplierId;
            item.Date_Begin = Date_Begin;
            item.Date_End = Date_End;
            item.State = (byte)State;
            item.Unpacked = Unpacked;
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
