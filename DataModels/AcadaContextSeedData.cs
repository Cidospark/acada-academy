using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadaAcademy.DataModels
{
    public class AcadaContextSeedData
    {
        private AcadaContext _context;
        private UserManager<AcadaUser> _userManager;

        public AcadaContextSeedData(AcadaContext context, UserManager<AcadaUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            //Ensure database is created
            _context.Database.EnsureCreated();

            //check to see if test user exists, then create it if not
            if (await _userManager.FindByEmailAsync("example@example.com") == null)
            {

                var user = new AcadaUser()
                {
                    UserName = "newuser",
                    Email = "example@example.com"
                };

                await _userManager.CreateAsync(user, "NewUser123!");
            }

            //check to see if test items has been posted or post it
            if (!_context.Items.Any())
            {
                var firstItem = new Item()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Eastside Disaster",
                    UserName = "newuser",
                    ItemRoutes = new List<ItemRoute>()
                    {
                        new ItemRoute() {  Location = "New York, NY", Description = "Sewage Leak", ActivityTime = new DateTime(2014, 6, 9), Latitude = 40.712784, Longitude = -74.005941, Order = 1 },
                        new ItemRoute() {  Location = "Boston, MA", Description = "Flooded Road", ActivityTime = new DateTime(2014, 7, 1), Latitude = 42.360082, Longitude = -71.058880, Order = 2 },
                    }
                };

                _context.Items.Add(firstItem);
                _context.ItemRoutes.AddRange(firstItem.ItemRoutes);

                var secondItem = new Item()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "Concurrent Issues",
                    UserName = "newuser",
                    ItemRoutes = new List<ItemRoute>()
                    {
                        new ItemRoute() { Order = 1, Latitude = 34.0534896850586, Longitude = -118.245323181152, Location = "Los Angeles, CA", Description = "Bad Pot Hole", ActivityTime = DateTime.Parse("Jun 4, 2014") },
                        new ItemRoute() { Order = 2, Latitude = 40.7145500183105, Longitude = -74.0071411132813, Location = "New York, NY", Description = "Mud Slide", ActivityTime = DateTime.Parse("Jun 25, 2014") },
                    }
                };

                _context.Items.Add(secondItem);
                _context.ItemRoutes.AddRange(secondItem.ItemRoutes);

                await _context.SaveChangesAsync();
            }
        }
    }
}
