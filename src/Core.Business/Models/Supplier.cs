using System;
using System.Collections.Generic;

namespace Core.Business.Models
{
    public class Supplier : Entity
    {
        public Guid AddressId { get; set; } 
        public string Name { get; set; }
        public string Document { get; set; }
        public TypeSupplier TypeSupplier { get; set; }
        public bool IsActive { get; set; }

        /* EF Relations */
        public IEnumerable<Product> Products { get; set; }

        public Address Address { get; set; }
    }
}