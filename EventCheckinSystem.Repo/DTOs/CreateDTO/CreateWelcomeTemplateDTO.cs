using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.CreateDTO
{
    public class CreateWelcomeTemplateDTOValidator : AbstractValidator<CreateWelcomeTemplateDTO>
    {
        public CreateWelcomeTemplateDTOValidator()
        {
            RuleFor(model => model.GuestGroupID)
                .NotEmpty().WithMessage("ID không được bỏ trống!");

            RuleFor(model => model.TemplateContent)
                .NotEmpty().WithMessage("Nội dung không được bỏ trống!");

            RuleFor(model => model.GuestGroupName)
                .NotEmpty().WithMessage("Tên nhóm không được bỏ trống!");

            RuleFor(model => model.ImageURL)
                .NotEmpty().WithMessage("URL cho avatar không được bỏ trống!");
        }
    }
    public class CreateWelcomeTemplateDTO
    {
        public int GuestGroupID { get; set; }
        public string TemplateContent { get; set; }
        public string GuestGroupName { get; set; }
        public string ImageURL { get; set; }
    }
}