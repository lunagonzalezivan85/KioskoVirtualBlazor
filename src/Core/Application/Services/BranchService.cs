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
    public class BranchService : IBranchService
    {
        private readonly IGenericRepository<Branch> _branchRepository;
        private readonly IGenericRepository<BranchMenuItem> _branchMenuItemRepository;
        private readonly IGenericRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;

        public BranchService(
            IGenericRepository<Branch> branchRepository,
            IGenericRepository<BranchMenuItem> branchMenuItemRepository,
            IGenericRepository<MenuItem> menuItemRepository,
            IMapper mapper)
        {
            _branchRepository = branchRepository;
            _branchMenuItemRepository = branchMenuItemRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<BranchDto> GetBranchByIdAsync(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            return _mapper.Map<BranchDto>(branch);
        }

        public async Task<IEnumerable<BranchDto>> GetAllBranchesAsync()
        {
            var branches = await _branchRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BranchDto>>(branches);
        }

        public async Task<BranchDto> CreateBranchAsync(CreateBranchDto branchDto)
        {
            var branch = _mapper.Map<Branch>(branchDto);
            branch.CreatedAt = DateTime.UtcNow;
            branch.IsActive = true;

            await _branchRepository.AddAsync(branch);
            await _branchRepository.SaveChangesAsync();

            return _mapper.Map<BranchDto>(branch);
        }

        public async Task UpdateBranchAsync(UpdateBranchDto branchDto)
        {
            var branch = await _branchRepository.GetByIdAsync(branchDto.BranchId);
            if (branch == null)
                throw new Exception($"Branch with id {branchDto.BranchId} not found");

            _mapper.Map(branchDto, branch);
            await _branchRepository.UpdateAsync(branch);
            await _branchRepository.SaveChangesAsync();
        }

        public async Task DeleteBranchAsync(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
                throw new Exception($"Branch with id {id} not found");

            await _branchRepository.DeleteAsync(branch);
            await _branchRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MenuItemDto>> GetBranchMenuAsync(int branchId)
        {
            var branchMenuItems = await _branchMenuItemRepository.FindAsync(
                bm => bm.BranchId == branchId && bm.IsActive);

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

        public async Task<bool> ToggleBranchStatusAsync(int branchId)
        {
            var branch = await _branchRepository.GetByIdAsync(branchId);
            if (branch == null)
                return false;

            branch.IsActive = !branch.IsActive;
            await _branchRepository.UpdateAsync(branch);
            await _branchRepository.SaveChangesAsync();
            return true;
        }
    }
}
