using ContactInformationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInformationAPI.Repositories
{
    public interface IContactInfoRespository
    {
        Task<IEnumerable<ContactInfo>> Get();
        Task<ContactInfo> Get(string emailID);
        Task<ContactInfo> Create(ContactInfo contact);
        Task Update(ContactInfo contact);
        Task Delete(string emailID);
    }
}
