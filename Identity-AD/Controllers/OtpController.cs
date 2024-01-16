// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using CustomIdentityServer4.Filters;
using CustomIdentityServer4.Models;
using CustomIdentityServer4.UserServices;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using ITokenService = CustomIdentityServer4.UserServices.ITokenService;
namespace CustomIdentityServer4.Controllers
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
    [AllowAnonymous]
    public class  OtpController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly IUserRepository userRepo;
        private readonly ICryptoService cryptoService;
        private readonly ITokenService tokenService;

        public OtpController(
            IIdentityServerInteractionService interaction,
            IEventService events,
            IUserRepository userRepo,
            ICryptoService cryptoService,
            ITokenService tokenService)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)

            this._interaction = interaction;
            this._events = events;
            this.cryptoService = cryptoService;
            this.tokenService = tokenService;
            this.userRepo = userRepo;
        }



        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ValidateOtp(Guid token, string returnUrl)
        {
            return View(new TwoFactorAuthModel
            {
                TokenId = token,
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateOtp(TwoFactorAuthModel model, string button)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            //if (context != null)
            //{
            //    await _events.RaiseAsync(new UserLoginFailureEvent(model.TokenId.ToString(), "invalid credentials", clientId: context?.Client.ClientId));
            //    ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);

            //    return View(model);
            //}

            var token = await this.tokenService.GetTokenDetails(model.TokenId);

            var descryptedUserName = this.cryptoService.Decrypt(token.UserName);

            var descryptedSubjectId = this.cryptoService.Decrypt(token.SubjectId);

            if (!ModelState.IsValid)
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(descryptedUserName, "Otp Required", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);

                return View(model);
            }
            // the user clicked the "cancel" button
            if (button != "login")
            {

            }

            if (token == null)
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(descryptedUserName, "invalid parameter", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidOtpErrorMessage);

                return View(model);
            }

            if (cryptoService.Decrypt(token.OTP).Trim() != model.OTP.Trim())
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(descryptedUserName, "invalid otp", clientId: context?.Client.ClientId));

                ModelState.AddModelError(string.Empty, AccountOptions.NotMatchOtoErrorMessage);

                return View(model);
            }
            if (token.ExpireOn <= DateTime.Now)
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(descryptedUserName, "otp expired", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.ExpiredOtpErrorMessage);

                return View(model);
            }
            //  var user = _users.FindByUsername(model.Username);
            await _events.RaiseAsync(new UserLoginSuccessEvent(descryptedUserName, descryptedUserName, descryptedSubjectId, clientId: context?.Client.ClientId));

            // only set explicit expiration here if user chooses "remember me". 
            // otherwise we rely upon expiration configured in cookie middleware.
            AuthenticationProperties props = null;
            if (AccountOptions.AllowRememberLogin && model.RememberLogin)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                };
            };
           var user=  await this.userRepo.FindBySubjectId(descryptedSubjectId);

            // issue authentication cookie with subject ID and username
            var isuser = new IdentityServerUser(descryptedSubjectId)
            {
                DisplayName =descryptedUserName,
                AdditionalClaims =
                        {
                            new Claim("", user.VoiceTelephoneNumber??"0548800")
                        }
            };

            await HttpContext.SignInAsync(isuser, props);

            await this.tokenService.RemoveToken(token.Id);

            //  return Redirect($"https://localhost:44305/signin-oidc");

            if (context != null)
            {
                //if (context.IsNativeClient())
                //{
                //    // The client is native, so this change in how to
                //    // return the response is for better UX for the end user.
                //    return this.LoadingPage("Redirect", model.ReturnUrl);
                //}

                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                return Redirect(model.ReturnUrl);
            }

            // request for a local page
            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else if (string.IsNullOrEmpty(model.ReturnUrl))
            {
                return Redirect("~/");
            }
            else
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

        }
      


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

 
  
         
    }
}
