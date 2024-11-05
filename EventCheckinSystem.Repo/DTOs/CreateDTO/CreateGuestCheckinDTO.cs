using FluentValidation;

namespace EventCheckinSystem.Repo.DTOs.CreateDTO
{
    public class CreateGuestCheckinDTOValidator : AbstractValidator<CreateGuestCheckinDTO>
    {
        public CreateGuestCheckinDTOValidator()
        {

            RuleFor(model => model.GuestID)
                .NotEmpty().WithMessage("GuestID không được bỏ trống!");


            RuleFor(model => model.Status)
                .NotEmpty().WithMessage("Status không được bỏ trống!")
                .Length(2, 50).WithMessage("Status phải có độ dài từ 2 đến 50 ký tự!");

            RuleFor(model => model.Notes)
                .MaximumLength(500).WithMessage("Notes không được vượt quá 500 ký tự!");
        }
    }

    public class CreateGuestCheckinDTO
    {
        public int GuestID { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
