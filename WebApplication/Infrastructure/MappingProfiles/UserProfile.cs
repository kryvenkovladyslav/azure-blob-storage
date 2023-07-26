using Abstract.Models;
using AutoMapper;
using WebApplication.Models.ViewModels;

namespace WebApplication.Infrastructure.MappingProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
        }
    }
}