using AutoMapper;
using Pms.Datalayer.Commands;
using Pms.Datalayer.Entities;
using Pms.Datalayer.Queries;
using Pms.Models;

namespace Pms.Api.Mappings
{
    public class UserPerformanceReviewMappingProfile : Profile
    {
        public UserPerformanceReviewMappingProfile()
        {
            // Map filter DTO to query filter
            CreateMap<PmsUserPerformanceReviewFilterDto, UserPerformanceReviewQueryFilter>(MemberList.Destination);

            // Map entities to DTOs (example mapping for UserPerformanceReview)
            CreateMap<UserPerformanceReview, PmsUserPerformanceReviewDto>(MemberList.Destination);

            CreateMap<PmsUserPerformanceReviewCreateDto, UserPerformanceReviewCreateCmdModel>(MemberList.Destination)
                .ForMember(dest => dest.EmployeeReviewDate,
                 opt => opt.MapFrom(src => src.EmployeeReviewDate)); 


            // Add any additional mappings related to user performance reviews if needed
            // Example: mapping for other commands or additional DTOs related to performance reviews
            // CreateMap<PmsUserPerformanceReviewCreateDto, UserPerformanceReviewCreateCmdModel>(MemberList.Destination);
            // CreateMap<PmsUserPerformanceReviewUpdateDto, UserPerformanceReviewUpdateCmdModel>(MemberList.Destination);
        }
    }
}
