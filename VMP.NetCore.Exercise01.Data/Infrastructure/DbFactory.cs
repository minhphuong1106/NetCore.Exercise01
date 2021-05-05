namespace VMP.NetCore.Exercise01.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ProjectDbContext dbContext;

        public ProjectDbContext Init()
        {
            return dbContext ?? (dbContext = new ProjectDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}