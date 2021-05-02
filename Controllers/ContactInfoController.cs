using ContactInformationAPI.Models;
using ContactInformationAPI.Properties;
using ContactInformationAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactInformationAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactInfoRespository infoRepository;
        private readonly Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex PhoneNumberRegex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
        private readonly Regex NameRegex = new Regex(@"^[a-zA-Z ]+$");

        public ContactInfoController(IContactInfoRespository repository)
        {
            infoRepository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ContactInfo>> GetContactInfos()
        {
            return await infoRepository.Get();
        }

        [HttpPost]
        public async Task<ActionResult<ContactInfo>> PostContactInfos([FromBody] ContactInfo info)
        {
            var message = ValidateContactInfo(info);

            if (string.IsNullOrEmpty(message))
            {
                var newContactInfo = await infoRepository.Create(info);
                return CreatedAtAction(nameof(GetContactInfos), newContactInfo);
            }
            else
            {
                throw new ArgumentException(message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutContactInfos(ContactInfo contact, [FromBody] ContactInfo info)
        {
            await infoRepository.Update(contact);

            return NoContent();
        }

        [HttpDelete("{emailID}")]
        public async Task<ActionResult> Delete(string emailID)
        {
            var bookToDelete = await infoRepository.Get(emailID);
            if (bookToDelete == null)
                return NotFound();

            await infoRepository.Delete(bookToDelete.Email);
            return NoContent();
        }

        /// <summary>
        /// Validate Contact information provided for insertion.
        /// </summary>
        /// <param name="info">Provided contact information</param>
        /// <returns></returns>
        private string ValidateContactInfo (ContactInfo info)
        {
            if (string.IsNullOrEmpty(info.Email) || string.IsNullOrEmpty(info.LastName))
            {
                return Resource.EmailORLastName;
            }
            else if (!((emailRegex.IsMatch(info.Email) && NameRegex.IsMatch(info.LastName))
                    || NameRegex.IsMatch(info.FirstName) || PhoneNumberRegex.IsMatch(info.PhoneNumber)))
            {
                return Resource.CheckParamter;
            }
            else 
            {
                return string.Empty;
            }

        }
    }
}
