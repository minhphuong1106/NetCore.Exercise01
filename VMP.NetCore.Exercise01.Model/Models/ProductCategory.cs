using System;
using System.ComponentModel.DataAnnotations;
using VMP.NetCore.Exercise01.Model.Abstract;

namespace VMP.NetCore.Exercise01.Model.Models
{
    public class ProductCategory : BaseEntity
    {
        [StringLength(256)]
        public string Name { get; set; }

        public Nullable<Guid> CategoryId { get; set; }
        public ProductCategory Category { get; set; }
    }
}