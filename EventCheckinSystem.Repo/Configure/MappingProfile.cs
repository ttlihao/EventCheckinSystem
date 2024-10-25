using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
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
            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.GuestGroups, opt => opt.MapFrom(src => src.GuestGroups)) 
                .ForMember(dest => dest.UserEvents, opt => opt.MapFrom(src => src.UserEvents));

            CreateMap<Guest, GuestDTO>();
            CreateMap<GuestCheckin, GuestCheckinDTO>();
            CreateMap<GuestGroup, GuestGroupDTO>();
            CreateMap<GuestImage, GuestImageDTO>();

            CreateMap<Organization, OrganizationDTO>()
                .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events));

            CreateMap<UserEvent, UserEventDTO>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.EventID, opt => opt.MapFrom(src => src.Event.Name));

            CreateMap<WelcomeTemplate, WelcomeTemplateDTO>();
        }
    }

}
