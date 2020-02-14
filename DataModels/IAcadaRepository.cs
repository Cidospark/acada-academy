using System.Collections.Generic;
using System.Threading.Tasks;
using AcadaAcademy.ViewModels;

namespace AcadaAcademy.DataModels
{
    public interface IAcadaRepository
    {
        IEnumerable<Item> GetAllItems();

        Item GetItemByName(string itemName);

        Item GetUserItemByName(string itemName, string username);

        IEnumerable<Item> GetItemsByUsername(string username);

        void AddItem(Item item);

        void AddRoute(string itemName, ItemRoute newRoute, string username);

        Task<bool> SaveChangesAsync();
        
    }
}