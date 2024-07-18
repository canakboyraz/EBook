using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCFinalProje.Core.BaseEntityConfigurations;
using MVCFinalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Configurations
{
    public class BookConfigurations : AuditableEntityConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Name).IsRequired().HasMaxLength(128);
            builder.Property(b => b.PublisDate).IsRequired();
            base.Configure(builder);
        }
    }
}
