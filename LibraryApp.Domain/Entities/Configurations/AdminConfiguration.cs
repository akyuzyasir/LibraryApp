using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Domain.Entities.Configurations;

public class AdminConfiguration : BaseUserConfiguration<Admin>
{
    public override void Configure(EntityTypeBuilder<Admin> builder)
    {
        base.Configure(builder);
    }
}
