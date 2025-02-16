using FluentValidation;
using UTS_Project_Efengsi_Rahmanto_Zalukhu.Models.DTO;

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Validators
{
    public class ValidatorRegisterRequest : AbstractValidator<RegisterRequestDTO>
    {
        public ValidatorRegisterRequest()
        {
            // Validasi Username
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.");

            // Validasi Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");


            // Validasi Role
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == "Admin" || role == "User").WithMessage("Role must be either 'Admin' or 'User'.");
        }

    }
}
