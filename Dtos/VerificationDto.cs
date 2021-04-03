using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordlessAuthentication.Dtos
{
    public class VerificationDto
    {
        public string AuthToken { get; set; }
        public string PhoneNumber { get; set; }
    }
}
