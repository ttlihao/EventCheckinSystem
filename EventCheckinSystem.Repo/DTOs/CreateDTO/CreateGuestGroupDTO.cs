using EventCheckinSystem.Repo.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.CreateDTO
{
    public class CreateGuestGroupDTOValidator : AbstractValidator<CreateGuestGroupDTO>
    {
        public CreateGuestGroupDTOValidator()
        {

            RuleFor(model => model.OrganizationID)
                .NotEmpty().WithMessage("OrganizationID không được bỏ trống!")
                .GreaterThan(0).WithMessage("OrganizationID phải lớn hơn 0!");

            RuleFor(model => model.Type)
                .NotEmpty().WithMessage("Type không được bỏ trống!")
                .Length(2, 100).WithMessage("Type phải có độ dài từ 2 đến 100 ký tự!");

            RuleFor(model => model.EventID)
                .NotEmpty().WithMessage("EventID không được bỏ trống!")
                .GreaterThan(0).WithMessage("EventID phải lớn hơn 0!");

            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("Name không được bỏ trống!")
                .Length(2, 100).WithMessage("Name phải có độ dài từ 2 đến 100 ký tự!");
        }
    }

    public class CreateGuestGroupDTO
    {
        public int OrganizationID { get; set; }
        public string Type { get; set; }

        public int EventID { get; set; }
        public string Name { get; set; }
    }
}
