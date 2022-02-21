using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 1)]
        [Display(Name = "Long description")]
        public string LongDesc { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        [Display(Name = "Short description")]
        public string ShortDesc { get; set; }

        public ICollection<Good> Goods { get; set; }
    }
}
