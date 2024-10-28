using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("Email không được bỏ trống!")
                .EmailAddress().WithMessage("Sai định dạng Email.");

            RuleFor(model => model.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại không được bỏ trống!")
                .Length(10).WithMessage("Số điện thoạt cần có 10 chữ số.");

            RuleFor(model => model.Username)
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
    public class UserDTO
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string RoleID { get; set; }
    }
}
