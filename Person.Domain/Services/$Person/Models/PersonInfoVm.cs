using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Person.Domain.Enums;
using Person.Domain.Models.CityModels;
using Person.Domain.Models.PersonModels;
using Person.Domain.Models.PhoneNumberModels;

namespace Person.Domain.Services._Person.Models
{
   public class PersonInfoVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public string PrivateNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string Picture  { get; set; }

        public long? CityId { get; set; }
         
        public   List<PersonRelationVm> Relations { get; set; }
        public   List<PhoneNumberVm> PhoneNumbers { get; set; }

        public  string CityName { get; set; }

        public PersonInfoVm(Domain.Models.PersonModels.Person person,
            string picture, List<PersonRelation> relations, 
            List<PhoneNumber> phoneNumbers,
            string cityName)
        {
            Picture = picture;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Gender = person.Gender;
            PrivateNumber = person.PrivateNumber;
            BirthDate = person.BirthDate; 
            CityId = person.CityId;
            CityName = cityName;
            PhoneNumbers = phoneNumbers.Select(x=> new PhoneNumberVm(x)).ToList();
            Relations = relations.Select(x=> new PersonRelationVm(x)).ToList();

        }

      
    }
}
