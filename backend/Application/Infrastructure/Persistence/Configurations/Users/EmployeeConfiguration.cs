namespace Application.Infrastructure.Persistence.Configurations.Users;

using Application.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> tableBuilder)
    {
        tableBuilder.HasKey(x => x.Id);

        tableBuilder
            .HasOne(u => u.User)
            .WithOne(x => x.Employee)
            .HasForeignKey<Employee>(x => x.UserId)
            .IsRequired(false);
    }
}
