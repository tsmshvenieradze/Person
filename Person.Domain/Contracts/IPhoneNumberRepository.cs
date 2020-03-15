using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Person.Domain.Models.CityModels;
using Person.Domain.Models.ExceptionModels;
using Person.Domain.Models.PhoneNumberModels;
using Person.Domain.Services.Models;

namespace Person.Domain.Contracts
{
    public interface IPhoneNumberRepository
    {
        PhoneNumber GetById(long id,bool asTracking = false);
        void Create(PhoneNumber phoneNumber);
        void Update(PhoneNumber phoneNumber);
        List<PhoneNumber> GetByPersonId(long personId);
    }
}
