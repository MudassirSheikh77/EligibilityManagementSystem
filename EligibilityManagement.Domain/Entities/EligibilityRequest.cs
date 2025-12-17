
using EligibilityManagement.Domain.Common;
using EligibilityManagement.Domain.Enums;
using System;

namespace EligibilityManagement.Domain.Entities;
public class EligibilityRequest : BaseEntity
{
    public string Payer { get; set; }
    public DateTime RequestDate { get; set; }
    public string PatientName { get; set; }
    public string PolicyHolderName { get; set; }
    public string DocumentType { get; set; }
    public string DocumentNumber { get; set; }
    public string? PolicyNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public string? MobileNumber { get; set; }
    public bool IsReferral { get; set; }
    public bool IsNewBorn { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
}
