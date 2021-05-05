using System;

namespace VMP.NetCore.Exercise01.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ProjectDbContext Init();
    }
}