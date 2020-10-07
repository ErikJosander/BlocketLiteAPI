using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteAPI.Profiles
{
    /// <summary>
    /// Povides mapping for the <see cref="User"/> and all the related Models
    /// </summary>
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            // Pass in source ad destination type (Obejct to Object)
            CreateMap<User, UserDto>()
                .ForMember(
                dest => dest.UserId,
                opt => opt.MapFrom(src => src.Id));

            CreateMap<UserForCreationDto, User>();
        }
    }
}
