using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.DTOs;

namespace Core.Application.Interfaces
{
    public interface IBranchService
    {
        Task<BranchDto> GetBranchByIdAsync(int id);
        Task<IEnumerable<BranchDto>> GetAllBranchesAsync();
        Task<BranchDto> CreateBranchAsync(CreateBranchDto branchDto);
        Task UpdateBranchAsync(UpdateBranchDto branchDto);
        Task DeleteBranchAsync(int id);
        Task<IEnumerable<MenuItemDto>> GetBranchMenuAsync(int branchId);
        Task<bool> ToggleBranchStatusAsync(int branchId);
    }
}
