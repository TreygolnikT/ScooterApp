using System.Linq;

namespace ScooterApp.Models
{
    internal static class DbSeeder
    {
        public static void Seed()
        {
            using var db = new AppContext();

            if (!db.Roles.Any())
            {
                db.Roles.AddRange(
                    new Role { RoleId = 1, Name = "Admin" },
                    new Role { RoleId = 2, Name = "User" }
                );
            }

            if (!db.Levels.Any())
            {
                db.Levels.AddRange(
                    new Level { LevelId = 1, Name = "Новичок", Percent = 92 },
                    new Level { LevelId = 2, Name = "Ветеренар", Percent = 67 }
                );
            }

            if (!db.Coords.Any())
            {
                db.Coords.AddRange(
                    new Coord { CoordId = 1, X = 54.98, Y = 82.89, Z = 1.0 },
                    new Coord { CoordId = 2, X = 55.03, Y = 82.92, Z = 1.0 },
                    new Coord { CoordId = 3, X = 55.01, Y = 82.87, Z = 1.0 }
                );
            }

            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new User
                    {
                        UserId = 1,
                        Username = "admin",
                        Password = "admin",
                        Points = 0,
                        Money = 1000,
                        LevelId = 2,
                        RoleId = 1
                    },
                    new User
                    {
                        UserId = 2,
                        Username = "user",
                        Password = "user",
                        Points = 0,
                        Money = 500,
                        LevelId = 1,
                        RoleId = 2
                    }
                );
            }

            if (!db.Scooters.Any())
            {
                db.Scooters.AddRange(
                    new Scooter
                    {
                        ScooterId = 1,
                        Model = "Econom Lite",
                        Charge = 90,
                        CoordId = 1
                    },
                    new Scooter
                    {
                        ScooterId = 2,
                        Model = "City Pro",
                        Charge = 75,
                        CoordId = 2
                    },
                    new Scooter
                    {
                        ScooterId = 3,
                        Model = "Ultra Max",
                        Charge = 60,
                        CoordId = 3
                    }
                );
            }

            db.SaveChanges();
        }
    }
}