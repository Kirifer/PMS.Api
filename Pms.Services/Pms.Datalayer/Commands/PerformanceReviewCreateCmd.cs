using Pms.Core.Database.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Models.Enums;
using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

namespace Pms.Datalayer.Commands
{
    public interface IPerformanceReviewCreateCmd :
    IDbCommand<PerformanceReviewCreateCmdModel, IdDto>
    { }

    public class PerformanceReviewCreateCmd(PmsDbContext context) :
        DbCommandBase<PerformanceReviewCreateCmdModel, IdDto>(context, context.UserContext),
        IPerformanceReviewCreateCmd
    {
        private PerformanceReview? _createRef;

        protected override async Task BuildCommandAsync()
        {
            _createRef = new PerformanceReview()
            {
                Name = _cmd.Name,
                DepartmentType = _cmd.DepartmentType,
                StartYear = _cmd.StartYear,
                EndYear = _cmd.EndYear,
                StartDate = _cmd.StartDate,
                EndDate = _cmd.EndDate,
                EmployeeId = _cmd.EmployeeId,
                SupervisorId = _cmd.SupervisorId,
                Goals = _cmd.Goals.Select(g => new PerformanceReviewGoal
                {
                    Goals = g.Goals,
                    OrderNo = g.OrderNo,
                    Weight = g.Weight,
                    Date = g.Date,
                    Measure1 = g.Measure1,
                    Measure2 = g.Measure2,
                    Measure3 = g.Measure3,
                    Measure4 = g.Measure4,
                }).ToList(),
                Competencies = _cmd.Competencies.Select(c => new PerformanceReviewCompetency
                {
                    CompetencyLevelId = c.CompetencyId,
                    OrderNo = c.OrderNo,
                    Weight = c.Weight,
                }).ToList()
            };

            await DbContext.AddAsync(_createRef);

            _result.Id = _createRef.Id;
        }

        protected override bool ValidateModel()
        {
            var context = DbContext as PmsDbContext;

            // Validate existing project codes
            var existingRecord = context!.PerformanceReviews
                .FirstOrDefault(pr => pr.Name == _cmd.Name);
            if (existingRecord != null)
                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed, $"Performance Review with name {_cmd.Name} already exists.");

            foreach(var competency in _cmd.Competencies)
            {
                var checkCompetency = context!.Competencies.FirstOrDefault(c => c.Id == competency.CompetencyId)
                    ?? throw new DatabaseAccessException(DbErrorCode.ValidationFailed,
                    $"Related comptentcy id {competency.CompetencyId} is not found on comptencies.");
            }

            return _createRef == null;
        }
    }

    public class PerformanceReviewCreateCmdModel
    {
        public string Name { get; set; } = string.Empty;
        public DepartmentType DepartmentType { get; set; }
        public DateOnly? StartYear { get; set; }
        public DateOnly? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? SupervisorId { get; set; }

        public List<PerformanceReviewGoalCreateCmdModel> Goals { get; set; } = new();
        public List<PerformanceReviewCompetencyCreateCmdModel> Competencies { get; set; } = new();
    }

    public class PerformanceReviewGoalCreateCmdModel
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

    public class PerformanceReviewCompetencyCreateCmdModel
    {
        public Guid CompetencyId { get; set; }
        public int OrderNo { get; set; } = 0;
        public decimal? Weight { get; set; } = 0M;
    }
}
