using System;

namespace EligibilityManagement.Application.ViewModels;

public class EligibilityListFilterVM
{
    public string? Payer { get; set; }
    public string? Status { get; set; }
    public string? DocumentType { get; set; }
    public string? DocumentNumber { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}