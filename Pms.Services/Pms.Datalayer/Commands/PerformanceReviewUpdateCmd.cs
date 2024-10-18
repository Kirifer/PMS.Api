using Pms.Core.Database.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

namespace Pms.Datalayer.Commands
{
    public interface IPerformanceReviewUpdateCmd :
    IDbCommand<PerformanceReviewUpdateCmdModel, IdDto>
    { }

    public class PerformanceReviewUpdateCmd(PmsDbContext context) :
        DbCommandBase<PerformanceReviewUpdateCmdModel, IdDto>(context, context.UserContext),
        IPerformanceReviewUpdateCmd
    {
        private PerformanceReview? _updateRef;

        protected override async Task BuildCommandAsync()
        {
            _updateRef!.Name = _cmd.Name;
            _updateRef.StartYear = _cmd.StartYear;
            _updateRef.EndYear = _cmd.EndYear;
            _updateRef.StartDate = _cmd.StartDate;
            _updateRef.EndDate = _cmd.EndDate;
            _updateRef.EmployeeId = _cmd.EmployeeId;
            _updateRef.SupervisorId = _cmd.SupervisorId;
            _updateRef.Goals.ForEach(goal =>
            {
                // Check and update existing item
                var existingItem = _cmd.Goals
                    .FirstOrDefault(item => item.Id == goal.Id);
                if (existingItem != null)
                {
                    goal.Goals = existingItem.Goals;
                    goal.OrderNo = existingItem.OrderNo;
                    goal.Weight = existingItem.Weight;
                    goal.Date = existingItem.Date;
                    goal.Measure1 = existingItem.Measure1;
                    goal.Measure2 = existingItem.Measure2;
                    goal.Measure3 = existingItem.Measure3;
                    goal.Measure4 = existingItem.Measure4;
                }
            });

            _updateRef.Competencies.ForEach(compentency =>
            {
                // Check and update existing item
                var existingItem = _cmd.Competencies
                    .FirstOrDefault(item => item.Id == compentency.Id);
                if (existingItem != null)
                {
                    compentency.CompetencyLevelId = existingItem.CompetencyId;
                    compentency.OrderNo = existingItem.OrderNo;
                    compentency.Weight = existingItem.Weight;
                }
            });

            await Task.Run(() => context.UpdateRange(_updateRef));

            _result.Id = _updateRef.Id;
        }

        protected override bool ValidateModel()
        {
            var context = DbContext as PmsDbContext;

            // Validate existing project codes
            _updateRef = context!.PerformanceReviews
                .FirstOrDefault(pr => pr.Id == _cmd.Id);
            if (_updateRef == null)
                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed, $"Performance Review is not found.");

            return _updateRef != null;
        }
    }

    public class PerformanceReviewUpdateCmdModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public DateOnly? StartYear { get; set; }
        public DateOnly? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? SupervisorId { get; set; }

        public List<PerformanceReviewGoalUpdateCmdModel> Goals { get; set; } = new();
        public List<PerformanceReviewCompetencyUpdateCmdModel> Competencies { get; set; } = new();
    }

    public class PerformanceReviewGoalUpdateCmdModel
    {
        public Guid Id { get; set; }

        public int OrderNo { get; set; }

        public string? Goals { get; set; }
        public decimal Weight { get; set; }
        public string? Date { get; set; }
        public string? Measure4 { get; set; }
        public string? Measure3 { get; set; }
        public string? Measure2 { get; set; }
        public string? Measure1 { get; set; }
    }

    public class PerformanceReviewCompetencyUpdateCmdModel
    {
        public Guid Id { get; set; }

        public Guid CompetencyId { get; set; }
        public int OrderNo { get; set; } = 0;
        public decimal? Weight { get; set; } = 0M;
    }
}
