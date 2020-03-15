using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Person.Domain.Contracts;
using Person.Domain.Models.CityModels;
using Person.Domain.Models.ExceptionModels;
using Person.Domain.Models.LogModels;
using Person.Domain.Services.Models;
using Person.Infrastructure.Db;
using Person.Infrastructure.Repositories;

namespace Person.Infrastructure.Repositories
{
    public class CityRepository : Repository<PersonDbContext>, ICityRepository
    {
        public CityRepository(PersonDbContext context) : base(context)
        {
        }


        public City GetById(long id)
        {
            return _db.Cities.FirstOrDefault(x => x.Id == id);
        }
    }
}
