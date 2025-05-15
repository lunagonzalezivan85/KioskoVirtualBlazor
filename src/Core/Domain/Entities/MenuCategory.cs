using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class MenuCategory
    {
        public MenuCategory()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        
        // Relaciones
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
