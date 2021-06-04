using System;

namespace DAL.Tables
{
    public class Report : BaseDBModel
    {
        public DateTime TimeRep { get; set; }
        public DateTime RealDate { get; set; }
    }
}