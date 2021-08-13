using System;

namespace Core.Business.Models
{
    public class Product : Entity
    {
        public Guid SupplierId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; } 
        public string Image { get; set; }
        public decimal Price { get; set; }
        public DateTime DateRegister { get; set; }
        public bool IsActive { get; set; }

        /* EF Relations */
        public Supplier Supplier { get; set; }
    }
}