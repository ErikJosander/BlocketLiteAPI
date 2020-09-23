using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteAPI.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            // Pass in source ad destination type (Obejct to Object)
            CreateMap<User, UserDto>();

            CreateMap<UserForCreationDto, User>();
        }
    }
}
