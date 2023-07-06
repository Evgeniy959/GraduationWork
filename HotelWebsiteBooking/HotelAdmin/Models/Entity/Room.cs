using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdmin.Models.Entity
{
    public class Room
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Number { get; set; }
        //public List<RoomTariff> Tariff { get; set; } = new List<RoomTariff>();

        // навигационные свойства
        public Category? Category { get; set; }
        public ICollection<RoomDate>? RoomDate { get; set; }
        public ICollection<Client>? Client { get; set; }
        public ICollection<RoomTariff>? Tariff { get; set; }

        public Room()
        {
            Id = default;
            Number = "";
        }
    }
}
