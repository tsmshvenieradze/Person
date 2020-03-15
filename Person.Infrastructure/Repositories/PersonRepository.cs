using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Person.Domain.Contracts;
using Person.Domain.Enums;
using Person.Infrastructure.Db;
using Person.Shared.Utils;

namespace Person.Infrastructure.Repositories
{
    public class PersonRepository : Repository<PersonDbContext>, IPersonRepository
    {
        public PersonRepository(PersonDbContext context) : base(context)
        {
        }

        public void Create(Domain.Models.PersonModels.Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            _db.Persons.Add(person);


        }

        public void Update(Domain.Models.PersonModels.Person model)
        {
            if (model == null)
            {
                throw new ArgumentException("Model is empty");
            }

            var entity = _db.Persons.Single(u => u.Id == model.Id);

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.CityId = model.CityId;
            entity.Gender = model.Gender;
            entity.PrivateNumber = model.PrivateNumber;
            entity.PicturePath = model.PicturePath;
            entity.BirthDate = model.BirthDate;


        }

        public Domain.Models.PersonModels.Person GetById(long modelId, bool asTracking = false)
        {
            if (asTracking)
            {
                return _db.Persons.AsTracking().Include(x => x.Relation).FirstOrDefault(x => !x.IsDeleted && x.Id == modelId);
            }
            return _db.Persons.AsNoTracking().Include(x => x.Relation).FirstOrDefault(x => !x.IsDeleted && x.Id == modelId);
        }

        public List<Domain.Models.PersonModels.Person> FastSearch(string firstName, string lastName, string privateNumber, int page = 0)
        {

            var query = _db.Persons.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(i => i.FirstName.Contains(firstName)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(i => i.LastName.Contains(lastName)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(privateNumber))
            {
                query = query.Where(i => i.PrivateNumber.Contains(privateNumber)).AsQueryable();
            }

            return query.Where(x => !x.IsDeleted).Skip(50 * page).Take(50).ToList();
        }

        public List<Domain.Models.PersonModels.Person> AdvancedSearch(string firstName, string lastName, string privateNumber,
            DateTime? birthDateStart, DateTime? birthDateEnd, Gender gender,
            int page = 0)
        {

            var query = _db.Persons.AsNoTracking()
                .Include(x => x.PhoneNumbers).AsQueryable();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(i => i.FirstName.Contains(firstName)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(i => i.LastName.Contains(lastName)).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(privateNumber))
            {
                query = query.Where(i => i.PrivateNumber.Contains(privateNumber)).AsQueryable();
            }

            if (birthDateStart.HasValue)
            {
                query = query.Where(i => i.BirthDate >= birthDateStart).AsQueryable();
            }

            if (birthDateEnd.HasValue)
            {
                query = query.Where(i => i.BirthDate < birthDateEnd).AsQueryable();
            }

            if (!EnumUtils.HasValue(gender))
            {
                query = query.Where(i => i.Gender == gender).AsQueryable();
            }




            return query.Where(x => !x.IsDeleted).Skip(50 * page).Take(50).ToList();
        }

        public List<Domain.Models.PersonModels.Person> GetAll()
        {
            return _db.Persons.AsNoTracking()
                .Include(x => x.Relations)
                .Include(x => x.Relation)
                .Where(x => !x.IsDeleted).ToList();
        }
    }
}
