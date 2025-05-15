using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.DTOs;
using Core.Domain.Enums;

namespace Core.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrdersByBranchAsync(int branchId);
        Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(int branchId, OrderStatus status);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto);
        Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusDto updateStatusDto);
        Task<IEnumerable<OrderDto>> GetTodaysOrdersAsync(int branchId);
        Task<OrderDto> CancelOrderAsync(int orderId);
    }
}
