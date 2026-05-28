using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterApp.Models
{
    public class Scooter
    {
        public int ScooterId { get; set; }
        public string Model { get; set; }
        public int CoordsId { get; set; }
        public int Charge { get; set; }
    }
}
