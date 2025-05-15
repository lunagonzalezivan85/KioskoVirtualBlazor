using System;
using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        
        // Relaciones
        public virtual Branch Branch { get; set; }
    }
}
