using System;

namespace Core.Application.DTOs
{
    public class BranchDto
    {
        public int BranchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateBranchDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
    }

    public class UpdateBranchDto
    {
        public int BranchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
