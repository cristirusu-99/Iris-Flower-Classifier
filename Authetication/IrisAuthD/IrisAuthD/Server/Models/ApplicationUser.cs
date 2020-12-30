using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IrisAuthD.Server.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    }
}
