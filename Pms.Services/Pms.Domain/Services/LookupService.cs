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

            ICompetencyQuery competencyQuery)
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
    }
}
