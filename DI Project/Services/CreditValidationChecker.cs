using DI_Project.Models;

namespace DI_Project.Services
{
    public class CreditValidationChecker : IValidationChecker
    {
        public string ErrorMessage => "You didn't meet Age/Salary/Credit Requirements.";

        public bool ValidatorLogic(CreditApplication model)
        {
            if (DateTime.Now.AddYears(-18) < model.DOB)
            {
                return false;
            }
            if(model.Salary < 10000)
            {
                return false;
            }
            return true;
        }
    }
}
