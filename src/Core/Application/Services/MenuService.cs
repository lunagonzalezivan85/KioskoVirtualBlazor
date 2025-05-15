using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IGenericRepository<MenuCategory> _categoryRepository;
        private readonly IGenericRepository<BranchMenuItem> _branchMenuItemRepository;
        private readonly IMapper _mapper;

        public MenuService(
            IGenericRepository<MenuItem> menuItemRepository,
            IGenericRepository<MenuCategory> categoryRepository,
            IGenericRepository<BranchMenuItem> branchMenuItemRepository,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _categoryRepository = categoryRepository;
            _branchMenuItemRepository = branchMenuItemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuCategoryDto>>(categories);
        }

        public async Task<IEnumerable<MenuItemDto>> GetAllMenuItemsAsync()
        {
            var menuItems = await _menuItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }

        public async Task<IEnumerable<MenuItemDto>> GetMenuItemsByCategoryAsync(int categoryId)
        {
            var menuItems = await _menuItemRepository.FindAsync(m => m.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }

        public async Task<IEnumerable<MenuItemDto>> GetMenuItemsByBranchAsync(int branchId)
        {
            var branchMenuItems = await _branchMenuItemRepository.FindAsync(bm => bm.BranchId == branchId && bm.IsActive);
            var menuItems = new List<MenuItem>();
            foreach (var branchMenuItem in branchMenuItems)
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(branchMenuItem.MenuItemId);
                if (menuItem != null && menuItem.IsAvailable)
                {
                    menuItems.Add(menuItem);
                }
            }
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }

        public async Task<MenuItemDto> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            return _mapper.Map<MenuItemDto>(menuItem);
        }

        public async Task<MenuItemDto> CreateMenuItemAsync(CreateMenuItemDto menuItemDto)
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            menuItem.IsAvailable = true;
            await _menuItemRepository.AddAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();
            return _mapper.Map<MenuItemDto>(menuItem);
        }

        public async Task UpdateMenuItemAsync(UpdateMenuItemDto menuItemDto)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(menuItemDto.MenuItemId);
            if (menuItem == null)
                throw new Exception($"MenuItem with id {menuItemDto.MenuItemId} not found");

            _mapper.Map(menuItemDto, menuItem);
            await _menuItemRepository.UpdateAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
                throw new Exception($"MenuItem with id {id} not found");

            await _menuItemRepository.DeleteAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();
        }

        public async Task<bool> AssignMenuItemToBranchAsync(int branchId, int menuItemId)
        {
            var exists = await _branchMenuItemRepository.ExistsAsync(
                bm => bm.BranchId == branchId && bm.MenuItemId == menuItemId);

            if (exists)
                return false;

            var branchMenuItem = new BranchMenuItem
            {
                BranchId = branchId,
                MenuItemId = menuItemId,
                IsActive = true
            };

            await _branchMenuItemRepository.AddAsync(branchMenuItem);
            await _branchMenuItemRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveMenuItemFromBranchAsync(int branchId, int menuItemId)
        {
            var branchMenuItem = await _branchMenuItemRepository.FindAsync(
                bm => bm.BranchId == branchId && bm.MenuItemId == menuItemId);

            if (!branchMenuItem.Any())
                return false;

            await _branchMenuItemRepository.DeleteAsync(branchMenuItem.First());
            await _branchMenuItemRepository.SaveChangesAsync();
            return true;
        }
    }
}
