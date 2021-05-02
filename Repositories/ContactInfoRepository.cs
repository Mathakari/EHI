using ContactInformationAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInformationAPI.Repositories
{
    public class ContactInfoRepository : IContactInfoRespository
    {
        private readonly ContactInfoContext infoContext;

        public ContactInfoRepository(ContactInfoContext context)
        {
            infoContext = context;
        }

        public async Task<ContactInfo> Create(ContactInfo contact)
        {
            infoContext.ContactInfo.Add(contact);
            await infoContext.SaveChangesAsync();
            return contact;
        }

        public async Task Delete(string emailID)
        {
            var contactToDel = await infoContext.ContactInfo.FindAsync(emailID);
            infoContext.ContactInfo.Remove(contactToDel);
            await infoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactInfo>> Get()
        {
            return await infoContext.ContactInfo.ToListAsync();
        }

        public async Task<ContactInfo> Get(string emailID)
        {
            return await infoContext.ContactInfo.FindAsync(emailID);
        }


        public async Task Update(ContactInfo contact)
        {
            infoContext.Entry(contact).State = EntityState.Modified;
            await infoContext.SaveChangesAsync();
        }
    }
}
