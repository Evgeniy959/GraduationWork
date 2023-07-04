using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdmin.Models.Entity
{
    public class Order
    {
        [Column("Number")]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }

        public Client? Client { get; set; }
    }
}
