using System;
using System.Collections.Generic;

namespace Core.Business.Models
{
    public class Category : Entity
    {
        public Guid? CategoryId { get; set; } 
        public string Name { get; set; }


        /* EF Relation */
        public Category ParentCategory { get; set; }
        public IEnumerable<Category> ParentCategories { get; set; }
    }
}
