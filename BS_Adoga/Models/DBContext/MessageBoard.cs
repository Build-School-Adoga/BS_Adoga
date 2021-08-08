namespace BS_Adoga.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MessageBoard")]
    public partial class MessageBoard
    {
        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerID { get; set; }

        [Key]
        [StringLength(50)]
        public string OrderID { get; set; }

        [Required]
        public string MessageText { get; set; }
      
        public decimal Score { get; set; }


        public virtual Order Order { get; set; }
    }
}
