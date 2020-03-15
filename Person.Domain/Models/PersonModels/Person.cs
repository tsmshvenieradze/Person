using System;
using System.Collections.Generic;
using Person.Domain.Enums;
using Person.Domain.Models.CityModels;
using Person.Domain.Models.PhoneNumberModels;
using Person.Shared.Utils;

namespace Person.Domain.Models.PersonModels
{
    public class Person : Entity
    {
        public static Person Create(string firstName, string lastName, Gender? gender, string privateNumber,
            DateTime birthDate, int? cityId)
        {
            return new Person
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                PrivateNumber = privateNumber,
                BirthDate = birthDate,
                CityId = cityId,
                CreateDate = DateTimeUtils.Now,
            };
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public string PrivateNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string PicturePath { get; set; }

        public long? CityId { get; set; }
        /// <summary>
        /// ვისგანაც  ხდება მისი შექმნა
        /// </summary>
        public virtual PersonRelation Relation { get; set; }

        /// <summary>
        /// მისგან შექმნილ  ფიზიკურ პირებთან კავშირი
        /// </summary>
        public virtual List<PersonRelation> Relations { get; set; }
        public virtual List<PhoneNumber> PhoneNumbers { get; set; }
         

        public void Update(string firstName, string lastName, Gender? gender, string privateNumber,
            DateTime birthDate, int? cityId)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            PrivateNumber = privateNumber;
            BirthDate = birthDate;
            CityId = cityId;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void ChangeImagePath(string path)
        {
            PicturePath = path;
        }
    }
}
