using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Person.Domain.Models.CityModels;
using Person.Domain.Models.PersonModels;

namespace Person.Infrastructure.Db.Mapping
{
    public class PersonMap : DbEntityConfiguration<Domain.Models.PersonModels.Person>
    {
        public override void Configure(EntityTypeBuilder<Domain.Models.PersonModels.Person> entity)
        {
            entity.ToTable("Person");

            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").ValueGeneratedOnAdd(); 
             

            entity.HasMany(x => x.PhoneNumbers).WithOne().HasForeignKey(x=>x.PersonId);

            entity.HasOne(x => x.Relation).WithOne(x=>x.Person).HasForeignKey<PersonRelation>(x=>x.RelatedPersonId); 
            entity.HasMany(x => x.Relations).WithOne().HasForeignKey(x=>x.PersonId).OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
