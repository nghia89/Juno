namespace Juno.Data.Infrastructure
{
    internal class UnitOfWork:IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private JunoDBContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public JunoDBContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}