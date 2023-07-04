using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdmin.Models.Entity
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        public string Photo { get; set; }
        [Required]
        public string Сategory { get; set; }
        [Required]
        public int Price { get; set; }
        [Display(Name = "Persons count")]
        [Column("Persons_count")]
        [Required]
        public int PersonsCount { get; set; }

        // навигационные свойства
        public ICollection<RoomDate>? RoomDate { get; set; }
        public ICollection<Client>? Client { get; set; }

        public Room()
        {
            Id = default;
            Number = "";
            Photo = "";
            Сategory = "";
            Price = default;
            PersonsCount = default;
        }
    }
}
