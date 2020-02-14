using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadaAcademy.DataModels
{
    public class AcadaRepository : IAcadaRepository
    {
        private AcadaContext _context;
        private ILogger<AcadaRepository> _logger;

        /// <summary>
        /// Public constructor for instantiating class when injected into others
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public AcadaRepository(AcadaContext context, ILogger<AcadaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add a lost item or a title for an envrionmental hazard
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            _context.Add(item);
        }

        public void AddRoute(string itemName, ItemRoute newRoute, string userName)
        {
            var item = GetUserItemByName(itemName, userName);

            if(item != null)
            {
                item.ItemRoutes.Add(newRoute);
                _context.ItemRoutes.Add(newRoute);
            }
        }

        public IEnumerable<Item> GetAllItems()
        {
            _logger.LogInformation("Getting all items from database");
            return _context.Items.ToList();
        }

        public IEnumerable<Item> GetItemsByUsername(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Enumerable.Empty<Item>();
            }

            try
            {
                var items = _context.Items.Include(i => i.ItemRoutes).Where(i => i.UserName.Equals(userName)).ToList();
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get items by username", ex);
                throw;
            }
        }

        public Item GetUserItemByName(string itemName, string userName)
        {
            if (!String.IsNullOrWhiteSpace(itemName) && !String.IsNullOrWhiteSpace(userName))
            {
                return _context.Items
                               .Include(i => i.ItemRoutes)
                               .Where(i => itemName.Equals(i.Name) && userName.Equals(i.UserName))
                               .FirstOrDefault();
            }
            return null;
        }

        public Item GetItemByName(string itemName)
        {
            _logger.LogInformation("Getting Specific Item from database");
            return _context.Items.Include(i => i.ItemRoutes).Where(i => i.Name == itemName).FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
