﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ez7zu6.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ez7zu6.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            //var loggedIn = base.User.Identity.IsAuthenticated;

            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
                return View();

            var canAuthenticate = await CanAuthenticateUser(model.Username, model.Password);
            if (!canAuthenticate)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            var identity = new ClaimsIdentity(LoadClaims(model), "login");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return Redirect(returnUrl);
        }

        private async Task<bool> CanAuthenticateUser(string username, string password)
        {
            // todo: link to database
            return (username == "john@test.com");
        }

        private Claim[] LoadClaims(LoginViewModel model)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, model.Username), };
            return claims;
        }
    }
}