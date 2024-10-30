using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class GuestImageDTOValidator : AbstractValidator<GuestImageDTO>
    {
        public GuestImageDTOValidator()
        {
            RuleFor(model => model.GuestID)
                .NotEmpty().WithMessage("GuestID không được bỏ trống!");

            RuleFor(model => model.ImageURL)
                .NotEmpty().WithMessage("Image URL không được bỏ trống!");
        }
    }
    public class GuestImageDTO
    {
        public int GuestImageID { get; set; }
        public int GuestID { get; set; }
        public string ImageURL { get; set; }
    }
}
