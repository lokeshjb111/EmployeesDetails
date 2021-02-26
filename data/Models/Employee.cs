using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace data.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string Mobile { get; set; }
        [Required]
        [MaxLength(3)]
        public string Designation { get; set; }
        [Required]
        [MaxLength(1)]
        public string Gender { get; set; }
        public bool Bca { get; set; }
        public bool Mca { get; set; }
        public bool Bsc { get; set; }
        [Required]
        public string ImageId { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public string CreatedDate { get; set; }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }


}
