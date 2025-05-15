using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Branch
    {
        public Branch()
        {
            BranchMenuItems = new HashSet<BranchMenuItem>();
            Orders = new HashSet<Order>();
            Users = new HashSet<User>();
        }

        public int BranchId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Relaciones
        public virtual ICollection<BranchMenuItem> BranchMenuItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
