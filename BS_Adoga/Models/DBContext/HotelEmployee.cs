namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotelEmployee")]
    public partial class HotelEmployee
    {
        [StringLength(50)]
        public string HotelEmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime RegisterDatetime { get; set; }
    }
}
