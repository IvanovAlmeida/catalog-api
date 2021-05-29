namespace Catalog.Domain.Models
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public ItemType ItemType { get; set; }
        public decimal Value { get; set; }
    }
}
