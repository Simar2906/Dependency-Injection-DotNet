using DI_Project.Data.Repository.IRepository;
using DI_Project.Models;

namespace DI_Project.Data.Repository
{
    public class CreditApplicationRepository : Repository<CreditApplication>, ICreditApplicationRepository
    {
        private readonly ApplicationDbContext _db;

        public CreditApplicationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CreditApplication obj)
        {
            _db.CreditApplicationModel.Update(obj);
        }
    }
}
