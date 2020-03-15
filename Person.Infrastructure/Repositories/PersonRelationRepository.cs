using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Person.Domain.Contracts;
using Person.Domain.Models.PersonModels;
using Person.Infrastructure.Db;

namespace Person.Infrastructure.Repositories
{
    public class PersonRelationRepository : Repository<PersonDbContext>, IPersonRelationRepository
    {
        public PersonRelationRepository(PersonDbContext context) : base(context)
        {
        }

        public void Create(PersonRelation person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            _db.PersonRelations.Add(person);


        }

        public void Update(PersonRelation model)
        {
            if (model == null)
            {
                throw new ArgumentException("Model is empty");
            }

            var entity = _db.PersonRelations.Single(u => u.Id == model.Id);

            entity.RelationType = model.RelationType;

        }

        public PersonRelation GetById(long modelId, bool asTracking = false)
        {
            if (asTracking)
            {
                return _db.PersonRelations.AsTracking().FirstOrDefault(x => !x.IsDeleted && x.Id == modelId);
            }
            return _db.PersonRelations.AsNoTracking().FirstOrDefault(x => !x.IsDeleted && x.Id == modelId);
        }

        public List<PersonRelation> GetByPersonId(long? personId, bool asTracking = false)
        {
            if (asTracking)
            {
                return _db.PersonRelations.AsTracking()
                    .Include(x => x.Person)
                    .Where(x => !x.IsDeleted && x.PersonId == personId)
                    .ToList();
            }
            return _db.PersonRelations.AsNoTracking()
                .Include(x=>x.Person)
                .Where(x => !x.IsDeleted && x.PersonId == personId)
                .ToList();
        }

        public PersonRelation GetByRelatedPersonId(long? relatedPersonId, bool asTracking = false)
        {
            if (asTracking)
            {
                return _db.PersonRelations.AsTracking().FirstOrDefault(x => !x.IsDeleted && x.RelatedPersonId == relatedPersonId);
            }
            return _db.PersonRelations.AsNoTracking().FirstOrDefault(x => !x.IsDeleted && x.RelatedPersonId == relatedPersonId);
        }
    }
}
