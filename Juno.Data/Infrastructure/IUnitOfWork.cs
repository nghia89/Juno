namespace Juno.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}