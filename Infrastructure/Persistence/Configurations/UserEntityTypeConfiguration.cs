using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsApp.Core.Domain.Entities.UserEntity;

namespace NewsApp.Infrastructure.Persistence.Configurations;

internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user", "client");

        builder
            .Property<int>("Id")
            .UseHiLo("user_id_sequence", "client");

        builder.ComplexProperty(
            user => user.PersonalName,
            propertyBuilder =>
            {
                propertyBuilder
                    .Property(personalName => personalName.Name)
                    .HasColumnName("name");

                propertyBuilder
                    .Property(personalName => personalName.Surname)
                    .HasColumnName("surname");
            });

        builder.ComplexProperty(
            user => user.Email,
            propertyBuilder => propertyBuilder
                .Property(email => email.Value)
                .HasColumnName("email"));

        builder
            .Property<string>("normalized_email")
            .HasComputedColumnSql("upper(email)", stored: true);

        builder
            .HasIndex("normalized_email")
            .IsUnique();
    }
}
