using EligibilityManagement.Application.DTOs;
using EligibilityManagement.Application.ViewModels;
using EligibilityManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EligibilityManagement.Application.Interfaces;

public interface IEligibilityService
{
    Task ApproveAsync(int id);
    Task RejectAsync(int id);
    Task<IEnumerable<EligibilityDto>> GetAllAsync();
    Task<EligibilityDetailsVM?> GetDetailsAsync(int id);
    Task<EligibilityEditVM?> GetEditAsync(int id);
    Task CreateAsync(EligibilityCreateVM model);
    Task UpdateAsync(EligibilityEditVM model);
    Task DeleteAsync(int id);
    Task<PagedResult<EligibilityDto>> GetPagedAsync(EligibilityListFilterVM filter,int pageNumber,int pageSize);
    Task<List<string>> GetDistinctPayersAsync();
    Task<List<string>> GetDistinctDocumentTypesAsync();

}