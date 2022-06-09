using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordlessAuthentication.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(IdentityUser user);
    }
}
