namespace Kursach.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Info_client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int client_id { get; set; }

        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(4)]
        public string serial_pass { get; set; }

        [StringLength(6)]
        public string numer_pass { get; set; }

        public DateTime date_of_birth { get; set; }

        public int number_license { get; set; }

        [StringLength(50)]
        public string Firstname { get; set; }

        [StringLength(50)]
        public string Lastname { get; set; }

        [StringLength(50)]
        public string Middlename { get; set; }

        public virtual Client Client { get; set; }

        public virtual License License { get; set; }
    }
}
