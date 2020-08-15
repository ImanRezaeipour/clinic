using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Clinic.Core.Models.User;
using Clinic.FrameWork.Extensions;
using Clinic.FrameWork.Filters;
using Clinic.FrameWork.Results;
using Clinic.FrameWork.Toast;
using Clinic.Service.Services.ApplicationSignIn;
using Clinic.Service.Services.Users;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace Clinic.Web.Controllers
{
    /// <summary>
    /// </summary>
    public partial class UserController : BaseController
    {

        #region Private Fields

        private readonly IApplicationSignInManager _signInManagerService;
        private readonly IUserService _userService;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// </summary>
        /// <param name="userService">         </param>
        /// <param name="signInManagerService"></param>
        public UserController(IUserService userService, IApplicationSignInManager signInManagerService)
        {
            _userService = userService;
            _signInManagerService = signInManagerService;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult ChangePassword()
        {
            // Result
            //  var viewModel =  _userService.ChangePasswordAsync();
            //  Result
            return View(MVC.User.Views.ChangePassword);
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ChangePassword(UserChangePasswordViewModel viewModel)
        {
            //  Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            //  Validation
            if (!ModelState.IsValid)
                return View(MVC.User.Views.ChangePassword, viewModel);

            //  Result
            var result = await _userService.ChangePasswordAsync(Guid.Parse(User.Identity.GetUserId()), viewModel.OldPassword, viewModel.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userService.FindByIdAsync(Guid.Parse(User.Identity.GetUserId()));
                    await _signInManagerService.SignInAsync(user, false, false);
                return RedirectToAction(MVC.Account.SignOut());
            }
            return View(MVC.User.Views.ChangePassword, viewModel);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AjaxOnly]
        public virtual async Task<JsonResult> DeleteAjax(Guid? id)
        {
            //  Check
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            //  Result
             await _userService.DeleteByIdAsync(id.Value);
            return Json( AjaxResult.Succeeded(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            //  Check
            if (id == null)
                return View(MVC.Error.Views.BadRequest);

            //  Result
            var viewModel = await _userService.GetForEditAsync(id.Value);
            return viewModel != null ? View(MVC.User.Views.Edit, viewModel) : View(MVC.Error.Views.InternalServerError);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        public virtual async Task<JsonResult> GetListAjax()
        {
            //  Result
            var request = JsonConvert.DeserializeObject<Clinic.Core.Utilities.Kendo.KendoDataSourceRequest>(Request.Url.ParseQueryString().GetKey(0));
            //var resuklt = await _userService.ListByRequestAsync(request);
            return Json(request, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ActionResult> List()
        {
            //  Result
            return View(MVC.User.Views.List);
        }

        /// <summary>
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(UserEditViewModel viewModel)
        {
            //  Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            //  Validation
            if (ModelState.IsValid == false)
                return View(MVC.User.Views.Edit);

            //  Result
             await _userService.UpdateByViewModelAsync(viewModel);
           
                this.AddToastMessage("","عملیات با موفقیت انجام شد",ToastType.Success);
                return RedirectToAction(MVC.User.List());
            
        }

        #endregion Public Methods

    }
}