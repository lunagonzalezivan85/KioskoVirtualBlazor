using System;
using System.Collections.Generic;
using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }

        public int OrderId { get; set; }
        public int BranchId { get; set; }
        public int? TableNumber { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Relaciones
        public virtual Branch Branch { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
