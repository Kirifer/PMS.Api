using AutoMapper;

using Pms.Datalayer.Commands;
using Pms.Datalayer.Entities;
using Pms.Datalayer.Queries;
using Pms.Models;

namespace Pms.Api.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<PmsUserFilterDto, UserQueryFilter>(MemberList.Destination);
            CreateMap<User, PmsUserDto>(MemberList.Destination);
            CreateMap<PmsUserCreateDto, UserCreateCmdModel>(MemberList.Destination);
            CreateMap<PmsUserUpdateDto, UserUpdateCmdModel>(MemberList.Destination);
        }
    }
}
