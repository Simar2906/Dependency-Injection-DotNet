using DI_Project.Models;

namespace DI_Project.Services
{
    public interface IValidationChecker
    {
        bool ValidatorLogic(CreditApplication model);
        string ErrorMessage { get;}
    }
}
