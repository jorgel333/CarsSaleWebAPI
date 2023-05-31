using CarSalesWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSalesWebAPI.Data.Mapping
{
    public class CarMapping : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Model)
                    .IsRequired()
                    .HasMaxLength(30);

            builder.Property(x => x.Brand)
                    .IsRequired()
                    .HasMaxLength(30);

            builder.Property(x => x.Color)
                    .IsRequired()
                    .HasMaxLength(30);

            builder.Property(x => x.Type)
                    .IsRequired()
                    .HasMaxLength(30);

            builder.Property(x => x.YearOfManufacture)
                    .IsRequired();
            
            builder.Property(x => x.Average)
                    .IsRequired()
                    .HasDefaultValue(0);
            
            builder.Property(x => x.Stock)
                    .IsRequired();

            builder.Property(x => x.IsDeleted)
                    .HasDefaultValue(false);

            builder.HasMany(x => x.Assessments)
                    .WithOne(x => x.Car);

        }
    }
}
