using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Person.Domain.Models.CityModels;

namespace Person.Infrastructure.Db.Mapping
{
    public class CityMap :   DbEntityConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> entity)
        {
            entity.ToTable("City");

            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").ValueGeneratedOnAdd();

        }
    }
}
