using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebsiteBooking.Models.Entity
{
    public class RoomTariff
    {
        public int Id { get; set; }
        //[Key]
        public int RoomId { get; set; }
        public int TariffPlanId { get; set; }
        public int Price { get; set; }
        
        // навигационные свойства
        public Room? Room { get; set; }
        public TariffPlan? TariffPlan { get; set; }
        
        //public string? Description { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        //[Column(TypeName = "decimal(18,2)")]
        //public int DiscountPrice { get; set; }
    }
}
