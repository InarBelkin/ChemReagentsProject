using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Tables
{
    public class Supply: BaseDBModel
    {
        [Required]
        public virtual Reagent Reagent { get; set; }    
        public virtual Supplier Supplier { get; set; }
        public virtual Report Report { get; set; }
        public  string Manufacturer { get; set; }
        
        public string IncomContr { get; set; }
        public string Qualification { get; set; }
        public string BatchNum { get; set; }
        public int PassNum { get; set; }
        
        public DateTime Date_Production { get; set; }
        public DateTime Date_StartUse { get; set; }
        public DateTime Date_Expiration { get; set; }
        
        
        public decimal Density { get; set; }
        public decimal CountMas { get; set; }
        [NotMapped]
        public decimal CountVolum { get; set; }
        [NotMapped]
        public string ShortName { get; set; }
        [NotMapped]
        public  SupplStates State { get; set; }
    }
    
    
    public enum SupplStates : byte
    {
        Active = 1,
        SoonToWriteOff = 2,
        ToWriteOff = 3,
        WriteOff = 4
    }
}