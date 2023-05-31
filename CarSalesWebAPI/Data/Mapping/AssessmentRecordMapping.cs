using CarSalesWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSalesWebAPI.Data.Mapping
{
    public class AssessmentRecordMapping : IEntityTypeConfiguration<AssessmentRecord>
    {
        public void Configure(EntityTypeBuilder<AssessmentRecord> builder)
        {
            builder.ToTable("AssessmentRecords");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

            builder.Property(x => x.Note)
                    .IsRequired();
            
            builder.Property(x => x.Date);

            builder.HasOne(x => x.Car)
                    .WithMany(x => x.Assessments)
                    .HasForeignKey(x => x.UserId);
            
            builder.HasOne(x => x.User)
                    .WithMany(x => x.Assessment)
                    .HasForeignKey(x => x.CarId);

            builder.Property(x => x.IsDeleted)
                    .HasDefaultValue(false);
                    
        }
    }
}
