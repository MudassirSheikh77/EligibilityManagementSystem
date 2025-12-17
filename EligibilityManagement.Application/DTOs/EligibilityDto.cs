using EligibilityManagement.Domain.Enums;
using System;

namespace EligibilityManagement.Application.DTOs;

public class EligibilityDto
{
    public int Id { get; set; }
    public string Payer { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public RequestStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsReferral { get; set; }
    public bool IsNewBorn { get; set; }
}