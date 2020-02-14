using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadaAcademy.DataModels
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserName { get; set; }

        public ICollection<ItemRoute> ItemRoutes { get; set; }
        
    }
}
