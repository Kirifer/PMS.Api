using FluentValidation;

using Pms.Models.Enums;

namespace Pms.Models
{
    public class PmsPerformanceReviewCreateDto
    {
        public string? Name { get; set; }
        public DepartmentType? DepartmentType { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? SupervisorId { get; set; }

        public List<PmsPerformanceReviewGoalCreateDto>? Goals { get; set; }
        public List<PmsPerformanceReviewCompetencyCreateDto>? Competencies { get; set; }
    }

    public class PmsPerformanceReviewGoalCreateDto
    {
        public int OrderNo { get; set; }

        public string? Goals { get; set; }
        public decimal Weight { get; set; }
        public string? Date { get; set; }
        public string? Measure4 { get; set; }
        public string? Measure3 { get; set; }
        public string? Measure2 { get; set; }
        public string? Measure1 { get; set; }
    }

    public class PmsPerformanceReviewCompetencyCreateDto
    {
        public Guid CompetencyId { get; set; }
        public int OrderNo { get; set; } = 0;
        public decimal? Weight { get; set; } = 0M;
    }

    public class PmsPerformanceReviewCreateDtoAbstractValidator : AbstractValidator<PmsPerformanceReviewCreateDto>
    {
        public PmsPerformanceReviewCreateDtoAbstractValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required.");
            RuleFor(x => x.DepartmentType).NotEmpty().NotNull().WithMessage("Department Type is required.");
            RuleFor(x => x.StartYear).NotEmpty().NotNull().WithMessage("Start Year is required.");
            RuleFor(x => x.EndYear).NotEmpty().NotNull().WithMessage("End Year is required.");
            RuleFor(x => x.StartDate).NotEmpty().NotNull();
            RuleFor(x => x.EndDate).NotEmpty().NotNull();
        }
    }
}
