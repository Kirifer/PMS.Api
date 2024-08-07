using Pms.Datalayer.Entities;
using Pms.Models;

using AutoMapper;

namespace Pms.Api.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<PmsUserCreateDto, User>(MemberList.Destination);
            CreateMap<PmsUserUpdateDto, User>(MemberList.Destination);
            CreateMap<User, PmsUserDto>(MemberList.Destination);
        }
    }
}
