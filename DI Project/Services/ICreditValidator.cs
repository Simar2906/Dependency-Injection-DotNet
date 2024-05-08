using DI_Project.Models;

namespace DI_Project.Services
{
    public interface ICreditValidator
    {
        Task<(bool, IEnumerable<string>)> PassAllValidations(CreditApplication model);
    }
}
