﻿using DI_Project.Models;

namespace DI_Project.Services
{
    public class CreditValidator : ICreditValidator
    {
        private readonly IEnumerable<IValidationChecker> _validations;
        public CreditValidator(IEnumerable<IValidationChecker> validations)
        {
            _validations = validations;
        }

        public async Task<(bool, IEnumerable<string>)> PassAllValidations(CreditApplication model)
        {
            bool validationsPassed = true;
            List<string> errorMessages = new List<string>();

            foreach(var validation in _validations)
            {
                if (!validation.ValidatorLogic(model))
                {
                    errorMessages.Add(validation.ErrorMessage);
                    validationsPassed = false;
                }
            }

            return (validationsPassed, errorMessages);
        }
    }
}
