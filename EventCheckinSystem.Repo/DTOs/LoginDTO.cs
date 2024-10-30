using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(model => model.UserName)
                .NotEmpty().WithMessage("Username không được bỏ trống!")
                .MinimumLength(6).WithMessage("Username phải có ít nhất 6 ký tự.");

            RuleFor(model => model.Password)
                .NotEmpty().WithMessage("Mật khẩu không được bỏ trống!")
                .MinimumLength(8).WithMessage("Mật khẩu phải có ít nhất 8 ký tự.")
                .MaximumLength(20).WithMessage("Mật khẩu không được vượt quá 20 ký tự.")
                .Matches("[A-Z]").WithMessage("Mật khẩu phải chứa ít nhất 1 ký tự in hoa.")
                .Matches("[a-z]").WithMessage("Mật khẩu phải chứa ít nhất 1 ký tự thường.")
                .Matches("[0-9]").WithMessage("Mật khẩu phải chứa ít nhất 1 ký tự số.")
                .Matches("[!@#$%^&*]").WithMessage("Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt.");
        }
    }
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
