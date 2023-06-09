﻿namespace HotelAdmin.Models.Entity
{
    public class TariffPlan
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public ICollection<RoomTariff>? Tariffs { get; set; }
        public ICollection<RoomTariffAdmin>? TariffAdmins { get; set; }
    }
}
