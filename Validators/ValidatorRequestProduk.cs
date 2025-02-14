using FluentValidation;
using System.Text.RegularExpressions;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Validators
{
    public class ValidatorRequestProduk : AbstractValidator<ProdukRequestDTO>
    {
        public ValidatorRequestProduk()
        {

            RuleFor(x => x.NameProduk)
                .NotEmpty()
                .MinimumLength(3)
              .MaximumLength(100)
              .Matches(@"^[a-zA-Z0-9\s]+$")
              .WithMessage("Nama produk harus antara 3-100 karakter dan boleh mengandung huruf, angka, dan spasi");

           RuleFor(x => x.Supplier)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50)
                .Must(ValidLettersOnly)
                .WithMessage("Nama supplier harus antara 3-50 karakter dan hanya boleh mengandung huruf dan spasi");

            RuleFor(x => x.TanggalKadalwarsa)
                .NotEmpty()
                .Must(BeValidExpiryDate)
                .WithMessage("Tanggal kadaluarsa harus lebih dari tanggal hari ini");

            RuleFor(x => x.Harga)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(1000000000)
                .WithMessage("Harga harus lebih besar dari 0 dan kurang dari 1 miliar");

        }


        private bool ValidLettersOnly(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            string regexLettersOnly = @"^[a-zA-Z\s]+$";
            return Regex.IsMatch(text, regexLettersOnly);
        }

        private bool BeValidExpiryDate(DateTime date)
        {
            return date.Date > DateTime.Now.Date;
        }


    }
}
