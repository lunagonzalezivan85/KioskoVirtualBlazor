using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MockMenuService : IMenuService
    {
        public async Task<MenuCategoryDto> CreateCategoryAsync(CreateMenuCategoryDto createDto)
        {
            var category = new MenuCategory
            {
                CategoryId = InMemoryData.Categories.Max(c => c.CategoryId) + 1,
                Name = createDto.Name,
                Description = createDto.Description,
                DisplayOrder = createDto.DisplayOrder,
                IsActive = true
            };

            InMemoryData.Categories.Add(category);

            return new MenuCategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive
            };
        }

        public async Task<MenuItemDto> CreateMenuItemAsync(CreateMenuItemDto createDto)
        {
            var item = new MenuItem
            {
                MenuItemId = InMemoryData.MenuItems.Max(m => m.MenuItemId) + 1,
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price,
                CategoryId = createDto.CategoryId,
                ImageUrl = createDto.ImageUrl,
                IsAvailable = true
            };

            InMemoryData.MenuItems.Add(item);

            return new MenuItemDto
            {
                MenuItemId = item.MenuItemId,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CategoryId = item.CategoryId,
                CategoryName = InMemoryData.Categories.First(c => c.CategoryId == item.CategoryId).Name,
                ImageUrl = item.ImageUrl,
                IsAvailable = item.IsAvailable
            };
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = InMemoryData.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category != null)
            {
                InMemoryData.Categories.Remove(category);
            }
        }

        public async Task DeleteMenuItemAsync(int menuItemId)
        {
            var item = InMemoryData.MenuItems.FirstOrDefault(m => m.MenuItemId == menuItemId);
            if (item != null)
            {
                InMemoryData.MenuItems.Remove(item);
            }
        }

        public async Task<IEnumerable<MenuCategoryDto>> GetAllCategoriesAsync()
        {
            return InMemoryData.Categories.Select(c => new MenuCategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                DisplayOrder = c.DisplayOrder,
                IsActive = c.IsActive
            });
        }

        public async Task<IEnumerable<MenuItemDto>> GetAllMenuItemsAsync()
        {
            return InMemoryData.MenuItems.Select(m => new MenuItemDto
            {
                MenuItemId = m.MenuItemId,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                CategoryId = m.CategoryId,
                CategoryName = InMemoryData.Categories.First(c => c.CategoryId == m.CategoryId).Name,
                ImageUrl = m.ImageUrl,
                IsAvailable = m.IsAvailable
            });
        }

        public async Task<MenuCategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = InMemoryData.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category == null) return null;

            return new MenuCategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive
            };
        }

        public async Task<MenuItemDto> GetMenuItemByIdAsync(int menuItemId)
        {
            var item = InMemoryData.MenuItems.FirstOrDefault(m => m.MenuItemId == menuItemId);
            if (item == null) return null;

            return new MenuItemDto
            {
                MenuItemId = item.MenuItemId,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CategoryId = item.CategoryId,
                CategoryName = InMemoryData.Categories.First(c => c.CategoryId == item.CategoryId).Name,
                ImageUrl = item.ImageUrl,
                IsAvailable = item.IsAvailable
            };
        }

        public async Task<MenuCategoryDto> UpdateCategoryAsync(UpdateMenuCategoryDto updateDto)
        {
            var category = InMemoryData.Categories.FirstOrDefault(c => c.CategoryId == updateDto.CategoryId);
            if (category == null) return null;

            category.Name = updateDto.Name;
            category.Description = updateDto.Description;
            category.DisplayOrder = updateDto.DisplayOrder;
            category.IsActive = updateDto.IsActive;

            return new MenuCategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive
            };
        }

        public async Task UpdateMenuItemAsync(UpdateMenuItemDto updateDto)
        {
            var item = InMemoryData.MenuItems.FirstOrDefault(m => m.MenuItemId == updateDto.MenuItemId);
            if (item != null)
            {
                item.Name = updateDto.Name;
                item.Description = updateDto.Description;
                item.Price = updateDto.Price;
                item.CategoryId = updateDto.CategoryId;
                item.ImageUrl = updateDto.ImageUrl;
                item.IsAvailable = updateDto.IsAvailable;
            }
        }

        public async Task<IEnumerable<MenuItemDto>> GetMenuItemsByCategoryAsync(int categoryId)
        {
            return InMemoryData.MenuItems
                .Where(m => m.CategoryId == categoryId)
                .Select(m => new MenuItemDto
                {
                    MenuItemId = m.MenuItemId,
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    CategoryId = m.CategoryId,
                    CategoryName = InMemoryData.Categories.First(c => c.CategoryId == m.CategoryId).Name,
                    ImageUrl = m.ImageUrl,
                    IsAvailable = m.IsAvailable
                });
        }

        public async Task<IEnumerable<MenuItemDto>> GetMenuItemsByBranchAsync(int branchId)
        {
            var branchMenuItems = InMemoryData.BranchMenuItems
                .Where(bm => bm.BranchId == branchId)
                .Select(bm => bm.MenuItemId);

            return InMemoryData.MenuItems
                .Where(m => branchMenuItems.Contains(m.MenuItemId))
                .Select(m => new MenuItemDto
                {
                    MenuItemId = m.MenuItemId,
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    CategoryId = m.CategoryId,
                    CategoryName = InMemoryData.Categories.First(c => c.CategoryId == m.CategoryId).Name,
                    ImageUrl = m.ImageUrl,
                    IsAvailable = m.IsAvailable
                });
        }

        public async Task<bool> AssignMenuItemToBranchAsync(int branchId, int menuItemId)
        {
            var branchMenuItem = InMemoryData.BranchMenuItems
                .FirstOrDefault(bm => bm.BranchId == branchId && bm.MenuItemId == menuItemId);

            if (branchMenuItem != null)
                return false; // Ya está asignado

            InMemoryData.BranchMenuItems.Add(new BranchMenuItem
            {
                BranchId = branchId,
                MenuItemId = menuItemId
            });

            return true;
        }

        public async Task<bool> RemoveMenuItemFromBranchAsync(int branchId, int menuItemId)
        {
            var branchMenuItem = InMemoryData.BranchMenuItems
                .FirstOrDefault(bm => bm.BranchId == branchId && bm.MenuItemId == menuItemId);

            if (branchMenuItem == null)
                return false; // No estaba asignado

            InMemoryData.BranchMenuItems.Remove(branchMenuItem);
            return true;
        }
    }
}
