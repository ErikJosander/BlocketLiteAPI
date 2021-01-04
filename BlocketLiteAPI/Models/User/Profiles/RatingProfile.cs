using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteAPI.Profiles
{
    /// <summary>
    /// Povides mapping for the <see cref="Rating"/> and all the related Models
    /// </summary>
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
