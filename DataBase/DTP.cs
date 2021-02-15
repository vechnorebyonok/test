namespace Kursach.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DTP")]
    public partial class DTP
    {
        public int id { get; set; }

        public int? contract_id { get; set; }

        [Required]
        [StringLength(50)]
        public string date { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual Description_DTP Description_DTP { get; set; }
    }
}
