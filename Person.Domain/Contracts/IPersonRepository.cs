using System;
using System.Collections.Generic;
using System.Text;
using Person.Domain.Enums;

namespace Person.Domain.Contracts
{
    public interface IPersonRepository : IRepository
    {
        void Create(Domain.Models.PersonModels.Person person);

        void Update(Domain.Models.PersonModels.Person model);
        Models.PersonModels.Person GetById(long modelId,bool asTracking = false);
        List<Models.PersonModels.Person> FastSearch(string firstname, string lastname, string privatenumber, int page);

        List<Domain.Models.PersonModels.Person> AdvancedSearch(string firstName, string lastName, string privateNumber,
            DateTime? birthDateStart, DateTime? birthDateEnd, Gender gender, 
            int page);

        List<Models.PersonModels.Person> GetAll();
    }
}
