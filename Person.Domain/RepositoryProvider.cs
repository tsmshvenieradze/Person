using System;
using System.Collections.Generic;
using System.Text;
using Person.Domain.Contracts;

namespace Person.Domain
{
    public class RepositoryProvider
    {
        public RepositoryProvider(IPersonRepository person, ILoggerRepository logger, ICityRepository city, IPhoneNumberRepository phoneNumber, IPersonRelationRepository personRelation)
        {
            Person = person;
            Logger = logger;
            City = city;
            PhoneNumber = phoneNumber;
            PersonRelation = personRelation;
        }

        public IPersonRepository Person { get; }
        public IPersonRelationRepository PersonRelation { get; }
        public IPhoneNumberRepository PhoneNumber { get; }
        public ILoggerRepository Logger { get; }
        public ICityRepository City { get; }
    }
}
