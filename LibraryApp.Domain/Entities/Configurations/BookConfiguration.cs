using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Entities.Configurations
{
    public class BookConfiguration:AuditableEntityConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.ISBN)
                    .HasMaxLength(13)
                    .IsRequired();

            builder.Property(b => b.Title)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(b => b.Description)
                   .HasMaxLength(1024)
                   .IsRequired(false);

            builder.Property(b => b.Author)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(b => b.PublicationYear)
                   .IsRequired();


            builder.HasOne(b => b.BookCategory)
                   .WithMany(bc => bc.Books)
                   .HasForeignKey(b => b.BookCategoryId);

            builder.HasMany(b => b.BookCopies)
                   .WithOne(bl => bl.Book)
                   .HasForeignKey(bl => bl.BookId);
        }
    }
}
