using AutoMapper;
using Instagraph.DataProcessor.DtoModels;
using Instagraph.Models;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<User, PopularUserDto>()
                .ForMember(dto => dto.Followers, f => f.MapFrom(u => u.Followers.Count));
        }
    }
}
