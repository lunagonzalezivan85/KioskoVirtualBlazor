using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.DTOs;

namespace Core.Application.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuCategoryDto>> GetAllCategoriesAsync();
        Task<IEnumerable<MenuItemDto>> GetAllMenuItemsAsync();
        Task<IEnumerable<MenuItemDto>> GetMenuItemsByCategoryAsync(int categoryId);
        Task<IEnumerable<MenuItemDto>> GetMenuItemsByBranchAsync(int branchId);
        Task<MenuItemDto> GetMenuItemByIdAsync(int id);
        Task<MenuItemDto> CreateMenuItemAsync(CreateMenuItemDto menuItemDto);
        Task UpdateMenuItemAsync(UpdateMenuItemDto menuItemDto);
        Task DeleteMenuItemAsync(int id);
        Task<bool> AssignMenuItemToBranchAsync(int branchId, int menuItemId);
        Task<bool> RemoveMenuItemFromBranchAsync(int branchId, int menuItemId);
    }
}
