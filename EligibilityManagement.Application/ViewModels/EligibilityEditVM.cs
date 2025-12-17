using System;
using System.ComponentModel.DataAnnotations;
using EligibilityManagement.Domain.Enums;

namespace EligibilityManagement.Application.ViewModels;

public class EligibilityEditVM
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Payer { get; set; } = null!;

    [Required]
    public string PatientName { get; set; } = null!;

    public string? PolicyHolderName { get; set; }

    [Required]
    public string PolicyNumber { get; set; } = null!;

    [Required]
    public string DocumentType { get; set; } = null!;

    [Required]
    public string DocumentNumber { get; set; } = null!;

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public MaritalStatus MaritalStatus { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Phone]
    public string MobileNumber { get; set; } = null!;

    public bool IsNewBorn { get; set; }
    public bool IsReferral { get; set; }
}