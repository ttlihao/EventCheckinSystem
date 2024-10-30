using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using FluentValidation;

public class OrganizationDTOValidator : AbstractValidator<OrganizationDTO>
{
    public OrganizationDTOValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty().WithMessage("Tên công ty không được bỏ trống!");
        RuleFor(model => model.PhoneNumber)
            .NotEmpty().WithMessage("Số điện thoại không được bỏ trống!");
        RuleFor(model => model.Email)
            .NotEmpty().WithMessage("Email không được bỏ trống!");
        RuleFor(model => model.Address)
            .NotEmpty().WithMessage("Địa chỉ không được bỏ trống!");
        RuleFor(model => model.Description)
            .NotEmpty().WithMessage("Miêu tả không được bỏ trống!");
        RuleFor(model => model.EstablishedDate)
            .NotEmpty().WithMessage("Ngày thành lập công ty không được bỏ trống!");
    }
}
public class OrganizationDTO
{
    public int OrganizationID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public DateTime EstablishedDate { get; set; }

}

