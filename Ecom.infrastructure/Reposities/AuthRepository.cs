using Ecom.Core.DTO;
using Ecom.Core.Entities.Identity;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Identity;
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
                DispalyName = registerDTO.DisplayName,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
                return result.Errors.First().Description;

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
        public async Task<bool> SendEmailForForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            await IssueAndSendOtp(user, purpose: "reset");
            return true;
        }

        // ─── Reset Password ──────────────────────────────────────────────────────
        public async Task<string> ResetPassword(RestPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return "User not found.";

            if (!IsOtpValid(user, dto.Token))
                return "Invalid or expired OTP.";

            // Remove the password and set the new one directly
            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                return removeResult.Errors.First().Description;

            var addResult = await _userManager.AddPasswordAsync(user, dto.Password);
            if (!addResult.Succeeded)
                return addResult.Errors.First().Description;

            ClearOtp(user);
            await _userManager.UpdateAsync(user);
            return "Password reset successfully.";
        }

        // ─── Active Account (email confirmation via OTP) ──────────────────────────
        public async Task<bool> ActiveAccount(ActiveAccountDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;

            if (!IsOtpValid(user, dto.Token))
            {
                // OTP wrong / expired → issue a fresh one
                await IssueAndSendOtp(user, purpose: "active");
                return false;
            }

            user.EmailConfirmed = true;
            ClearOtp(user);
            await _userManager.UpdateAsync(user);
            return true;
        }

        // ─── Helpers ─────────────────────────────────────────────────────────────

        /// <summary>Generates a new OTP, persists it on the user, and emails it.</summary>
        private async Task IssueAndSendOtp(AppUser user, string purpose)
        {
            short otp = CreateOTP();
            user.OtpCode = otp;
            user.OtpExpiry = DateTime.UtcNow.Add(OtpLifetime);
            await _userManager.UpdateAsync(user);

            string otpString = otp.ToString();

            // Fixed: use separate variables instead of tuple deconstruction
            string subject = purpose == "reset" ? "Reset Password" : "Activate Account";
            string message = purpose == "reset"
                ? "Use the code below to reset your password:"
                : "Use the code below to activate your account:";

            var emailDto = new EmailDto(
                user.Email,
                _configuration["EmailSetting:From"],   // or hardcode sender
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
            return (short)Random.Shared.Next(100000, 999999);
        }

        public async Task SendEmail(string email, string code, string component, string subject, string message)
        {
            var dto = new EmailDto(
                email,
                _configuration["EmailSetting:From"],
                subject,
                EmailStringBody.Send(email, code, message));

            await _emailService.SendEmail(dto);
        }
    }
}