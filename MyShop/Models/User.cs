using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class User
    {
        [Key]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [DataType(DataType.EmailAddress)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Surname { get; set; }        

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string Status { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
