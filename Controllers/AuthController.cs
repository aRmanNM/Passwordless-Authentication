using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PasswordlessAuthentication.Dtos;
using PasswordlessAuthentication.Interfaces;

namespace PasswordlessAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ISMSService _smsService;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper,  ITokenService tokenService, ISMSService smsService)
        {
            _smsService = smsService;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.PhoneNumber); // used phoneNumber as userName! :|
            if (user == null) return Unauthorized("ورود نامعتبر");
            var authToken = await _userManager.GenerateUserTokenAsync(user, "Default", "passwordless-auth");
            var res = _smsService.SendSMS(user.PhoneNumber, authToken);

            if (!res)
            {
                return BadRequest("ارسال پیامک با مشکل روبرو شد");
            }

            return Ok();
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<RegisterDto, IdentityUser>(registerDto);
            var phoneNumberExists = await _userManager.FindByNameAsync(registerDto.PhoneNumber) != null;
            if (phoneNumberExists) return BadRequest();
            string authToken = await _userManager.GenerateUserTokenAsync(user, "Default", "passwordless-auth");
            authToken = HttpUtility.UrlEncode(authToken);
            var res = _smsService.SendSMS(user.PhoneNumber, authToken);
            if (!res)
            {
                return BadRequest("failed to send sms");
            }

            var result = await _userManager.CreateAsync(user);
            // await _userManager.AddToRoleAsync(user, "Member");
            if (!result.Succeeded) return BadRequest();
            return Ok();
        }

        [HttpPost("smsverification")]
        public async Task<ActionResult> SMSVerification(VerificationDto verificationDto)
        {
            var user = await _userManager.FindByNameAsync(verificationDto.PhoneNumber);
            bool isValid = await _userManager.VerifyUserTokenAsync(user, "Default", "passwordless-auth", verificationDto.AuthToken);
            if (!isValid)
            {
                return Unauthorized("not authorized!");
            }

            await _userManager.UpdateSecurityStampAsync(user);
            var userDto = MapAppUserToUserDto(user);
            return Ok(userDto.Result);
        }

        [HttpGet("phoneexists")]
        public async Task<ActionResult<bool>> CheckPhoneNumberExistsAsync([FromQuery] string phoneNumber)
        {
            return await _userManager.FindByNameAsync(phoneNumber) != null;
        }

        public async Task<UserDto> MapAppUserToUserDto(IdentityUser user)
        {
            return new UserDto
            {
                Token = await _tokenService.CreateToken(user),
                UserName = user.UserName
            };
        }
    }
}