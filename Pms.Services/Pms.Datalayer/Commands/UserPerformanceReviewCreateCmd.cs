using Pms.Core.Database.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Models;
using Pms.Models.Enums;
using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

namespace Pms.Datalayer.Commands
{
    public interface IUserPerformanceReviewCreateCmd :
    IDbCommand<UserPerformanceReviewCreateCmdModel, IdDto>
    { }

    public class UserPerformanceReviewCreateCmd(PmsDbContext context) :
        DbCommandBase<UserPerformanceReviewCreateCmdModel, IdDto>(context, context.UserContext),
        IUserPerformanceReviewCreateCmd
    {
        private UserPerformanceReview? _createRef;

        protected override async Task BuildCommandAsync()
        {
            _createRef = new UserPerformanceReview()
            {
                UserId = _cmd.UserId,
                PerformanceReviewId = _cmd.PerformanceReviewId,
                CalibrationComments = _cmd.CalibrationComments,
                EmployeeReviewDate = _cmd.EmployeeReviewDate,
                ManagerReviewDate = _cmd.ManagerReviewDate,
                Goals = _cmd.Goals.Select(g => new UserPerformanceReviewGoals
                {
            
                    PerformanceReviewGoalId = g.PerformanceReviewGoalId,
                    Value = g.Value,
                    Comment= g.Comment,
                    IsManager = g.IsManager,
                }).ToList(),
                Competencies = _cmd.Competencies.Select(c => new UserPerformanceReviewCompetencies
                {
             
                    PerformanceReviewCompetencyId = c.PerformanceReviewCompetencyId,
                    Value = c.Value,
                    Comment = c.Comment,
                    IsManager = c.IsManager,
                }).ToList()

            };

            await DbContext.AddAsync(_createRef);

            _result.Id = _createRef.Id;
        }

        protected override bool ValidateModel()
        {
            var context = DbContext as PmsDbContext;

            // Validate existing project codes
            var existingRecord = context!.UserPerformanceReviews
                .FirstOrDefault(pr => pr.UserId == _cmd.UserId);
            if (existingRecord != null)
                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed, $"Performance Review with userId {_cmd.UserId} already exists.");

            return _createRef == null;
        }
    }

    public class UserPerformanceReviewCreateCmdModel
    {
        public string CalibrationComments { get; set; } = string.Empty;
        public DateOnly EmployeeReviewDate { get; set; }
        public DateOnly ManagerReviewDate { get; set; }
        public Guid UserId { get; set; }
        public Guid PerformanceReviewId { get; set; }
        public List<PmsUserPerfromanceReviewGoalsCreateDto> Goals { get; set; } = new();
        public List<PmsUserPeformanceReviewCompetenciesCreateDto> Competencies { get; set; } = new();
    }

    public class UserPerformanceReviewGoalCreateCmdModel
    {
        public Guid? UserPerformanceReviewId { get; set; }
        public Guid? PerformanceReviewGoalId { get; set; }
        public int Value { get; set; }

        public string? Comment { get; set; }
        public bool? IsManager { get; set; }
    }

    public class UserPerformanceReviewCompetencyCreateCmdModel
    {
        public Guid? UserPerformanceReviewId { get; set; }
        public Guid? PerformanceReviewCompetencyId { get; set; }
        public int Value { get; set; }
        public string? Comment { get; set; }
        public bool? IsManager { get; set; }
    }
}
