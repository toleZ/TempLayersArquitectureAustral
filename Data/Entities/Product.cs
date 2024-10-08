using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Range(1, int.MaxValue)]

        public int Stock {  get; set; }
        [Range(1, double.MaxValue)]

        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
