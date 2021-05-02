using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInformationAPI.Models
{
    public class ContactInfoContext : DbContext
    {
        public ContactInfoContext(DbContextOptions<ContactInfoContext> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<ContactInfo> ContactInfo { get; set; }
    }
}
