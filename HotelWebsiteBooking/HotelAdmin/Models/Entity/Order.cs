﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdmin.Models.Entity
{
    public class Order
    {
        [Key]
        [Column("Number")]
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }

        public Client? Client { get; set; }
    }
}
