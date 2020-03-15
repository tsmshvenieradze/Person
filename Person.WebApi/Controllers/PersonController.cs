using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using Person.Domain.Enums;
using Person.Domain.Services;
using Person.Domain.Services._Person.Models;
using Person.Domain.Services.Models;
using Person.WebApi.Filters;

namespace Person.WebApi.Controllers
{
    [Route("api/person")]
    public class PersonController : BaseController
    {
        private readonly PersonService _userService;

        public PersonController(PersonService userService, IStringLocalizer<SharedResources> _localizer) : base(
            _localizer)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("add-person")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult AddPerson([FromBody] PersonCreateVm model)
        {
            var apiResult = Try(() => { return _userService.AddPerson(model); });

            return Ok(apiResult);
        }

        [HttpPost]
        [Route("update-person")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult UpdatePerson([FromBody] PersonUpdateVm model)
        {
            var apiResult = Try(() => { return _userService.UpdatePerson(model); });

            return Ok(apiResult);
        }

        [HttpPost]
        [Route("add-related-person")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult UpdatePerson([FromBody] RelatedPersonCreateVm model)
        {
            var apiResult = Try(() => { return _userService.AddRelatedPerson(model); });

            return Ok(apiResult);
        }

        [HttpPost]
        [Route("delete-person")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult DeletePerson([FromBody] DeletePersonVm model)
        {
            var apiResult = Try(() => { return _userService.DeletePerson(model); });

            return Ok(apiResult);
        }

        [HttpPost]
        [Route("delete-related-person")]

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult UpdatePerson([FromBody] DeleteRelatedPersonVm model)
        {
            var apiResult = Try(() => { return _userService.DeleteRelatedPerson(model); });

            return Ok(apiResult);
        }

        [HttpPost]
        [Route("upload-person-image")]
        public IActionResult Upload(long personId)
        {
            var file = Request.Form.Files[0];
            if (file.Length == 0)
            {
                return BadRequest();
            }

            if (!string.Equals(file.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(file.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(file.ContentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(file.ContentType, "image/gif", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(file.ContentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(file.ContentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }


            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


            var fileName = $"{Guid.NewGuid()}-{System.IO.Path.GetFileName(file.FileName)}";
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }


            var apiResult = Try(() => { return _userService.UploadImage(personId, dbPath); });
            return Ok(apiResult);
        }

        [HttpPost]
        [Route("get-person-info/{personId}")]
        public IActionResult GetPersonInfo(long personId)
        {
            var apiResult = Try(() => { return _userService.GetUserInfo(personId); });

            return Ok(apiResult);
        }



        [HttpGet]
        [Route("fast-search")]
        public IActionResult FastSearch(string firstname, string lastname, string privatenumber)
        {
            var apiResult = Try(() => { return _userService.FastSearch(firstname, lastname, privatenumber); });

            return Ok(apiResult);
        }



        [HttpGet]
        [Route("advanced-search")]
        public IActionResult AdvancedSearch(string firstName, string lastName, string privateNumber,
            DateTime? birthDateStart, DateTime? birthDateEnd, Gender gender,
            int page)
        {
            var apiResult = Try(() => { return _userService.AdvancedSearch(firstName, lastName, privateNumber, birthDateStart, birthDateEnd, gender, page); });

            return Ok(apiResult);
        }
        
        [HttpGet]
        [Route("get-report")]
        public IActionResult GetReport( )
        {
            var apiResult = Try(() => { return _userService.GetReport(); });

            return Ok(apiResult);
        }
    }
}
