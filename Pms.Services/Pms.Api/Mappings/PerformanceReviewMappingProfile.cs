﻿using AutoMapper;

using Pms.Datalayer.Commands;
using Pms.Datalayer.Entities;
using Pms.Datalayer.Queries;
using Pms.Models;

namespace Pms.Api.Mappings
{
    public class PerformanceReviewMappingProfile : Profile
    {
        public PerformanceReviewMappingProfile()
        {
            CreateMap<PmsPerformanceReviewFilterDto, PerformanceReviewQueryFilter>(MemberList.Destination);
            CreateMap<PerformanceReview, PmsPerformanceReviewDto>(MemberList.Destination);
            CreateMap<PmsPerformanceReviewCreateDto, PerformanceReviewCreateCmdModel>(MemberList.Destination);
            CreateMap<PmsPerformanceReviewUpdateDto, PerformanceReviewUpdateCmdModel>(MemberList.Destination);

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
