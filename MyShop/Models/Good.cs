using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Good
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? Quantity { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string ShortDesc { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 1)]
        public string LongDesc { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [DataType(DataType.EmailAddress)]
        public string AuthorMail { get; set; }

        public ICollection<OrderGood> OrderGoods { get; set; }

        public string CategoryName { get; set; }

        public Category Category { get; set; }
    }
}
