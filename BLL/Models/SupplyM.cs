using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Tables;
namespace BLL.Models
{
    public class SupplyM
    {
        public int Id { get; set; }
        public int ReagentId { get; set; }
        public int SupplierId { get; set; }
        public DateTime Date_Begin { get; set; }
        public DateTime Date_End { get; set; }
        private SupplStates state;
        public SupplStates State
        {
            get => state;
            set
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
            }
        }
        public string ShortName { get; set; }
        public string RusState { get; set; }
        public float Count { get; set; }

        public SupplyM()
        {
            Id = -2;
            ReagentId = -2; //да просто чтобы по умолчанию был неверный вариант
            SupplierId = -2;
            State = SupplStates.Active;
        }
        public SupplyM(Supply s)
        {
            Id = s.Id;
            ReagentId = s.ReagentId;
            SupplierId = s.SupplierId;
            Date_Begin = s.Date_Begin;
            Date_End = s.Date_End;
            State = (SupplStates)s.State;
            Count = s.count;
        }

        public bool Validate()
        {
            if (ReagentId != -2 && SupplierId != -2) return true;
            else return false;
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
