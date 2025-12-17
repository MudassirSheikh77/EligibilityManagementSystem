using EligibilityManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EligibilityManagement.Infrastructure.Configurations;

public class EligibilityRequestConfig : IEntityTypeConfiguration<EligibilityRequest>
{
    public void Configure(EntityTypeBuilder<EligibilityRequest> builder)
    {
        builder.ToTable("EligibilityRequests");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Payer)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PatientName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.PolicyHolderName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.DocumentType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.DocumentNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();
    }
}