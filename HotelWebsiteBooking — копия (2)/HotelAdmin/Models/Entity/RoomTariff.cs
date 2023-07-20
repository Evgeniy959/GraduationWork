using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdmin.Models.Entity
{
    public class RoomTariff
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int TariffPlanId { get; set; }
        public int Price { get; set; }
        
        // навигационные свойства
        public Room? Room { get; set; }
        public TariffPlan? TariffPlan { get; set; }
        
    }
}
