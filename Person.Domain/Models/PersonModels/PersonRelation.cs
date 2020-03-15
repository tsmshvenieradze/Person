using System.Security.Cryptography.X509Certificates;
using Person.Domain.Enums;
using Person.Shared.Utils;

namespace Person.Domain.Models.PersonModels
{
    public class PersonRelation : Entity
    {
        public RelationType RelationType { get; set; }
        /// <summary>
        /// პირის იდენტიფიკატორი რომელტანაც ვუწერთ მეორე მომხმარებელს კავშირს (რომელსაც ვუმატებთ კავშირად)
        /// </summary>
        public long PersonId { get; set; }
        /// <summary>
        /// დაკავშირებული მომხმარებლის იდენტიფიკატორი
        /// </summary>
        public long RelatedPersonId { get; set; }

        public virtual Person Person { get; set; }
        public static PersonRelation Create(long personId, long relatePersonId, RelationType relationType)
        {
            return new PersonRelation
            {
                PersonId = personId,
                CreateDate = DateTimeUtils.Now,
                RelatedPersonId = relatePersonId,
                RelationType = relationType
            };
        }


    }
}
