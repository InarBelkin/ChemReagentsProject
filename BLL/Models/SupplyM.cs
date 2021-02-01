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
        public int ReagentId { get=> reagentId; set { reagentId = value; OnPropertyChanged(); } }
        private DateTime date_Begin;
        public DateTime Date_Begin { get=> date_Begin; set { date_Begin = value;OnPropertyChanged(); } }
        private DateTime date_End;
        public DateTime Date_End { get=>date_End; set { date_End = value;OnPropertyChanged(); } }
        private SupplStates state;
        public SupplStates State { get=> state; set { state = value;OnPropertyChanged(); } }
        private bool unpacked;
        public bool Unpacked { get=>unpacked; set { unpacked = value; OnPropertyChanged(); } }
        private float count;
        public float Count { get=>count; set { count = value;OnPropertyChanged(); } }

        public SupplyM() { }
        public SupplyM(Supply s)
        {
            setfromDal(s);
        }


        internal override void setfromDal(Supply s)
        {
            Id = s.Id;
            ReagentId = s.ReagentId;
            Date_Begin = s.Date_Begin;
            Date_End = s.Date_End;
            State = (SupplStates)s.State;
            Unpacked = s.Unpacked;
            Count = s.Count;
        }

        internal override void updDal(Supply item)
        {
            item.Id = Id;
            item.ReagentId = ReagentId;
            item.Date_Begin = Date_Begin;
            item.Date_End = Date_End;
            item.State = (byte)State;
            item.Unpacked = Unpacked;
            item.Count = Count;
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
