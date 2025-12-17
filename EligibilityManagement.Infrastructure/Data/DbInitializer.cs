using EligibilityManagement.Domain.Entities;
using EligibilityManagement.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EligibilityManagement.Infrastructure.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Prevent duplicate seeding
        if (context.EligibilityRequests.Any())
            return;

        var seedData = new List<EligibilityRequest>
        {
            new EligibilityRequest
            {
                Payer = "Mr.sheikh",
                PatientName = "chris evans",
                PolicyHolderName = "chris evans",
                DocumentType = "Passport ",
                DocumentNumber = "1234567890",
                PolicyNumber = "AD998878",
                DateOfBirth = new DateTime(1998, 2, 20),
                Gender = Gender.Male,
                MaritalStatus = MaritalStatus.Married,
                MobileNumber = "+918745612321",
                IsNewBorn = false,
                IsReferral = false,
                Status = RequestStatus.Pending
            },
            new EligibilityRequest
            {
                Payer = "Mr.lucifer",
                PatientName = "sara",
                PolicyHolderName = "sara",
                DocumentType = "Passport",
                DocumentNumber = "1111111111",
                PolicyNumber = "DDD456987",
                DateOfBirth = new DateTime(1978, 8, 5),
                Gender = Gender.Female,
                MaritalStatus = MaritalStatus.Married,
                MobileNumber = "+918965472364",
                IsNewBorn = false,
                IsReferral = true,
                Status = RequestStatus.Approved
            }
        };

        context.EligibilityRequests.AddRange(seedData);
        context.SaveChanges();
    }
}