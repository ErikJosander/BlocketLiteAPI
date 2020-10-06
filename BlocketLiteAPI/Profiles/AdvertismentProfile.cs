using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteAPI.Models.Advertisment;
using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteAPI.Profiles
{
    /// <summary>
    /// Povides mapping for the <see cref="Advertisement"/> and all the related <see cref="Models.Advertisment"/>
    /// </summary>
    public class AdvertismentProfile : Profile
    {
        public AdvertismentProfile()
        {
            CreateMap<Advertisement, AdvertismentAdvancedDto>();

            CreateMap<AdvertisementForCreationDto, Advertisement>()
                .ForMember(
                    dest => dest.PropertyTypeId,
                    opt => opt.MapFrom(src => src.Type));

            CreateMap<Advertisement, AdvertismentSimpleDto>();

            CreateMap<Advertisement, AdvertismentMoreAdvancedDto>();
        }
    }
}
