using Ecom.Core.DTO;
using Ecom.Core.Entities.Identity;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ecom.infrastructure.Reposities
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IGenrateToken _genrateToken;
        private readonly IConfiguration _configuration;

        private static readonly TimeSpan OtpLifetime = TimeSpan.FromMinutes(10);

        public AuthRepository(
            UserManager<AppUser> userManager,
            IEmailService emailService,
            SignInManager<AppUser> signInManager,
            IGenrateToken genrateToken,
            IConfiguration configrtion
            )
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _genrateToken = genrateToken;
            _configuration = configrtion;
        }

        // ─── Register ────────────────────────────────────────────────────────────
        public async Task<string> RegisterAsync(RegisterDto registerDTO)
        {
            if (registerDTO == null) return null;

            if (await _userManager.FindByNameAsync(registerDTO.UserName) is not null)
                return "This username is already registered.";

            if (await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
                return "This email is already registered.";

         
            var user = new AppUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                DispalyName = registerDTO.DisplayName
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
                return result.Errors.First().Description;

            // Generates, saves, and emails the OTP in one definitive step
            await IssueAndSendOtp(user, purpose: "active");

            return await _genrateToken.GetAndCreateToken(user);
        }

        // ─── Login ───────────────────────────────────────────────────────────────
        public async Task<string> LoginAsync(LoginDto login)
        {
            if (login == null) return null;

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return "Please check your email and password.";

            if (!user.EmailConfirmed)
            {
                await IssueAndSendOtp(user, purpose: "active");
                return "Please confirm your email first. A new OTP has been sent to your inbox.";
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: true);
            if (result.Succeeded)
                return await _genrateToken.GetAndCreateToken(user);

            return "Please check your email and password.";
        }

        // ─── Forget Password ─────────────────────────────────────────────────────
        public async Task<bool> SendEmailForForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordDto.Email);
            if (user == null) return false;

            await IssueAndSendOtp(user, purpose: "reset");
            return true;
        }

        // ─── Reset Password ──────────────────────────────────────────────────────
        public async Task<string> ResetPassword(RestPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return "User not found.";

            if (!IsOtpValid(user, dto.OtpCode.ToString()))
                return "Invalid or expired OTP.";

            // SECURE: Instead of destroying the password first, use ResetPasswordAsync with Identity's token system
            // Or if you want a direct overwrite safely:
            var restToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, restToken, dto.Password);

            if (!result.Succeeded)
                return result.Errors.First().Description;

            ClearOtp(user);
            await _userManager.UpdateAsync(user);
            return "the password rest ";
        }

        // ─── Active Account (email confirmation via OTP) ──────────────────────────
        public async Task<bool> ActiveAccount(ActiveAccountDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;

            if (!IsOtpValid(user, dto.OtpCode.ToString()))
            {
                // OPTIONAL: Regenerating a token automatically on failure means if they typos, 
                // the old one instantly dies. If you want this behavior, keep this line. 
                // If you want them to be able to retry typing, remove this line.
                await IssueAndSendOtp(user, purpose: "active");
                return false;
            }

            user.EmailConfirmed = true;
            ClearOtp(user);

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        // ─── Helpers ─────────────────────────────────────────────────────────────

        private async Task IssueAndSendOtp(AppUser user, string purpose)
        {
            short otp = CreateOTP();
            user.OtpCode = otp;
            user.OtpExpiry = DateTime.UtcNow.Add(OtpLifetime);
            await _userManager.UpdateAsync(user);

            string otpString = otp.ToString();
            string subject = purpose == "reset" ? "Reset Password" : "Activate Account";
            string message = purpose == "reset"
                ? "Use the code below to reset your password:"
                : "Use the code below to activate your account:";

            var emailDto = new EmailDto(
                user.Email,
                _configuration["EmailSetting:From"],
                subject,
                EmailStringBody.Send(user.Email, otpString, message)
            );

            await _emailService.SendEmail(emailDto);
        }

        private static bool IsOtpValid(AppUser user, string submittedOtp)
        {
            return user.OtpCode != null
                && user.OtpExpiry != null
                && user.OtpExpiry > DateTime.UtcNow
                && user.OtpCode.ToString() == submittedOtp;
        }

        private static void ClearOtp(AppUser user)
        {
            user.OtpCode = null;
            user.OtpExpiry = null;
        }

        private short CreateOTP()
        {
            return (short)Random.Shared.Next(1000, 9999);
        }
    }
}