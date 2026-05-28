using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ScooterApp.Models
{
    internal class User
    {
        public int UserId { get; set; }
        [MinLength(3)]
        public string Username { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
        public double Points { get; set; }
        public double Money { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<RentalHistory> RentalHistories { get; set; }
    }
}
