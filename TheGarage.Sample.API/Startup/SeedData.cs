namespace TheGarage.Sample
{
    using Core.Contexts;
    using Core.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public static class SeedData
    {

        public static readonly string IP_ADDRESS_1 = "8.8.8.8";
        public static readonly string IP_ADDRESS_2 = "8.8.4.4";

        public static void PopulateTestData(ApplicationDbContext dbContext)
        {
            dbContext.GarageOwners.AddRange(Enumerable.Range(1, 3).Select(s => new GarageOwner()
            {
                FullName = $"Garage Owner {s}",
                Garage = new Garage()
                {
                    Name = $"Garage {s}",
                    Address = $"Amsterdamseweg {s}",
                    GarageDoors = new List<GarageDoor>() {
                        new GarageDoor(){ IpAddress = IP_ADDRESS_1},
                        new GarageDoor(){ IpAddress = IP_ADDRESS_2},
                    }
                }
            }));
            dbContext.SaveChanges();
        }
    }
}
