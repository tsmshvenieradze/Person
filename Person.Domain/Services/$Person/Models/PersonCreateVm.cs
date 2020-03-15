using System;
using System.Collections.Generic;
using Person.Domain.Enums;
using Person.Domain.Models.PhoneNumberModels;
using Person.Domain.Services._Person.Models;
using Person.Shared.Utils;

namespace Person.Domain.Services.Models
{
    public class PersonCreateVm : ValidationObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public string PrivateNumber { get; set; }

        public DateTime BirthDate { get; set; }


        public int? CityId { get; set; }

        public List<PhoneNumberVm> PhoneNumbers { get; set; }

        public override bool Validate()
        {
            if (!RegexUtils.IsValidFirstName(FirstName))
            {
                throw new DomainException("FirstName invalid", ExceptionLevel.Error);
            }
            if (!RegexUtils.IsValidLastName(LastName))
            {
                throw new DomainException("LastName invalid", ExceptionLevel.Error);
            }

            if (Gender.HasValue && !EnumUtils.HasValue(Gender.Value))
            {
                throw new DomainException("Gender invalid", ExceptionLevel.Error);
            }

            if (RegexUtils.IsValidPrivateNumber(PrivateNumber))
            {
                throw new DomainException("PrivateNumber invalid", ExceptionLevel.Error);
            }

            if (BirthDate == DateTime.MinValue || BirthDate ==DateTime.MaxValue)
            {
                throw new DomainException("BirthDate invalid",ExceptionLevel.Error);
            }

           var  age = DateTimeUtils.Now.Year - BirthDate.Year;

            if (BirthDate > DateTimeUtils.Now.AddYears(-age))
            {
                age--;
            }
             
            if( age < 18)
            {
                throw new DomainException("Person age error",ExceptionLevel.Error);
            }


            if (PhoneNumbers != null)
            {
                foreach (var phoneNumberVm in PhoneNumbers)
                {
                    phoneNumberVm.Validate();
                }
            }
            return base.Validate();
        }
    }
}
