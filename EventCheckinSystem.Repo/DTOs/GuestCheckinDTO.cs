﻿using FluentValidation;

namespace EventCheckinSystem.Repo.DTOs
{
    public class GuestCheckinDTOValidator : AbstractValidator<GuestCheckinDTO>
    {
        public GuestCheckinDTOValidator()
        {
            RuleFor(model => model.GuestCheckinID)
                .NotEmpty().WithMessage("GuestCheckinID không được bỏ trống!");

            RuleFor(model => model.GuestID)
                .NotEmpty().WithMessage("GuestID không được bỏ trống!");

            RuleFor(model => model.CheckinTime)
                .NotEmpty().WithMessage("CheckinTime không được bỏ trống!")
                .Must(date => date <= DateTime.Now).WithMessage("CheckinTime không thể là thời gian tương lai!");

            RuleFor(model => model.Status)
                .NotEmpty().WithMessage("Status không được bỏ trống!")
                .Length(2, 50).WithMessage("Status phải có độ dài từ 2 đến 50 ký tự!");

            RuleFor(model => model.Notes)
                .MaximumLength(500).WithMessage("Notes không được vượt quá 500 ký tự!");
        }
    }

    public class GuestCheckinDTO
    {
        public int GuestCheckinID { get; set; }
        public int GuestID { get; set; }
        public DateTime CheckinTime { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
