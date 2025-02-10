using FluentValidation;
using System.Text.RegularExpressions;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Validators
{
    public class ValidatorRequestProduk : AbstractValidator<ProdukRequestDTO>
    {
        public ValidatorRequestProduk()
        {
            RuleFor(x => x.NameProduk).NotEmpty().Must(ValidAlphanumeric).WithMessage("Name must contain only letters and numbers!");
        }

        // Validasi untuk memastikan hanya angka
        public bool ValidAttack(string attack)
        {
            string regexNumberOnly = @"^\d+$";
            if (Regex.IsMatch(attack, regexNumberOnly))
                return true;
            else
                return false;
        }

        // Validasi untuk memastikan hanya huruf
        public bool ValidLettersOnly(string nameSup)
        {
            string regexLettersOnly = @"^[a-zA-Z\s]+$"; // Regex untuk memastikan hanya huruf (termasuk spasi)
            return Regex.IsMatch(nameSup, regexLettersOnly);
        }

        // Validasi untuk memastikan hanya angka dan huruf
        public bool ValidAlphanumeric(string nameBrg)
        {
            string regexAlphanumeric = @"^[a-zA-Z0-9\s]+$"; // Regex untuk memastikan hanya angka dan huruf (termasuk spasi)
            return Regex.IsMatch(nameBrg, regexAlphanumeric);
        }

        //Validasi Hanya Angka
        public bool ValidNumberOnly(string value)
        {
            string regexNumberOnly = @"^\d+$";
            return Regex.IsMatch(value, regexNumberOnly);
        }
    }
}
