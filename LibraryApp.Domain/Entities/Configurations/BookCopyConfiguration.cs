using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Entities.Configurations
{
    public class BookCopyConfiguration:AuditableEntityConfiguration<BookCopy>
    {
        public override void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            base.Configure(builder);

            builder.Property(bc => bc.CopyNumber)
                                    .IsRequired();
            builder.Property(bc=>bc.BookStatus)
                                    .IsRequired();

            builder.HasMany(bc => bc.BookLoans)
                    .WithOne(bl => bl.BookCopy)
                    .HasForeignKey(bl => bl.BookCopyId);

            builder.HasOne(bc => bc.Book)
                    .WithMany(b => b.BookCopies)
                    .HasForeignKey(bc => bc.BookId);
            
        }
    }
}
