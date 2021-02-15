namespace Kursach.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pay")]
    public partial class Pay
    {
        [Key]
        public int code_pay { get; set; }

        public int Contract { get; set; }

        public double Price { get; set; }

        public virtual Contract Contract1 { get; set; }
    }
}
