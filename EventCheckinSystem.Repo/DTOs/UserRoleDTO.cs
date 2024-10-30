using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class UserRoleDTOValidator : AbstractValidator<UserRoleDTO>
    {
        public UserRoleDTOValidator()
        {
            RuleFor(model => model.UserId)
                .NotEmpty().WithMessage("ID không được bỏ trống!");

            RuleFor(model => model.RoleId)
                .NotEmpty().WithMessage("RoleID không được bỏ trống!");
        }
    }
    public class UserRoleDTO
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
