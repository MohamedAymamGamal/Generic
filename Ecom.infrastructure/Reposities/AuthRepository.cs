using Ecom.Core.DTO;
using Ecom.Core.Entities.Identity;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.Core.Sharing;
using Ecom.infrastructure.Reposities.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IGenrateToken _genrateToken;
        public AuthRepository(UserManager<AppUser> userManager, 
            IEmailService emailService, 
            SignInManager<AppUser> signInManager,
            IGenrateToken genrateToken
            )
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _genrateToken = genrateToken;

        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {

            if (registerDto == null)
            {
                return null;

            }
            if (await _userManager.FindByNameAsync(registerDto.UserName) != null)
            {
                return "UserName already exists";
            }
            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
            {

                return "Email already exists";


            }
            AppUser user = new AppUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName
            };
            var IdentityResult = await _userManager.CreateAsync(user, registerDto.Password);
            if(IdentityResult.Succeeded is not true)
            {
                return IdentityResult.Errors.ToList()[0].Description;
            }
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            SendEmail(user.Email,
                token,  
                "Welcome", "Welcome to Ecom", 
                "We are glad to have you here").Wait();
           
            return "nah i am done";

             
        }

        public async Task SendEmail(string email, string code, string component, string subject, string message)
        {
            var result = new EmailDto(email,
                "ma7048710@gmail.com",
                subject
                , EmailStringBody.send(email, code, component, message));
            await _emailService.SendEmail(result);
        }

        public async Task<string> LoginAsync(LoginDto login)
        {
            if (login == null)
            {
                return null;
            }
            var findUser = await _userManager.FindByEmailAsync(login.Email);

            if (!findUser.EmailConfirmed)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);    
                await SendEmail(findUser.Email, token, "active", "ActiveEmail", "Please active your email, click on button to active");

                return "Please confirem your email first, we have send activat to your E-mail";
            }

            var result = await _signInManager.CheckPasswordSignInAsync(findUser, login.Password, true);

            if (result.Succeeded)
            {
                return await _genrateToken.GetAndCreateToken(findUser);
            }

            return "please check your email and password, something went wrong";
        }

          public async Task<bool> SendEmailForForgetPassword(string email )
            {

                var findUser = await _userManager.FindByEmailAsync(email);
                if (findUser == null)
                {
                    return false;
                }
            else
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(findUser);
                await SendEmail(findUser.Email, token, "reset", "Reset Password", "Please click on button to reset your password");
            }

            return true;
            }

        public async Task<string> ResetPassword(RestPasswordDto restPasswordDto)
        {
            var findUser = await _userManager.FindByEmailAsync(restPasswordDto.Email);
            if (findUser == null)
            {
                return "User not found";
            }
            var result = await _userManager.ResetPasswordAsync(findUser, restPasswordDto.Token, restPasswordDto.Password);

            if (result.Succeeded)
            {
                return "Password reset successfully";
            }
            else
            {
                return result.Errors.ToList()[0].Description;
            }
        }
         //public async Task<string> ConfirmEmail(string email, string token)
        public async Task<bool> ActiveAccount(ActiveAccountDto activeAccountDto)
        {
            var findUser = await _userManager.FindByEmailAsync(activeAccountDto.Email);

            if (findUser == null)
            {
                return false;
            }
            var result = await _userManager.ConfirmEmailAsync(findUser, activeAccountDto.Token);
            if (result.Succeeded) {

                return true;
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);
            await SendEmail(findUser.Email, token, "active", "ActiveEmail", "Please active your email, click on button to active");

            return result.Succeeded;
        }

       
    }
    }