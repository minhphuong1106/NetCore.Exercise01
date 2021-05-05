using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VMP.NetCore.Exercise01.Model.Abstract;

namespace VMP.NetCore.Exercise01.Model.Models
{
    public class Product : BaseEntity
    {
        [StringLength(256)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}