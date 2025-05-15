using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
