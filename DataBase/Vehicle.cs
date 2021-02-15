namespace Kursach.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehicle")]
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            Contract = new HashSet<Contract>();
        }

        public int id { get; set; }

        public int code_class { get; set; }

        [Required]
        [StringLength(50)]
        public string color { get; set; }

        public DateTime date_release { get; set; }

        public int code_model { get; set; }

        public int code_type { get; set; }

        [Required]
        [StringLength(50)]
        public string VIN_number { get; set; }

        [Required]
        [StringLength(50)]
        public string Gos_number { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contract { get; set; }

        public virtual Model Model { get; set; }

        public virtual Type_body Type_body { get; set; }
    }
}
