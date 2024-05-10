using DI_Project.Models;

namespace DI_Project.Data.Repository.IRepository
{
    public interface ICreditApplicationRepository : IRepository<CreditApplication>
    {
        void Update(CreditApplication obj);
    }
}
