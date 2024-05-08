using DI_Project.Models;

namespace DI_Project.Services
{
    public class CreditApprovedLow : ICreditApproved
    {
        public double GetCreditApproved(CreditApplication creditApplication)
        {
            //have logic to determine credit limit
            //hardcoded to 50 percent of salary
            return creditApplication.Salary * 0.5;
        }
    }
}
