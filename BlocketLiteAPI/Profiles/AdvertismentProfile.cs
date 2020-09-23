using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteAPI.Models.Advertisment;
using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteAPI.Profiles
{
    public class AdvertismentProfile : Profile
    {
        public AdvertismentProfile()
        {
            CreateMap<Advertisement, AdvertismentAdvancedDto>();

            CreateMap<AdvertisementForCreationDto, Advertisement>();

            CreateMap<Advertisement, AdvertismentSimpleDto>();

            CreateMap<Advertisement, AdvertismentMoreAdvancedDto>();
        }
    }
}
