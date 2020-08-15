using System.Threading.Tasks;
using System.Web.Mvc;
using Clinic.Core.Models.Account;
using Clinic.FrameWork.Filters;
using Clinic.Service.Services.ApplicationSignIn;
using Clinic.Service.Services.Users;
using Clinic.Service.Validations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Clinic.Web.Controllers
{
    /// <summary>
    /// </summary>
    public partial class AccountController : BaseController
    {

        #region Private Fields

        private readonly IAccountValidation _accountValidation;
        private readonly IApplicationSignInManager _applicationSignInManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IUserService _userService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// </summary>
        /// <param name="userManager">             </param>
        /// <param name="applicationSignInManager"></param>
        /// <param name="authenticationManager">   </param>
        /// <param name="accountValidation"></param>
        public AccountController(IApplicationSignInManager applicationSignInManager, IAuthenticationManager authenticationManager, IUserService userManager, IAccountValidation accountValidation)
        {
            _userService = userManager;
            _applicationSignInManager = applicationSignInManager;
            _authenticationManager = authenticationManager;
            _accountValidation = accountValidation;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// نمایش فرم ورود
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public virtual async Task<ActionResult> Login(string returnUrl)
        {
            // Result
            ViewBag.ReturnUrl = returnUrl;
            return View(MVC.Account.Views.Login);
        }

        /// <summary>
        /// ورود به سیستم
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public virtual async Task<ActionResult> SignIn(string returnUrl, LoginViewModel viewModel)
        {
            //  Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            // Validation
            if (ModelState.IsValid == false)
                return View(MVC.Account.Views.Login, viewModel);

            var validate = await _accountValidation.SignInValidationAsync(viewModel);
            if (validate != null)
            {
                ModelState.AddModelError("", validate);
                return View(MVC.Account.Views.Login, viewModel);
            }

            // Result
            //var r = _authenticationManager.SignIn()
            var result = await _applicationSignInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, true);
            switch (result)
            {
                case SignInStatus.Success:
                    return Url.IsLocalUrl(returnUrl) ? RedirectToAction(returnUrl) : RedirectToAction(MVC.Home.Dashboard());

                case SignInStatus.LockedOut:
                    ModelState.AddModelError("UserName", $"دقیقه دوباره امتحان کنید {_userService.DefaultAccountLockoutTimeSpan} حساب شما قفل شد ! لطفا بعد از ");
                    return View(MVC.Account.Views.Login, viewModel);

                case SignInStatus.Failure:
                    ModelState.AddModelError("UserName", "نام کاربری یا کلمه عبور  صحیح نمی باشد");
                    return View(MVC.Account.Views.Login, viewModel);

                case SignInStatus.RequiresVerification:
                    //await _userService.SendEmailConfirmationTokenAsync(user.Id);
                    ModelState.AddModelError("UserName", "می بایست ابتدا حساب کاربری خود را تایید نمایید");
                    return View(MVC.Account.Views.Login, viewModel);

                default:
                    ModelState.AddModelError("UserName", "در این لحظه امکان ورود به  سابت وجود ندارد . مراتب را با مسئولان سایت در میان بگذارید");
                    return View(MVC.Account.Views.Login, viewModel);
            }
        }

        /// <summary>
        /// خروج از سیستم
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        public virtual async Task<ActionResult> SignOut()
        {
            // Result
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction(MVC.Home.Dashboard());
        }

        #endregion Public Methods

    }
}