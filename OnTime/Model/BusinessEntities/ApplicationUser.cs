using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Model.BusinessEntities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientPersonal ClientPersonal { get; set; }
    }
}
