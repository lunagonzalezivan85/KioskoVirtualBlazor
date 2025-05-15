using System;
using System.Collections.Generic;
using Core.Domain.Enums;

namespace Core.Application.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int? TableNumber { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }

    public class OrderDetailDto
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string Comments { get; set; }
    }

    public class CreateOrderDto
    {
        public int BranchId { get; set; }
        public int? TableNumber { get; set; }
        public string Comments { get; set; }
        public List<CreateOrderDetailDto> OrderDetails { get; set; }
    }

    public class CreateOrderDetailDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public string Comments { get; set; }
    }

    public class UpdateOrderStatusDto
    {
        public int OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }
}
