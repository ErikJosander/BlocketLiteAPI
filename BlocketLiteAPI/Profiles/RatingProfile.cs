using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteAPI.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingDto>();
            CreateMap<RatingForCreationDto, Rating>()
                .ForMember(dest => dest.RatedUserId,
                    opts => opts.MapFrom(source => source.UserId));
        }
    }
}
