namespace Core.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string Comments { get; set; }
        
        // Relaciones
        public virtual Order Order { get; set; }
        public virtual MenuItem MenuItem { get; set; }
    }
}
