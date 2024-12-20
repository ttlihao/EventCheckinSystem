﻿using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventCheckinSystem.Repo.Configure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Event, CreateEventDTO>().ReverseMap();
            CreateMap<Event, EventResponse>()
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Organization.Name))
                .ReverseMap();
            CreateMap<CreateEventDTO, EventResponse>().ReverseMap();

            CreateMap<Guest, GuestDTO>()
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.GuestImage.ImageURL))
                .ReverseMap();
            CreateMap<Guest, GuestResponse>()
                .ForMember(dest => dest.checkinStatus, opt => opt.MapFrom(src => src.GuestCheckin != null))
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.GuestImage.ImageURL))
                .ReverseMap();
            CreateMap<Guest, CreateGuestDTO>().ReverseMap();

            CreateMap<GuestCheckin, GuestCheckinDTO>().ReverseMap();
            CreateMap<GuestCheckin, CreateGuestCheckinDTO>().ReverseMap();

            CreateMap<GuestGroup, GuestGroupDTO>().ReverseMap();
            CreateMap<GuestGroup, CreateGuestGroupDTO>().ReverseMap();
            CreateMap<GuestGroup, GuestGroupResponse>()
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Organization.Name))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name))
                .ReverseMap();

            CreateMap<GuestImage, GuestImageDTO>().ReverseMap();
            CreateMap<GuestImage, CreateGuestImageDTO>().ReverseMap();

            CreateMap<Organization, OrganizationDTO>().ReverseMap();
            CreateMap<Organization, CreateOrganizationDTO>().ReverseMap();


            CreateMap<WelcomeTemplate, CreateWelcomeTemplateDTO>().ReverseMap();
            CreateMap<WelcomeTemplate, WelcomeTemplateDTO>().ReverseMap();

            CreateMap<IdentityUser, UserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<IdentityRole, RoleResponse>().ReverseMap();
            CreateMap<UserEvent, UserEventDTO>().ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                                                .ForMember(dest => dest.EventID, opt => opt.MapFrom(src => src.EventID)).ReverseMap();
            CreateMap<UserEvent, UserEventResponse>().ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                                    .ForMember(dest => dest.EventID, opt => opt.MapFrom(src => src.EventID))
                                    .ReverseMap();


        }
    }

}
