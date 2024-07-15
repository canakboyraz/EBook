﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCFinalProje.Core.BaseEntities;
using MVCFinalProje.Core.BaseEntityConfigurations;
using MVCFinalProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Configurations
{
    public class AdminConfigurations:AuditableEntityConfiguration<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(120);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(120);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(120);
            builder.Property(a => a.IdentityId).IsRequired();
            base.Configure(builder);
        }
    }
}
