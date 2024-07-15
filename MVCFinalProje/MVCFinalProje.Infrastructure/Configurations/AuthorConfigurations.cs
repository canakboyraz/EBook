using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCFinalProje.Core.BaseEntityConfigurations;
using MVCFinalProje.Core.Interfaces;
using MVCFinalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Configurations
{
    public class AuthorConfigurations : AuditableEntityConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Name).IsRequired().HasMaxLength(128);
            builder.Property(a => a.DateOfBirth).IsRequired();
            base.Configure(builder);
        }
    }
}
