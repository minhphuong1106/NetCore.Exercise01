using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VMP.NetCore.Exercise01.Common;

namespace VMP.NetCore.Exercise01.Model.Abstract
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.Empty;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNo { get; set; } = 1;
        public bool IsDeleted { get; set; } = false;
        public DateTime InsAt { get; set; } = DateTime.UtcNow;
        [StringLength(128)]
        public string InsBy { get; set; } = ProjectConstants.DBConstants.DefaultUser;
        public DateTime UpdAt { get; set; } = DateTime.UtcNow;
        [StringLength(128)]
        public string UpdBy { get; set; } = ProjectConstants.DBConstants.DefaultUser;
    }
}
