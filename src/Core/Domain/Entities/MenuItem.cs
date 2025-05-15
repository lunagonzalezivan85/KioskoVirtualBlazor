using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class MenuItem
    {
        public MenuItem()
        {
            BranchMenuItems = new HashSet<BranchMenuItem>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
        
        // Relaciones
        public virtual MenuCategory Category { get; set; }
        public virtual ICollection<BranchMenuItem> BranchMenuItems { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
