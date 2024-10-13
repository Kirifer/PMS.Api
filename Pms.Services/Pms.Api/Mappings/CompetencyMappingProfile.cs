using AutoMapper;

using Pms.Datalayer.Entities;
using Pms.Datalayer.Queries;
using Pms.Models;

namespace Pms.Api.Mappings
{
    public class CompetencyMappingProfile : Profile
    {
        public CompetencyMappingProfile()
        {
            CreateMap<PmsCompetencyFilterDto, CompetencyQueryFilter>(MemberList.Destination);
            CreateMap<PmsCompetency, PmsCompetencyDto>(MemberList.Destination);
        }
    }
}
