using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Interfaces;

namespace Core.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IGenericRepository<Order> orderRepository,
            IGenericRepository<OrderDetail> orderDetailRepository,
            IGenericRepository<MenuItem> menuItemRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByBranchAsync(int branchId)
        {
            var orders = await _orderRepository.FindAsync(o => o.BranchId == branchId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(int branchId, OrderStatus status)
        {
            var orders = await _orderRepository.FindAsync(o => o.BranchId == branchId && o.Status == status);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.CreatedAt = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;

            // Calculate total amount and create order details
            decimal totalAmount = 0;
            var orderDetails = new List<OrderDetail>();

            foreach (var detail in orderDto.OrderDetails)
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(detail.MenuItemId);
                if (menuItem == null)
                    throw new Exception($"MenuItem with id {detail.MenuItemId} not found");

                var orderDetail = new OrderDetail
                {
                    MenuItemId = detail.MenuItemId,
                    Quantity = detail.Quantity,
                    UnitPrice = menuItem.Price,
                    Subtotal = menuItem.Price * detail.Quantity,
                    Comments = detail.Comments
                };

                totalAmount += orderDetail.Subtotal;
                orderDetails.Add(orderDetail);
            }

            order.TotalAmount = totalAmount;
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            // Add order details
            foreach (var detail in orderDetails)
            {
                detail.OrderId = order.OrderId;
                await _orderDetailRepository.AddAsync(detail);
            }
            await _orderDetailRepository.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusDto updateStatusDto)
        {
            var order = await _orderRepository.GetByIdAsync(updateStatusDto.OrderId);
            if (order == null)
                return false;

            order.Status = updateStatusDto.NewStatus;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDto>> GetTodaysOrdersAsync(int branchId)
        {
            var today = DateTime.UtcNow.Date;
            var orders = await _orderRepository.FindAsync(o => 
                o.BranchId == branchId && 
                o.CreatedAt.Date == today);
            
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> CancelOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception($"Order with id {orderId} not found");

            if (order.Status != OrderStatus.Pending)
                throw new Exception("Can only cancel pending orders");

            order.Status = OrderStatus.Cancelled;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }
    }
}
