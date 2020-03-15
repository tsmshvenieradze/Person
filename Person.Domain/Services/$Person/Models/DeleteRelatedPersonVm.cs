using System;
using System.Collections.Generic;
using System.Text;

namespace Person.Domain.Services._Person.Models
{
    public class DeleteRelatedPersonVm : ValidationObject

    {
        public long? PersonId { get; set; }

        public override bool Validate()
        {
            if (!PersonId.HasValue || PersonId.Value <1)
            {
                throw new DomainException("Person Id Invalid", ExceptionLevel.Error);
            }
            return base.Validate();
        }
    }
}
