using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Infrastructure.Persistence
{
    public static class InMemoryData
    {
        public static List<Branch> Branches { get; } = new List<Branch>
        {
            new Branch
            {
                BranchId = 1,
                Name = "Sucursal Central",
                Address = "Avenida Central #123",
                ContactInfo = "2222-1111",
                IsActive = true,
                CreatedAt = DateTime.Now.AddDays(-30)
            },
            new Branch
            {
                BranchId = 2,
                Name = "Sucursal Norte",
                Address = "Plaza Norte Local #45",
                ContactInfo = "2222-2222",
                IsActive = true,
                CreatedAt = DateTime.Now.AddDays(-15)
            }
        };

        public static List<MenuCategory> Categories { get; } = new List<MenuCategory>
        {
            new MenuCategory
            {
                CategoryId = 1,
                Name = "Entradas",
                Description = "Aperitivos y entradas",
                DisplayOrder = 1,
                IsActive = true
            },
            new MenuCategory
            {
                CategoryId = 2,
                Name = "Platos Principales",
                Description = "Platos fuertes",
                DisplayOrder = 2,
                IsActive = true
            },
            new MenuCategory
            {
                CategoryId = 3,
                Name = "Postres",
                Description = "Postres y dulces",
                DisplayOrder = 3,
                IsActive = true
            }
        };

        public static List<MenuItem> MenuItems { get; } = new List<MenuItem>
        {
            new MenuItem
            {
                MenuItemId = 1,
                Name = "Ensalada César",
                Description = "Lechuga romana, crutones, queso parmesano y aderezo césar",
                Price = 5500,
                CategoryId = 1,
                ImageUrl = "/images/caesar-salad.jpg",
                IsAvailable = true
            },
            new MenuItem
            {
                MenuItemId = 2,
                Name = "Filete de Res",
                Description = "Filete de res a la parrilla con guarnición",
                Price = 12500,
                CategoryId = 2,
                ImageUrl = "/images/steak.jpg",
                IsAvailable = true
            },
            new MenuItem
            {
                MenuItemId = 3,
                Name = "Tiramisú",
                Description = "Postre italiano tradicional",
                Price = 4500,
                CategoryId = 3,
                ImageUrl = "/images/tiramisu.jpg",
                IsAvailable = true
            }
        };

        public static List<Order> Orders { get; } = new List<Order>
        {
            new Order
            {
                OrderId = 1,
                BranchId = 1,
                TableNumber = 1,
                Status = OrderStatus.Pending,
                TotalAmount = 18000,
                CreatedAt = DateTime.Now.AddHours(-2),
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        OrderDetailId = 1,
                        OrderId = 1,
                        MenuItemId = 1,
                        Quantity = 1,
                        UnitPrice = 5500,
                        Subtotal = 5500
                    },
                    new OrderDetail
                    {
                        OrderDetailId = 2,
                        OrderId = 1,
                        MenuItemId = 2,
                        Quantity = 1,
                        UnitPrice = 12500,
                        Subtotal = 12500
                    }
                }
            },
            new Order
            {
                OrderId = 2,
                BranchId = 1,
                TableNumber = 2,
                Status = OrderStatus.InKitchen,
                TotalAmount = 17000,
                CreatedAt = DateTime.Now.AddHours(-1),
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        OrderDetailId = 3,
                        OrderId = 2,
                        MenuItemId = 2,
                        Quantity = 1,
                        UnitPrice = 12500,
                        Subtotal = 12500
                    },
                    new OrderDetail
                    {
                        OrderDetailId = 4,
                        OrderId = 2,
                        MenuItemId = 3,
                        Quantity = 1,
                        UnitPrice = 4500,
                        Subtotal = 4500
                    }
                }
            }
        };
    }
}
