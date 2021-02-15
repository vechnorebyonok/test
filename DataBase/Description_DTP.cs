namespace Kursach.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Description_DTP
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string DPT_id { get; set; }

        [StringLength(50)]
        public string Place { get; set; }

        [Required]
        [StringLength(50)]
        public string Date { get; set; }

        [StringLength(50)]
        public string Conditions { get; set; }

        public virtual DTP DTP { get; set; }
    }
}
