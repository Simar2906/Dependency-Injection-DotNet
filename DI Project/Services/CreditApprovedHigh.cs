using DI_Project.Models;

namespace DI_Project.Services
{
    public class CreditApprovedHigh : ICreditApproved
    {
        public double GetCreditApproved(CreditApplication creditApplication)
        {
            //have logic to determine credit limit
            //hardcoded to 50 percent
            return creditApplication.Salary * 0.3;
        }
    }
}
