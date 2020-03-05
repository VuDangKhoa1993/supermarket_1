using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketAPI.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Range(0, 100)]
        public short QuantityInPackage { get; set; }
        [Required]
        [Range(1, 5)]
        public int UnitOfMeasurement { get; set; }
        public int CategoryId { get; set; }
    }
}
