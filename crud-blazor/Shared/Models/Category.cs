using System;
using System.Collections.Generic;
using System.Text;

namespace crud_blazor.Shared.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
