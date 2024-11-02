using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.CreateDTO
{
    public class CreateGuestImageDTOValidator : AbstractValidator<CreateGuestImageDTO>
    {
        public CreateGuestImageDTOValidator()
        {
            RuleFor(model => model.GuestID)
                .NotEmpty().WithMessage("GuestID không được bỏ trống!");

            RuleFor(model => model.ImageURL)
                .NotEmpty().WithMessage("Image URL không được bỏ trống!");
        }
    }
    public class CreateGuestImageDTO
    {
        public int GuestID { get; set; }
        public string ImageURL { get; set; }
    }
}
