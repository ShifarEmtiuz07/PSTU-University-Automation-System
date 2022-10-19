using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PSTU_Automation1.Models.Enums;
using PSTU_Automation1.ViewModels;
using PSTU_Automation1.Data;
using PSTU_Automation1.Models;

namespace PSTU_Automation1.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly ILogger<AccountController> _logger;

        //private readonly EmailHelper _emailHelper;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<AccountController> logger, IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            //_emailHelper = new EmailHelper(configuration);
            _context = context;
        }

        //[HttpGet]
        //public async Task<IActionResult> AddPassword()
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    var userHasPassword = await _userManager.HasPasswordAsync(user);

        //    if (userHasPassword)
        //    {
        //        return RedirectToAction("ChangePassword");
        //    }

        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddPassword(AddPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);

        //        var result = await _userManager.AddPasswordAsync(user, model.NewPassword);

        //        if (!result.Succeeded)
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }

        //            return View();
        //        }

        //        await _signInManager.RefreshSignInAsync(user);

        //        return View("AddPasswordConfirmation");
        //    }

        //    return View(model);
        //}

        //[HttpGet]
        //public async Task<IActionResult> ChangePassword()
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    var userHasPassword = await _userManager.HasPasswordAsync(user);

        //    if (!userHasPassword)
        //    {
        //        return RedirectToAction("AddPassword");
        //    }

        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        if (user == null)
        //        {
        //            return RedirectToAction("Login");
        //        }

        //        var result = await _userManager.ChangePasswordAsync(user,
        //            model.CurrentPassword, model.NewPassword);
        //        if (!result.Succeeded)
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }

        //            return View();
        //        }

        //        await _signInManager.RefreshSignInAsync(user);
        //        return View("ChangePasswordConfirmation");
        //    }

        //    return View(model);
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    if (token == null || email == null)
        //    {
        //        ModelState.AddModelError("", "Invalid password reset token");
        //    }

        //    return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);

        //        if (user != null)
        //        {
        //            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        //            if (result.Succeeded)
        //            {
        //                if (await _userManager.IsLockedOutAsync(user))
        //                {
        //                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
        //                }

        //                return View("ResetPasswordConfirmation");
        //            }

        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }

        //            return View(model);
        //        }

        //        return View("ResetPasswordConfirmation");
        //    }

        //    return View(model);
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);

        //        if (user != null && await _userManager.IsEmailConfirmedAsync(user))
        //        {
        //            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        //            var passwordResetLink = Url.Action("ResetPassword", "Account",
        //                new {email = model.Email, token = token}, Request.Scheme);

        //            _logger.Log(LogLevel.Warning, passwordResetLink);

        //            return View("ForgotPasswordConfirmation");
        //        }

        //        return View("ForgotPasswordConfirmation");
        //    }

        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Administration");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(AccountType? accountType)
        {
            if (accountType == null)
            {
                accountType = AccountType.Officer;
            }


            var model = new RegisterViewModel
            {
                AccountType = (AccountType)accountType

            };

            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email.Trim().Normalize());

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email is invalid or already taken.");
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUserNameInUse(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Username is not available");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName.Trim(),
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    switch (model.AccountType)
                    {
                        case AccountType.UndergraduateStudent:
                            await _userManager.AddToRoleAsync(user, "Student");
                            break;
                        case AccountType.PostgraduateStudent:
                            await _userManager.AddToRoleAsync(user, "Student");
                            break;
                        case AccountType.Teacher:
                            await _userManager.AddToRoleAsync(user, "Teacher");
                            break;
                        default: break;
                    }

                    if (_signInManager.IsSignedIn(User) && (User.IsInRole("Admin") || User.IsInRole("Super Admin")))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }

                    ViewBag.ErrorTitle = "Registration successful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                                           "username, by clicking on the confirmation link we have emailed you";
                    return View("Login", new LoginViewModel
                    {
                        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                    });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    if (userId == null || token == null)
        //    {
        //        return RedirectToAction("index", "home");
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //    {
        //        ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
        //        return View("NotFound");
        //    }

        //    var result = await _userManager.ConfirmEmailAsync(user, token);

        //    if (result.Succeeded)
        //    {
        //        return View();
        //    }

        //    ViewBag.ErrorTitle = "Email cannot be confirmed";
        //    return View("Error");
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email) ??
                           await _userManager.FindByNameAsync(model.Email);
                //if (user != null && !user.PhoneNumberConfirmed &&
                //    await _userManager.CheckPasswordAsync(user, model.Password))
                //{
                //    ModelState.AddModelError(string.Empty, "Phone number is not verified");
                //    return View(model);
                //}

                if (user != null && !await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    ModelState.AddModelError(string.Empty, "Invalid Username/Password");
                    return View(model);
                }

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Username/Password");
                    return View(model);
                }

                //if (user != null && (await _userManager.IsInRoleAsync(user, "Admin")))
                //{

                //}

                var result = await _signInManager.PasswordSignInAsync(await _userManager.GetUserNameAsync(user),
                    model.Password,
                    model.RememberMe, true);

                if (result.Succeeded)
                {
                    //if (user != null)
                    //{
                    //    var log = new LoginHistoryLog
                    //    {
                    //        LoginTime = DateTime.UtcNow,
                    //        UserId = user.Id
                    //    };
                    //     await _context.LoginHistoryLogs.AddAsync(log);
                    //     await _context.SaveChangesAsync();
                    //}



                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
        //        new {ReturnUrl = returnUrl});

        //    var properties =
        //        _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        //    return new ChallengeResult(provider, properties);
        //}

        //[AllowAnonymous]
        //public async Task<IActionResult>
        //    ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    LoginViewModel loginViewModel = new LoginViewModel
        //    {
        //        ReturnUrl = returnUrl,
        //        ExternalLogins =
        //            (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        //    };

        //    if (remoteError != null)
        //    {
        //        ModelState.AddModelError(string.Empty,
        //            $"Error from external provider: {remoteError}");

        //        return View("Login", loginViewModel);
        //    }

        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        ModelState.AddModelError(string.Empty,
        //            "Error loading external login information.");

        //        return View("Login", loginViewModel);
        //    }

        //    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //    IdentityUser user = null;

        //    if (email != null)
        //    {
        //        user = await _userManager.FindByEmailAsync(email);

        //        if (user != null && !user.EmailConfirmed)
        //        {
        //            ModelState.AddModelError(string.Empty, "Email not confirmed yet");
        //            return View("Login", loginViewModel);
        //        }
        //    }

        //    var signInResult = await _signInManager.ExternalLoginSignInAsync(
        //        info.LoginProvider, info.ProviderKey,
        //        isPersistent: false, bypassTwoFactor: true);

        //    if (signInResult.Succeeded)
        //    {
        //        return LocalRedirect(returnUrl);
        //    }
        //    else
        //    {
        //        if (email != null)
        //        {
        //            if (user == null)
        //            {
        //                user = new IdentityUser
        //                {
        //                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
        //                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)
        //                };

        //                await _userManager.CreateAsync(user);

        //                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //                var confirmationLink = Url.Action("ConfirmEmail", "Account",
        //                    new {userId = user.Id, token = token}, Request.Scheme);

        //                _logger.Log(LogLevel.Warning, confirmationLink);

        //                ViewBag.ErrorTitle = "Registration successful";
        //                ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
        //                                       "username, by clicking on the confirmation link we have emailed you";
        //                return View("Error");
        //            }

        //            await _userManager.AddLoginAsync(user, info);
        //            await _signInManager.SignInAsync(user, isPersistent: false);

        //            return LocalRedirect(returnUrl);
        //        }

        //        ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
        //        ViewBag.ErrorMessage = "Please contact support on admin@cse.pstu.ac.bd";

        //        return View("Error");
        //    }
        //}



        [AllowAnonymous]
        public IActionResult Recover()
        {
            return View();
        }

        private static string GeneratePassword()
        {
            var pass = "";
            var rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                pass += (char)(rand.Next(25) + 65);
                ;
            }

            return pass;
        }


    }
}