﻿using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Models;

namespace Pms.Domain.Services.Interface
{
    public interface ILookupService : IEntityService
    {
        Task<Response<List<PmsCompetencyDto>>> GetCompetenciesAsync(PmsCompetencyFilterDto filter);
        Task<Response<List<PmsUserDto>>> GetUsersAsync();
        Task<Response<List<PmsUserDto>>> GetSupervisorsAsync();
    }
}
