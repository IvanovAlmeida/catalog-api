using AutoMapper;

using Catalog.API.ViewModels;
using Catalog.Domain.Models;

namespace Catalog.API.Configuration
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserAddViewModel>().ReverseMap();
        }
    }
}
