using EligibilityManagement.Domain.Enums;
using System;

namespace EligibilityManagement.Application.ViewModels;

public class EligibilityDetailsVM
{
    public int Id { get; set; }
    public string Payer { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string PolicyHolderName { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string MobileNumber { get; set; } = string.Empty;
    public bool IsNewBorn { get; set; }
    public bool IsReferral { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}