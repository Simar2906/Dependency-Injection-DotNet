namespace DI_Project.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICreditApplicationRepository CreditApplicationRepository { get; }
        void Save();
    }
}
