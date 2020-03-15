using System;
using System.Collections.Generic;
using System.Text;
using Person.Domain.Enums;
using Person.Domain.Models.PhoneNumberModels;
using Person.Shared.Utils;

namespace Person.Domain.Services._Person.Models
{
    public class PhoneNumberVm : ValidationObject
    {
        public PhoneNumberVm()
        {
            
        }
        public PhoneNumberVm(PhoneNumber phoneNumber)
        {
            Id = phoneNumber.Id;
            NumberType = phoneNumber.NumberType;
            Number = phoneNumber.Number;
        }

        public long? Id { get; set; }
        public NumberType NumberType { get; set; }

        public string Number { get; set; }
        public override bool Validate()
        {
            if (!EnumUtils.HasValue(NumberType))
            {
                throw new DomainException("NumberType invalid", ExceptionLevel.Error);
            }

            if (string.IsNullOrWhiteSpace(Number) || Number.Length < 4 || Number.Length > 50)
            {
                throw new DomainException("PhoneNumber invalid", ExceptionLevel.Error);
            }
            return base.Validate();
        }
    }
}
