using System;
using System.Collections.Generic;
using System.Text;
using Person.Domain.Models.PersonModels;

namespace Person.Domain.Contracts
{
    public interface IPersonRelationRepository : IRepository
    {
        void Create(PersonRelation personRelation);

        void Update(PersonRelation model);
        PersonRelation GetById(long modelId, bool asTracking = false);
        List<PersonRelation> GetByPersonId(long? personId, bool asTracking = false);
        PersonRelation GetByRelatedPersonId(long? relatedPersonId, bool asTracking = false);
    }
}
