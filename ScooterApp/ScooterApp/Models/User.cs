using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double Points { get; set; }
        public int LevelId { get; set; }
        public bool Role { get; set; } = false;
    }
}
