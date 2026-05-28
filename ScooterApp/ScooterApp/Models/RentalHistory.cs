using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterApp.Models
{
    internal class RentalHistory
    {
        public int RentalHistoryId { get; set; }
        public double Price { get; set; }
        public int Length { get; set; }
        public int ScooterId { get; set; }
        public Scooter Scooter { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
