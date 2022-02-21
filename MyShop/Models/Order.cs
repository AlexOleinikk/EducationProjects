using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalSum { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd-hh-mm-ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date and time")]
        public DateTime DateTime { get; set; }

        public ICollection<OrderGood> OrderGoods { get; set; }

        public string UserEmail { get; set; }

        public User User { get; set; }
    }
}
