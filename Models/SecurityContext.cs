using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab4_3_AspNetMVC_BlindDating.Models
{
    public class SecurityContext: IdentityDbContext
    {

        public SecurityContext()
        {

        }

        public SecurityContext(DbContextOptions<SecurityContext> options)
            :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

            if (!optionsBuilder.IsConfigured) 
            {

                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=BlindDating;Trusted_Connection=True;MultipleActiveResultSets=true");

            }
        
        }


    }
}
