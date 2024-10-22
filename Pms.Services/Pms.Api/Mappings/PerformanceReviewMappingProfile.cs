using AutoMapper;

using Pms.Datalayer.Commands;
using Pms.Datalayer.Entities;
using Pms.Datalayer.Queries;
using Pms.Models;
using Pms.Shared.Extensions;

namespace Pms.Api.Mappings
{
    public class PerformanceReviewMappingProfile : Profile
    {
        public PerformanceReviewMappingProfile()
        {
            CreateMap<PmsPerformanceReviewFilterDto, PerformanceReviewQueryFilter>(MemberList.Destination);
            CreateMap<PerformanceReview, PmsPerformanceReviewDto>(MemberList.Destination);
            CreateMap<PmsPerformanceReviewCreateDto, PerformanceReviewCreateCmdModel>(MemberList.Destination)
                .ForMember(dest => dest.StartYear,
                    opt => opt.MapFrom(src => DateOnlyUtility.ConvertNumberYearToDateOnly(src.StartYear ?? DateTime.Now.Year, 1, 1)))
                .ForMember(dest => dest.EndYear,
                    opt => opt.MapFrom(src => DateOnlyUtility.ConvertNumberYearToDateOnly(src.EndYear ?? DateTime.Now.Year, 1, 1)));

            CreateMap<PmsPerformanceReviewUpdateDto, PerformanceReviewUpdateCmdModel>(MemberList.Destination)
                .ForMember(dest => dest.StartYear,
                    opt => opt.MapFrom(src => DateOnlyUtility.ConvertNumberYearToDateOnly(src.StartYear ?? DateTime.Now.Year, 1, 1)))
                .ForMember(dest => dest.EndYear,
                    opt => opt.MapFrom(src => DateOnlyUtility.ConvertNumberYearToDateOnly(src.EndYear ?? DateTime.Now.Year, 1, 1)));

            CreateMap<PerformanceReviewGoal, PmsPerformanceReviewGoalDto>(MemberList.Destination);
            CreateMap<PmsPerformanceReviewGoalCreateDto, PerformanceReviewGoalCreateCmdModel>(MemberList.Destination);
            CreateMap<PmsPerformanceReviewGoalUpdateDto, PerformanceReviewGoalUpdateCmdModel>(MemberList.Destination);

            CreateMap<PerformanceReviewCompetency, PmsPerformanceReviewCompetencyDto>(MemberList.Destination);
            CreateMap<PmsPerformanceReviewCompetencyCreateDto, PerformanceReviewCompetencyCreateCmdModel>(MemberList.Destination);
            CreateMap<PmsPerformanceReviewCompetencyUpdateDto, PerformanceReviewCompetencyUpdateCmdModel>(MemberList.Destination);

            CreateMap<PmsCompetencyFilterDto, CompetencyQueryFilter>(MemberList.Destination);
            CreateMap<PmsCompetency, PmsCompetencyDto>(MemberList.Destination);
        }
    }
}
