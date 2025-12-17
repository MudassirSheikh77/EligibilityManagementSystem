using EligibilityManagement.Application.DTOs;
using EligibilityManagement.Application.Interfaces;
using EligibilityManagement.Application.ViewModels;
using EligibilityManagement.Domain.Entities;
using EligibilityManagement.Domain.Enums;
using EligibilityManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EligibilityManagement.Application.Services;

public class EligibilityService : IEligibilityService
{
    private readonly AppDbContext _context;

    public EligibilityService(AppDbContext context)
    {
        _context = context;
    }

    // LIST
    public async Task<IEnumerable<EligibilityDto>> GetAllAsync()
    {
        return await _context.EligibilityRequests
            .AsNoTracking()
            .Select(x => new EligibilityDto
            {
                Id = x.Id,
                Payer = x.Payer,
                PatientName = x.PatientName,
                PolicyNumber = x.PolicyNumber,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
                IsReferral = x.IsReferral,
                IsNewBorn = x.IsNewBorn
            })
            .ToListAsync();
    }

    // CREATE
    public async Task CreateAsync(EligibilityCreateVM model)
    {
        var entity = new EligibilityRequest
        {
            Payer = model.Payer,
            PatientName = model.PatientName,
            PolicyHolderName = model.PolicyHolderName,
            DocumentType = model.DocumentType,
            DocumentNumber = model.DocumentNumber,
            PolicyNumber = model.PolicyNumber,
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
            MaritalStatus = model.MaritalStatus,
            MobileNumber = model.MobileNumber,
            IsNewBorn = model.IsNewBorn,
            IsReferral = model.IsReferral,
            Status = RequestStatus.Pending,
            CreatedDate = DateTime.UtcNow   
        };

        _context.EligibilityRequests.Add(entity);
        await _context.SaveChangesAsync();
    }

    // DETAILS
    public async Task<EligibilityDetailsVM?> GetDetailsAsync(int id)
    {
        return await _context.EligibilityRequests
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new EligibilityDetailsVM
            {
                Id = x.Id,
                Payer = x.Payer,
                PatientName = x.PatientName,
                PolicyHolderName = x.PolicyHolderName,
                DocumentType = x.DocumentType,
                DocumentNumber = x.DocumentNumber,
                PolicyNumber = x.PolicyNumber,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                MaritalStatus = (MaritalStatus)x.MaritalStatus,
                MobileNumber = x.MobileNumber,
                IsNewBorn = x.IsNewBorn,
                IsReferral = x.IsReferral,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate
            })
            .FirstOrDefaultAsync();
    }

    // EDIT - GET
    public async Task<EligibilityEditVM?> GetEditAsync(int id)
    {
        return await _context.EligibilityRequests
            .Where(x => x.Id == id)
            .Select(x => new EligibilityEditVM
            {
                Id = x.Id,
                Payer = x.Payer,
                PatientName = x.PatientName,
                PolicyHolderName = x.PolicyHolderName,
                DocumentType = x.DocumentType,
                DocumentNumber = x.DocumentNumber,
                PolicyNumber = x.PolicyNumber,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                MaritalStatus = (MaritalStatus)x.MaritalStatus,
                MobileNumber = x.MobileNumber,
                IsNewBorn = x.IsNewBorn,
                IsReferral = x.IsReferral
            })
            .FirstOrDefaultAsync();
    }

    // EDIT - POST
    public async Task UpdateAsync(EligibilityEditVM model)
    {
        var entity = await _context.EligibilityRequests.FindAsync(model.Id);
        if (entity == null)
            throw new Exception("Eligibility request not found");

        entity.Payer = model.Payer;
        entity.PatientName = model.PatientName;
        entity.PolicyHolderName = model.PolicyHolderName;
        entity.PolicyNumber = model.PolicyNumber;
        entity.DocumentType = model.DocumentType;
        entity.DocumentNumber = model.DocumentNumber;
        entity.Gender = model.Gender;
        entity.MaritalStatus = model.MaritalStatus;
        entity.DateOfBirth = model.DateOfBirth;
        entity.MobileNumber = model.MobileNumber;
        entity.IsNewBorn = model.IsNewBorn;
        entity.IsReferral = model.IsReferral;
        entity.ModifiedDate = DateTime.UtcNow; 

        await _context.SaveChangesAsync();
    }

    // DELETE
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.EligibilityRequests.FindAsync(id);
        if (entity == null)
            return;

        _context.EligibilityRequests.Remove(entity);
        await _context.SaveChangesAsync();
    }

    // APPROVE & REJECT
    public async Task ApproveAsync(int id)
    {
        var entity = await _context.EligibilityRequests.FindAsync(id);
        if (entity == null) return;

        entity.Status = RequestStatus.Approved;
        entity.ModifiedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task RejectAsync(int id)
    {
        var entity = await _context.EligibilityRequests.FindAsync(id);
        if (entity == null) return;

        entity.Status = RequestStatus.Rejected;
        entity.ModifiedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<EligibilityDto>> GetPagedAsync(EligibilityListFilterVM filter, int pageNumber, int pageSize)
    {
        var query = _context.EligibilityRequests.AsQueryable();

        // Filters
        if (!string.IsNullOrWhiteSpace(filter.Payer))
            query = query.Where(x => x.Payer.Contains(filter.Payer));

        if (!string.IsNullOrWhiteSpace(filter.DocumentType))
            query = query.Where(x => x.DocumentType == filter.DocumentType);

        if (!string.IsNullOrWhiteSpace(filter.DocumentNumber))
            query = query.Where(x => x.DocumentNumber.Contains(filter.DocumentNumber));

        if (!string.IsNullOrWhiteSpace(filter.Status)
        && Enum.TryParse<RequestStatus>(filter.Status, out var status))
        {
            query = query.Where(x => x.Status == status);
        }

        if (filter.FromDate.HasValue)
            query = query.Where(x => x.CreatedDate >= filter.FromDate.Value);

        if (filter.ToDate.HasValue)
            query = query.Where(x => x.CreatedDate <= filter.ToDate.Value);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(x => x.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new EligibilityDto
            {
                Id = x.Id,
                Payer = x.Payer,
                PatientName = x.PatientName,
                PolicyNumber = x.PolicyNumber,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
                IsReferral =x.IsReferral,
                IsNewBorn = x.IsNewBorn
            })
            .ToListAsync();

        return new PagedResult<EligibilityDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    public async Task<List<string>> GetDistinctPayersAsync()
    {
        return await _context.EligibilityRequests
            .Select(x => x.Payer)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }

    public async Task<List<string>> GetDistinctDocumentTypesAsync()
    {
        return await _context.EligibilityRequests
            .Select(x => x.DocumentType)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }
}