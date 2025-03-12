using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Entities.Configurations
{
    public class BookLoanConfiguration : AuditableEntityConfiguration<BookLoan>
    {
        public override void Configure(EntityTypeBuilder<BookLoan> builder)
        {
            base.Configure(builder);
            builder.Property(bl=>bl.BorrowDate)
                                    .IsRequired();
            builder.Property(bl => bl.ReturnDate)
                                    .IsRequired(false);

            builder.HasOne(bl => bl.BookCopy)
                    .WithMany(b => b.BookLoans)
                    .HasForeignKey(bl => bl.BookCopyId);

            builder.HasOne(bl => bl.Member)
                    .WithMany(m => m.BookLoans)
                    .HasForeignKey(bl => bl.MemberId);


        }
    }
}
