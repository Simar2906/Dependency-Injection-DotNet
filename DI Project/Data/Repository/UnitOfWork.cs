using DI_Project.Data.Repository.IRepository;
using DI_Project.Models;

namespace DI_Project.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CreditApplicationRepository = new CreditApplicationRepository(_db);
        }
        public ICreditApplicationRepository CreditApplicationRepository { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
