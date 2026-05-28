using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ScooterApp.Models
{
    internal class Scooter
    {
        public int ScooterId { get; set; }
        public string Model { get; set; }
        public int Charge { get; set; }
        public int CoordsId { get; set; }
        public Coord Coord { get; set; }
        public List<RentalHistory> RentalHistories { get; set; }
    }
}
