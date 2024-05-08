using DI_Project.Models;

namespace DI_Project.Services
{
    public class AddressValidationChecker : IValidationChecker
    {
        public string ErrorMessage => "Location Validation Failed";

        public bool ValidatorLogic(CreditApplication model)
        {
            if(model.PostalCode <=0 || model.PostalCode >= 99999)
            {
                return false;
            }
            return true;
        }
    }
}
