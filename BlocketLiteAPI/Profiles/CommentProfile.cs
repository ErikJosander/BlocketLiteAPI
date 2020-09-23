using AutoMapper;
using BlocketLiteAPI.Models;
using BlocketLiteEFCoreDB.Entities;

namespace BlocketLiteAPI.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            // Pass in source ad destination type (Obejct to Object)
            CreateMap<Comment, CommentDto>();

            CreateMap<CommentForCreationDto, Comment>();

        }
    }
}
