using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsedCarsAPI.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Range(0,9000)]
        public int Year { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [Range(0,1000000)]
        public int Kilometers { get; set; }

        [Required]
        [Range(1,10000000)]
        public decimal Price { get; set; }
    }
}
