using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Person.Domain.Enums;
using Person.Domain.Models.PersonModels;
using Person.Domain.Models.PhoneNumberModels;
using Person.Domain.Services._Person.Models;
using Person.Domain.Services.Models;
using Person.Shared.Utils;

namespace Person.Domain.Services
{
    public class PersonService
    {

        private readonly RepositoryProvider _repository;
        public PersonService(RepositoryProvider repository)
        {
            this._repository = repository;
        }

        public ServiceResult<bool> AddPerson(PersonCreateVm model)
        {
            model.Validate();

            if (model.CityId.HasValue)
            {
                var city = _repository.City.GetById(model.CityId.Value);
                if (city == null)
                {
                    throw new DomainException("City not found", ExceptionLevel.Error);
                }
            }



            var newPerson = Domain.Models.PersonModels.Person.Create(model.FirstName,
                model.LastName,
                model.Gender,
                model.PrivateNumber,
                model.BirthDate,
                model.CityId);

            _repository.Person.Create(newPerson);
            newPerson.PhoneNumbers = new List<PhoneNumber>();
            foreach (var modelPhoneNumber in model.PhoneNumbers)
            {
                var newPhoneNumber =
                    PhoneNumber.Create(newPerson.Id, modelPhoneNumber.NumberType, modelPhoneNumber.Number);
                newPerson.PhoneNumbers.Add(newPhoneNumber);
            }

            _repository.Person.Save();
            return new ServiceResult<bool>
            {
                Status = ServiceResultStatus.Success
            };
        }
        public ServiceResult<bool> AddRelatedPerson(RelatedPersonCreateVm model)
        {
            model.Validate();

            if (!model.PersonId.HasValue)
            {
                throw new DomainException("Person Id invalid", ExceptionLevel.Error);
            }


            var person = _repository.Person.GetById(model.PersonId.Value);
            if (person == null)
            {
                throw new DomainException("Person Not Found", ExceptionLevel.Error);
            }


            if (model.CityId.HasValue)
            {
                var city = _repository.City.GetById(model.CityId.Value);
                if (city == null)
                {
                    throw new DomainException("City not found", ExceptionLevel.Error);
                }
            }



            var newPerson = Domain.Models.PersonModels.Person.Create(model.FirstName,
                model.LastName,
                model.Gender,
                model.PrivateNumber,
                model.BirthDate,
                model.CityId);

            _repository.Person.Create(newPerson);
            newPerson.PhoneNumbers = new List<PhoneNumber>();
            foreach (var modelPhoneNumber in model.PhoneNumbers)
            {
                var newPhoneNumber =
                    PhoneNumber.Create(newPerson.Id, modelPhoneNumber.NumberType, modelPhoneNumber.Number);
                newPerson.PhoneNumbers.Add(newPhoneNumber);
            }


            newPerson.Relation = PersonRelation.Create(model.PersonId.Value, newPerson.Id, model.RelationType);


            _repository.Person.Save();
            return new ServiceResult<bool>
            {
                Status = ServiceResultStatus.Success
            };
        }
        public ServiceResult<bool> UpdatePerson(PersonUpdateVm model)
        {
            model.Validate();
            var person = _repository.Person.GetById(model.Id);
            if (person == null)
            {
                throw new DomainException("Person Not Found", ExceptionLevel.Error);
            }

            if (model.CityId.HasValue)
            {
                var city = _repository.City.GetById(model.CityId.Value);
                if (city == null)
                {
                    throw new DomainException("City not found", ExceptionLevel.Error);
                }
            }



            person.Update(model.FirstName,
                model.LastName,
                model.Gender,
                model.PrivateNumber,
                model.BirthDate,
                model.CityId);

            _repository.Person.Update(person);

            var oldNumber = _repository.PhoneNumber.GetByPersonId(person.Id);

            var deletedNumbers = oldNumber.Where(x => model.PhoneNumbers.All(ph => x.Id != ph.Id)).ToList();
            var newNumbers = model.PhoneNumbers.Where(x => !x.Id.HasValue || oldNumber.All(on => on.Id != x.Id))
                .ToList();
            var updatedNumbers = model.PhoneNumbers.Where(x => oldNumber.Any(old => old.Id == x.Id)).ToList();
            foreach (var modelPhoneNumber in newNumbers)
            {
                var newPhoneNumber =
                    PhoneNumber.Create(person.Id, modelPhoneNumber.NumberType, modelPhoneNumber.Number);
                _repository.PhoneNumber.Create(newPhoneNumber);
            }

            foreach (var deletedNumber in deletedNumbers)
            {
                var old = _repository.PhoneNumber.GetById(deletedNumber.Id, true);
                old?.Delete();
            }

            foreach (var updatedNumber in updatedNumbers)
            {
                if (updatedNumber.Id != null)
                {
                    var old = _repository.PhoneNumber.GetById(updatedNumber.Id.Value, true);
                    old?.Update(updatedNumber.NumberType, updatedNumber.Number);
                }
            }


            _repository.Person.Save();
            return new ServiceResult<bool>
            {
                Status = ServiceResultStatus.Success
            };
        }

        public ServiceResult<bool> DeletePerson(DeletePersonVm model)
        {
            model.Validate();
            var person = _repository.Person.GetById(model.PersonId.Value, true);
            if (person == null)
            {
                throw new DomainException("Person Not Found", ExceptionLevel.Error);
            }


            person.Delete();


            var relations = _repository.PersonRelation.GetByPersonId(model.PersonId, true);

            for (int i = 0; i < relations.Count; i++)
            {

                relations[i].Delete();
            }
            var relation = _repository.PersonRelation.GetByRelatedPersonId(person.Id, true);
            relation.Delete();
            _repository.Person.Save();
            return new ServiceResult<bool>
            {
                Status = ServiceResultStatus.Success
            };
        }

        public ServiceResult<bool> DeleteRelatedPerson(DeleteRelatedPersonVm model)
        {
            model.Validate();
            var person = _repository.Person.GetById(model.PersonId.Value, true);
            if (person == null)
            {
                throw new DomainException("Person Not Found", ExceptionLevel.Error);
            }

            var personIds = _repository.PersonRelation.GetByPersonId(model.PersonId).Select(x => x.RelatedPersonId).ToList();


            foreach (var personId in personIds)
            {
                var relatedPerson = _repository.Person.GetById(personId, true);
                if (relatedPerson == null)
                {
                    throw new DomainException("relatedPerson Not Found", ExceptionLevel.Error);
                }


                relatedPerson.Delete();


                var relations = _repository.PersonRelation.GetByPersonId(personId, true);

                for (int i = 0; i < relations.Count; i++)
                {

                    relations[i].Delete();
                }

                var relation = _repository.PersonRelation.GetByRelatedPersonId(relatedPerson.Id, true);
                relation.Delete();
            }

            _repository.Person.Save();
            return new ServiceResult<bool>
            {
                Status = ServiceResultStatus.Success
            };
        }


        public ServiceResult<PersonInfoVm> GetPersonInfo(long personId)
        {
            var person = _repository.Person.GetById(personId);
            if (person == null)
            {
                throw new DomainException("Person Not Found", ExceptionLevel.Error);
            }

            var picture = GetImageBase64(person.Id);

            var relations = _repository.PersonRelation.GetByPersonId(person.Id);
            var phoneNumbers = _repository.PhoneNumber.GetByPersonId(person.Id);

            var cityName = string.Empty;
            if (person.CityId.HasValue)
            {
                var city = _repository.City.GetById(person.CityId.Value);

                cityName = city.CityName;
            }

            return new ServiceResult<PersonInfoVm>
            {
                Status = ServiceResultStatus.Success,
                Result = new PersonInfoVm(person, picture, relations, phoneNumbers, cityName)
            };
        }

        public ServiceResult<bool> UploadImage(long personId, string dbPath)
        {
            var person = _repository.Person.GetById(personId);
            if (person == null)
            {
                throw new DomainException("Person Not Found", ExceptionLevel.Error);
            }

            person.ChangeImagePath(dbPath);
            _repository.Person.Save();
            return new ServiceResult<bool>
            {
                Status = ServiceResultStatus.Success
            };
        }

        private string GetImageBase64(long personId)
        {
            var person = _repository.Person.GetById(personId);
            if (person == null)
            {
                throw new DomainException("Person Not Found", ExceptionLevel.Error);
            }

            if (!string.IsNullOrWhiteSpace(person.PicturePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(person.PicturePath);

                return "data:image/png;base64," + Convert.ToBase64String(b);

            }

            return string.Empty;

        }

        public ServiceResult<List<PersonInfoVm>> FastSearch(string firstname, string lastname, string privatenumber, int page = 0)
        {
            var persons = _repository.Person.FastSearch(firstname, lastname, privatenumber, page);
            var result = new List<PersonInfoVm>();
            foreach (var person in persons)
            {
                var picture = GetImageBase64(person.Id);

                var relations = _repository.PersonRelation.GetByPersonId(person.Id);
                var phoneNumbers = _repository.PhoneNumber.GetByPersonId(person.Id);

                var cityName = string.Empty;
                if (person.CityId.HasValue)
                {
                    var city = _repository.City.GetById(person.CityId.Value);

                    cityName = city.CityName;
                }

                result.Add(new PersonInfoVm(person, picture, relations, phoneNumbers, cityName));

            }
            return new ServiceResult<List<PersonInfoVm>>
            {
                Status = ServiceResultStatus.Success,
                Result = result
            };
        }

        public ServiceResult<List<PersonInfoVm>> AdvancedSearch(string firstName, string lastName, string privateNumber,
            DateTime? birthDateStart, DateTime? birthDateEnd, Gender gender,
            int page = 0)
        {
            var persons = _repository.Person.AdvancedSearch(firstName, lastName, privateNumber, birthDateStart, birthDateEnd, gender, page);
            var result = new List<PersonInfoVm>();
            foreach (var person in persons)
            {
                var picture = GetImageBase64(person.Id);

                var relations = _repository.PersonRelation.GetByPersonId(person.Id);
                var phoneNumbers = _repository.PhoneNumber.GetByPersonId(person.Id);

                var cityN = string.Empty;
                if (person.CityId.HasValue)
                {
                    var city = _repository.City.GetById(person.CityId.Value);

                    cityN = city.CityName;
                }

                result.Add(new PersonInfoVm(person, picture, relations, phoneNumbers, cityN));

            }
            return new ServiceResult<List<PersonInfoVm>>
            {
                Status = ServiceResultStatus.Success,
                Result = result
            };
        }


        public ServiceResult<List<ReportVm>> GetReport()
        {

            var Persons = _repository.Person.GetAll();
            var result = new List<ReportVm>();
            foreach (var person in Persons)
            {
                var relations = person.Relations;
                if (person.Relation != null)
                {
                    relations.Add(person.Relation);
                }
              

                var personRelations = relations.GroupBy(x => x.RelationType).Select(g => new { name = g.Key, count = g.Count() }).ToList();

                foreach (var rel in personRelations)
                {
                    var reportVm = new ReportVm
                    {
                        PersonFullName = $"{person.FirstName} {person.LastName}",
                        RelationType = rel.name,
                        Count = rel.count

                    };
                    result.Add(reportVm);

                }
            }
            return  new ServiceResult<List<ReportVm>>
            {
                Status = ServiceResultStatus.Success,
                Result = result
            };
        }
    }
}
