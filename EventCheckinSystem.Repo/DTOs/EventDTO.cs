﻿using FluentValidation;
using System;

namespace EventCheckinSystem.Repo.DTOs
{
    public class EventDTOValidator : AbstractValidator<EventDTO>
    {
        public EventDTOValidator()
        {
            RuleFor(model => model.EventID)
                .NotEmpty().WithMessage("EventID không được bỏ trống!");

            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("Tên sự kiện không được bỏ trống!")
                .Length(2, 100).WithMessage("Tên sự kiện phải có độ dài từ 2 đến 100 ký tự!");

            RuleFor(model => model.OrganizationID)
                .NotEmpty().WithMessage("OrganizationID không được bỏ trống!")
                .GreaterThan(0).WithMessage("OrganizationID phải lớn hơn 0!");

            RuleFor(model => model.StartDate)
                .NotEmpty().WithMessage("Ngày bắt đầu không được bỏ trống!")
                .Must(date => date >= DateTime.Today).WithMessage("Ngày bắt đầu phải sau hoặc là hôm nay!");

            RuleFor(model => model.EndDate)
                .NotEmpty().WithMessage("Ngày kết thúc không được bỏ trống!")
                .GreaterThanOrEqualTo(model => model.StartDate).WithMessage("Ngày kết thúc phải sau hoặc bằng ngày bắt đầu!");

            RuleFor(model => model.Location)
                .NotEmpty().WithMessage("Địa điểm không được bỏ trống!")
                .Length(2, 200).WithMessage("Địa điểm phải có độ dài từ 2 đến 200 ký tự!");

            RuleFor(model => model.Description)
                .NotEmpty().WithMessage("Mô tả không được bỏ trống!")
                .Length(12, 500).WithMessage("Mô tả phải có độ dài từ 12 đến 500 ký tự!");

            RuleFor(model => model.Capacity)
                .GreaterThan(0).WithMessage("Sức chứa phải lớn hơn 0!");

            RuleFor(model => model.Status)
                .NotEmpty().WithMessage("Trạng thái không được bỏ trống!")
                .Length(2, 50).WithMessage("Trạng thái phải có độ dài từ 2 đến 50 ký tự!");
        }
    }

    public class EventDTO
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public int OrganizationID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
    }
}
