using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Queries;
using Pms.Domain.Services.Interface;
using Pms.Models;

namespace Pms.Domain.Services
{
    public class LookupService(IMapper mapper, ILogger<LookupService> logger,
        ICompetencyQuery competencyQuery,
        IUserQuery userQuery)
        : EntityService(mapper, logger), ILookupService
    {
        public async Task<Response<List<PmsCompetencyDto>>> GetCompetenciesAsync(PmsCompetencyFilterDto filter)
        {
            try
            {
                var queryFilter = Mapper.Map<CompetencyQueryFilter>(filter);
                var result = await competencyQuery
                    .GetQuery(queryFilter)
                    .ToListAsync();

                return Response<List<PmsCompetencyDto>>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching competencies");
                return Response<List<PmsCompetencyDto>>.Exception(ex);
            }
        }

        public async Task<Response<List<PmsUserDto>>> GetUsersAsync()
        {
            try
            {
                var result = await userQuery
                    .GetQuery(new UserQueryFilter ())
                    .ToListAsync();

                return Response<List<PmsUserDto>>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching users");
                return Response<List<PmsUserDto>>.Exception(ex);
            }
        }

        public async Task<Response<List<PmsUserDto>>> GetSupervisorsAsync()
        {
            try
            {
                var result = await userQuery
                    .GetQuery(new UserQueryFilter { IsSupervisor = true })
                    .ToListAsync();

                return Response<List<PmsUserDto>>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching competencies");
                return Response<List<PmsUserDto>>.Exception(ex);
            }
        }
    }
}
