namespace Kursach.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_sost
    {
        public int id { get; set; }

        public int? return_id { get; set; }

        [StringLength(50)]
        public string result_check { get; set; }

        public virtual Return_contract Return_contract { get; set; }
    }
}
