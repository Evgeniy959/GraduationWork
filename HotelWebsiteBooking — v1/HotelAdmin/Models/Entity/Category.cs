using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Photo { get; set; }
        [Required]
        public string Сategory { get; set; }
        [Required]
        public string Square { get; set; }
        /*[Required]
        public int Price { get; set; }*/
        [Display(Name = "Persons count")]
        [Column("Persons_count")]
        [Required]
        public int PersonsCount { get; set; }
    }
}
