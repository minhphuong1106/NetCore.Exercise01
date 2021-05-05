using System;
using System.Collections.Generic;
using System.Text;

namespace VMP.NetCore.Exercise01.Model.Abstract
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        int OrderNo { get; set; }
        bool IsDeleted { get; set; }
        DateTime InsAt { get; set; }
        public string InsBy { get; set; }
        public DateTime UpdAt { get; set; }
        public string UpdBy { get; set; }
    }
}
