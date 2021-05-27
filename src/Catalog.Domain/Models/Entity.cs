using System;

namespace Catalog.Domain.Models
{
    public abstract class Entity 
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime? DisabledAt { get; set; }
    }
}