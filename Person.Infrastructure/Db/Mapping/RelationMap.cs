using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Person.Domain.Models.CityModels;
using Person.Domain.Models.PersonModels;

namespace Person.Infrastructure.Db.Mapping
{
    public class RelationMap :   DbEntityConfiguration<PersonRelation>
    {
        public override void Configure(EntityTypeBuilder<PersonRelation> entity)
        {
            entity.ToTable("PersonRelation");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").ValueGeneratedOnAdd();
             
        }
    }
}
