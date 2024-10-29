using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
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
            CreateMap<Event, CreateEventDTO>().ReverseMap();
            CreateMap<CreateEventDTO, EventDTO>().ReverseMap();
            CreateMap<Event, EventDTO>().ReverseMap();
                //.ForMember(dest => dest.GuestGroups, opt => opt.MapFrom(src => src.GuestGroups)).ReverseMap();

            CreateMap<Guest, GuestDTO>().ReverseMap();
            CreateMap<GuestCheckin, GuestCheckinDTO>().ReverseMap();
            CreateMap<GuestGroup, GuestGroupDTO>().ReverseMap();
            CreateMap<GuestImage, GuestImageDTO>().ReverseMap();

            CreateMap<Organization, CreateOrganizationDTO>().ReverseMap();
            CreateMap<Organization, OrganizationDTO>().ReverseMap();

            CreateMap<IdentityUser, UserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<IdentityRole, RoleResponseDTO>().ReverseMap();
            CreateMap<UserEvent, UserEventDTO>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.EventID, opt => opt.MapFrom(src => src.Event.Name)).ReverseMap();

            CreateMap<WelcomeTemplate, WelcomeTemplateDTO>().ReverseMap();
        }
    }

}
