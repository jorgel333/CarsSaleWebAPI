using CarSalesWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarSalesWebAPI.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);


            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(180);


            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(150);


            builder.Property(u => u.IsAdmin)
                   .HasDefaultValue(false);

            builder.Property(u => u.IsDeleted)
                    .HasDefaultValue(false);
        }
    }
}
