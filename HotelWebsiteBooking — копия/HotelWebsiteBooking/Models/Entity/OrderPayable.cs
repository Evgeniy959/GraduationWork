using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelWebsiteBooking.Models.Entity
{
    public class OrderPayable
    {
        [Key]
        [Column("Number")]
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public Client? Client { get; set; }

        public OrderPayable()
        {
            Status = string.Empty;
        }
    }
}
