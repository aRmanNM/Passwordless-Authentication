using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordlessAuthentication.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(IdentityUser user);
    }
}
