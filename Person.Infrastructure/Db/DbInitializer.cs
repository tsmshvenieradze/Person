using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Person.Domain.Models.CityModels;
using Person.Shared.Utils;

namespace Person.Infrastructure.Db
{
    public class DbInitializer
    {
        public void Seed(PersonDbContext _context)
        {
            var seedDb = _context.Persons.Any();
            if (!seedDb)
            {
                var person1 = Domain.Models.PersonModels.Person.Create(
                    "ცეზარი",
                    "მშვენიერაძე",
                    Domain.Enums.Gender.Female,
                    "10001000100",
                    DateTimeUtils.Now.AddYears(-20),
                    null
                );
                person1.PicturePath = @"Resources\\Images\\a0eaae0e-fa99-4814-9f04-b8d7522aade9-download.jpg";
                _context.Persons.Add(person1);

                for (int i = 0; i < 20; i++)
                {
                    var city = City.Create($"City-{i}");
                    _context.Cities.Add(city);
                }
                _context.SaveChanges();
            }
        }
    }
}
