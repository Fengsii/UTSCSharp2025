using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Validators
{
    public class ValidatorRequestPesanan : AbstractValidator<PesananRequestDTO>
    {
       
        public ValidatorRequestPesanan()
        {
            RuleFor(x => x.NamePembeli)
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(50)
               .Must(ValidLettersOnly)
               .WithMessage("Name must contain only letters, be between 3-50 characters");


            RuleFor(x => x.AlamatPembeli)
            .Must((instance, value) =>
            {
                if (string.IsNullOrEmpty(value))
                {
                    instance.AlamatPembeli = "Bandung";
                }
                return true;
            });

            RuleFor(x => x.JumlaH)
               .NotEmpty()
               .GreaterThan(0)
               .LessThanOrEqualTo(1000)
               .WithMessage("Quantity must be between 1-1000");

        }

        private bool ValidLettersOnly(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            string regexLettersOnly = @"^[a-zA-Z\s]+$";
            return Regex.IsMatch(name, regexLettersOnly);
        }





    }
}
