namespace Juno.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {

        private JunoDBContext dbContext;

        public JunoDBContext Init()
        {
            return dbContext ?? (dbContext = new JunoDBContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

    }
}