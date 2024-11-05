using EventCheckinSystem.Repo.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class GuestDTOValidator : AbstractValidator<GuestDTO>
    {
        public GuestDTOValidator()
        {
            RuleFor(model => model.GuestID)
                .NotEmpty().WithMessage("GuestID không được bỏ trống!");

            RuleFor(model => model.GuestGroupID)
                .NotEmpty().WithMessage("ID nhóm khách không được bỏ trống!");

            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("Tên không được bỏ trống!")
                .MinimumLength(2).WithMessage("Tên phải có ít nhất 2 ký tự!");

            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("Email không được bỏ trống!")
                .EmailAddress().WithMessage("Sai định dạng Email.");

            RuleFor(model => model.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại không được bỏ trống!")
                .Length(10).WithMessage("Số điện thoại cần có 10 chữ số.");

            RuleFor(model => model.Address)
                .NotEmpty().WithMessage("Địa chỉ không được bỏ trống!")
                .MinimumLength(8).WithMessage("Địa chỉ phải có ít nhất 8 ký tự!");

            RuleFor(model => model.Birthday)
                .NotEmpty().WithMessage("Ngày sinh không được bỏ trống!")
                .Must(date => date >= DateTime.Today).WithMessage("Ngày sinh không được trước ngày hôm nay!");
        }
    }

    public class GuestDTO
    {
        public int GuestID { get; set; }
        public int GuestGroupID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }

        public string? ImageURL { get; set; }    
    }
}
