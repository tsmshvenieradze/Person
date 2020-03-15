using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Person.Domain.Contracts;
using Person.Domain.Models.PhoneNumberModels;
using Person.Infrastructure.Db;

namespace Person.Infrastructure.Repositories
{
    public class PhoneNumberRepository : Repository<PersonDbContext>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(PersonDbContext context ) : base(context)
        { 
        }

        public void Create(PhoneNumber phoneNumber)
        {
            if (phoneNumber == null)
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }
            _db.PhoneNumbers.Add(phoneNumber);
             

        }

        public void Update(PhoneNumber phoneNumber)
        {
            if (phoneNumber == null)
            {
                throw new ArgumentException("Model is empty");
            } 

            var entity = _db.PhoneNumbers.Single(u => u.Id == phoneNumber.Id);
             
            entity.Number = phoneNumber.Number;
            entity.NumberType = phoneNumber.NumberType;
            entity.IsDeleted = phoneNumber.IsDeleted; 

           
        }

        public List<PhoneNumber> GetByPersonId(long personId)
        {
            return _db.PhoneNumbers.AsNoTracking().Where(x =>!x.IsDeleted&& x.PersonId == personId).ToList();
        }

        public PhoneNumber GetById(long id,bool asTracking =false)
        {
            if (asTracking)
            {
                return _db.PhoneNumbers.AsTracking().FirstOrDefault(x => !x.IsDeleted && x.Id == id);
            }

            return _db.PhoneNumbers.AsNoTracking().FirstOrDefault(x => !x.IsDeleted && x.Id == id);
        }

       

    
    }
}
