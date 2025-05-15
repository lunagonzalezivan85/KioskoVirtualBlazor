namespace Core.Domain.Entities
{
    public class BranchMenuItem
    {
        public int BranchId { get; set; }
        public int MenuItemId { get; set; }
        public bool IsActive { get; set; }
        public decimal? SpecialPrice { get; set; }
        
        // Relaciones
        public virtual Branch Branch { get; set; }
        public virtual MenuItem MenuItem { get; set; }
    }
}
