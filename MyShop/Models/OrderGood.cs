using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class OrderGood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Sum { get; set; }

        [Display(Name = "Order ID")]
        public int OrderID { get; set; }

        public Order Order { get; set; }

        [Display(Name = "Good ID")]
        public int GoodID { get; set; }

        public Good Good { get; set; }
    }
}
