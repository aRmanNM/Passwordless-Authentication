﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordlessAuthentication.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "وارد کردن نام ضروریست")]
        public string DisplayName { get; set; }

        // [Required(ErrorMessage="وارد کردن ایمیل ضروریست")]
        // [EmailAddress(ErrorMessage="ایمیل را به شکل صحیح وارد کنید")]
        // public string Email { get; set; }

        // [Required(ErrorMessage="وارد کردن رمز عبور ضروریست")]
        // [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "رمز عبور به شکل صحیح وارد نشده است - ترکیبی از حروف بزرگ، کوچک، اعداد، و علائم نگارشی به طول حداقل ۶ کاراکتر")]
        // public string Password { get; set; }

        [Required(ErrorMessage = "وارد کردن شماره تماس ضروریست")]
        [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$")]
        public string PhoneNumber { get; set; }
    }
}
