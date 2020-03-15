using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Person.Domain.Models.CityModels;
using Person.Domain.Models.LogModels;

namespace Person.Infrastructure.Db.Mapping
{
    public class LogMap : DbEntityConfiguration<LogData>
    {
        public override void Configure(EntityTypeBuilder<LogData> entity)
        {
            entity.ToTable("Log");

            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").ValueGeneratedOnAdd();

        }
    }
}
