using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Clinic.Core.Models.Role;
using Clinic.FrameWork.Extensions;
using Clinic.FrameWork.Filters;
using Clinic.FrameWork.Results;
using Clinic.FrameWork.Toast;
using Clinic.Service.Factories.Users;
using Clinic.Service.Services.Permissions;
using Clinic.Service.Services.Roles;
using Clinic.Service.Services.Users;
using Clinic.Service.Validations;
using MvcSiteMapProvider;
using Newtonsoft.Json;

namespace Clinic.Web.Controllers
{
    /// <summary>
    /// </summary>
    public partial class RoleController : BaseController
    {

        #region Private Fields

        private readonly IRoleFactory _roleFactory;
        private readonly IRoleService _roleService;

        #endregion Private Fields

        #region Public Constructors

        public RoleController(IRoleService roleService, IRoleFactory roleFactory)
        {
            _roleService = roleService;
            _roleFactory = roleFactory;
        }

        #endregion Public Constructors

        #region Public Methods


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(RoleCreateViewModel viewModel)
        {
            await _roleService.CreateByViewModelAsync(viewModel);
            return RedirectToAction(MVC.Role.Create());
        }


        [MvcSiteMapNode(ParentKey = "Panel_Home_Index", Title = "نقش ها", Key = "Panel_Role_New")]
        public virtual async Task<ActionResult> Create()
        {
            return View(MVC.Role.Views.Create);
        }


        public virtual async Task<JsonResult> DeleteAjax(Guid? id)
        {
            if (id == null)
                return Json(AjaxResult.Failed(AjaxErrorStatus.BadRequest), JsonRequestBehavior.AllowGet);

            await _roleService.DeleteByIdAsync(id.Value);
            return Json(AjaxResult.Succeeded(), JsonRequestBehavior.AllowGet);
        }


        [MvcSiteMapNode(ParentKey = "Panel_Role_ItemList", Title = "ویرایش", Key = "Panel_Role_Edit", PreservedRouteParameters = "id")]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
                return View(MVC.Error.Views.BadRequest);

            var viewModel = await _roleFactory.PrepareEditViewModelAsync(id.Value);
            return View(MVC.Role.Views.Edit, viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(RoleEditViewModel viewModel)
        {
            await _roleService.EditByViewModelAsync(viewModel);
            return RedirectToAction(MVC.Role.List());
        }


        [MvcSiteMapNode(ParentKey = "Panel_Home_Index", Title = "نقش ها", Key = "Panel_Role_ItemList")]
        public virtual async Task<ActionResult> List(RoleSearchRequest request)
        {
            var viewModel = await _roleFactory.PrepareListViewModelAsync(request);
            return View(MVC.Role.Views.List, viewModel);
        }

        #endregion Public Methods

    }
}