using DI_Project.Models;

namespace DI_Project.Services
{
    public interface ICreditApproved
    {
        double GetCreditApproved(CreditApplication creditApplication);
    }
}
