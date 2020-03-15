using System;
using System.Collections.Generic;
using System.Text;
using Person.Domain.Enums;
using Person.Shared.Utils;

namespace Person.Domain.Models.PhoneNumberModels
{
    public class PhoneNumber : Entity
    {
        public NumberType NumberType { get; set; }

        public string Number { get; set; }

        public long PersonId { get; set; }
        public static PhoneNumber Create(long newPersonId, NumberType numberType, string number)
        {
            return  new PhoneNumber
            {
                PersonId = newPersonId,
                Number =  number,
                NumberType =  numberType,
                CreateDate =  DateTimeUtils.Now
            };
          
        }

        public void Update( NumberType numberType, string number)
        {
            Number = number;
            NumberType = numberType;
        }

     
    }
}
