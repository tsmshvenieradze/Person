using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Person.Domain.Models.CityModels;
using Person.Domain.Models.LogModels;
using Person.Domain.Models.PersonModels;
using Person.Domain.Models.PhoneNumberModels;
using Person.Infrastructure.Db.Mapping;

namespace Person.Infrastructure.Db
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Models.PersonModels.Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PersonRelation> PersonRelations { get; set; }
        public DbSet<LogData> Logs { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new PersonMap());
            modelBuilder.AddConfiguration(new CityMap());
            modelBuilder.AddConfiguration(new RelationMap());
            modelBuilder.AddConfiguration(new LogMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}