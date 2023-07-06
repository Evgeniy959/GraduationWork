using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdmin.Models.Entity
{
    public class ExtendedRoom
    {
        public int RoomId { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int TariffPlanId { get; set; }
        public TariffPlan? TariffPlan { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public int Price { get; set; }
        /*public string Photo { get; set; }
        [Required]
        public string СategoryName { get; set; }
        [Required]
        public string Square { get; set; }        
        [Display(Name = "Persons count")]
        [Column("Persons_count")]
        [Required]
        public int PersonsCount { get; set; }*/

        public ExtendedRoom()
        {
            Number = "";
            Price = default;
        }
    }
}
