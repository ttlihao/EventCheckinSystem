using EventCheckinSystem.Repo.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{

    public class UserEventDTOValidator : AbstractValidator<UserEventDTO>
    {
        public UserEventDTOValidator()
        {
            RuleFor(model => model.UserID)
                .NotEmpty().WithMessage("UserID không được bỏ trống!");

            RuleFor(model => model.EventID)
                .NotEmpty().WithMessage("Event ID không được bỏ trống!");
        }
    }
    public class UserEventDTO
    {
        public string UserID { get; set; }
        public int EventID { get; set; }   
    }
}