namespace HotelAdmin.Models.Entity
{
    public class RoomTariffAdmin
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int TariffPlanId { get; set; }
        public int Price { get; set; }

        // навигационные свойства
        public Category? Category { get; set; }
        public TariffPlan? TariffPlan { get; set; }
    }
}
