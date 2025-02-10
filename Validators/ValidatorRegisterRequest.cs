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

            // Validasi Password
            //RuleFor(x => x.Password)
            //    .NotEmpty().WithMessage("Password is required.")
            //    .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            //    .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            //    .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            //    .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            //    .Matches(@"[\!\@\#\$\%\^\&\*\(\)\_\+\-\=\[\]\{\}\;\:\'\""\,\<\.\>\/\?]").WithMessage("Password must contain at least one special character.");

            // Validasi Role
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == "Admin" || role == "User").WithMessage("Role must be either 'Admin' or 'User'.");
        }

    }
}
