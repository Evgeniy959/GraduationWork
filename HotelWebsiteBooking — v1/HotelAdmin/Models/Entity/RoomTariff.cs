using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdmin.Models.Entity
{
    public class RoomTariff
    {
        //public int Id { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public string? Description { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        public int Price { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        //public int DiscountPrice { get; set; }
    }
}
