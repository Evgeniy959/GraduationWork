using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebsiteBooking.Models.Entity
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Photo { get; set; }
        public string Сategory { get; set; }
        public int Price { get; set; }
        [Column("Persons_count")]
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
