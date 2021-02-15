namespace Kursach.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceList")]
    public partial class PriceList
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public int Class { get; set; }

        public DateTime DateStart { get; set; }

        public bool Actual { get; set; }

        public virtual Class Class1 { get; set; }
    }
}
