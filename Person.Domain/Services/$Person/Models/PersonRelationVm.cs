using System;
using System.Collections.Generic;
using System.Text;
using Person.Domain.Enums;
using Person.Domain.Models.PersonModels;

namespace Person.Domain.Services._Person.Models
{
    public class PersonRelationVm
    {
        public RelationType RelationType { get; set; }  

        public  string RelatedPerson { get; set; }

        public PersonRelationVm(PersonRelation personRelation)
        {
            RelationType = personRelation.RelationType;

            RelatedPerson = $"{personRelation.Person.FirstName} {personRelation.Person.LastName}";
        } 
    }
}
