using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Person.Domain.Models.CityModels;
using Person.Domain.Models.ExceptionModels;
using Person.Domain.Services.Models;

namespace Person.Domain.Contracts
{
    public interface ICityRepository
    {
        City GetById(long id);
    }
}
