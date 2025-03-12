using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Domain.Entities.Configurations;

public class MemberConfiguration:BaseUserConfiguration<Member>
{
    public override void Configure(EntityTypeBuilder<Member> builder)
    {
        base.Configure(builder);
        builder.Property(m => m.MembershipNumber)
                                .HasMaxLength(50)
                                .IsRequired();

        // Navigation property: A member can borrow more than one book
        builder.HasMany(m => m.BookLoans)
               .WithOne(bl => bl.Member)
               .HasForeignKey(bl => bl.MemberId)
               .OnDelete(DeleteBehavior.Restrict);

    }
}
