using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Clinic.Core.Models.ProductSeller;
using Clinic.FrameWork.Extensions;
using Clinic.FrameWork.Filters;
using Clinic.FrameWork.Results;
using Clinic.FrameWork.Toast;
using Clinic.Service.Factories.Products;
using Clinic.Service.Services.Products;
using MvcSiteMapProvider;
using Newtonsoft.Json;

namespace Clinic.Web.Controllers
{
    public partial class ProductSellerController : BaseController
    {
        #region Private Fields

        private readonly IProductSellerService _productSellerService;
        private readonly IProductSellerFactory _productSellerFactory;

        #endregion

        #region Public Constractor

        public ProductSellerController(IProductSellerService productSellerService, IProductSellerFactory productSellerFactory)
        {
            _productSellerService = productSellerService;
            _productSellerFactory = productSellerFactory;
        }


        #endregion

        #region Public Method

        [HttpPost]
        [MvcAuthorize]

        public virtual async Task<ActionResult> Create(ProductSellerCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            // Validation
            if (ModelState.IsValid == false)
                return View(MVC.ProductSeller.Views.List);

            // Process
           await _productSellerService.CreateByViewModelAsync(viewModel);
           
                this.AddToastMessage("افزودن فروشنده با موفقیت انجام شد", "", ToastType.Success);
                return RedirectToAction(MVC.ProductSeller.List());
          
            // Result
        }


        [MvcAuthorize]
        [MvcSiteMapNode(Title = "ویرایش فروشنده", Key = "Panel_ProductSeller_Edit", ParentKey = "Panel_ProductSeller_List", PreservedRouteParameters = "id")]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            // Check
            if (id == null)
                return View(MVC.Error.Views.BadRequest);

            // Result
            var viewModel = await _productSellerFactory.PrepareEditViewModelAsync(id.Value);
            return viewModel == null ? View(MVC.Error.Views.InternalServerError) : View(MVC.ProductSeller.Views.Edit, viewModel);
        }

        [AjaxOnly]
        [MvcAuthorize]
        public virtual async Task<JsonResult> GetListAjax()
        {
            // Result
            var request = JsonConvert.DeserializeObject<Clinic.Core.Utilities.Kendo.KendoDataSourceRequest>(Request.Url.ParseQueryString().GetKey(0));
            var list = await _productSellerService.ListByRequestAsync(request);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public virtual async Task<JsonResult> GetSelectListAjax()
        {
            var doctors = await _productSellerService.GetProductSellerAsSelectItemList();
            return Json(AjaxResult.Succeeded(doctors), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "لیست فروشنده ها", Key = "Panel_ProductSeller_List", ParentKey = "Panel_Home_Dashboard")]
        public virtual async Task<ActionResult> List()
        {
            //  Result
            return View(MVC.ProductSeller.Views.List);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "افزودن فروشنده", Key = "Panel_ProductSeller_New", ParentKey = "Panel_ProductSeller_List")]
        public virtual async Task<ActionResult> Create()
        {
            //  Result
            // var viewModel = await _productSellerService.GetForNewAsync();
            return View(MVC.ProductSeller.Views.Create);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [MvcAuthorize]
        public virtual async Task<ActionResult> Edit(ProductSellerEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            // Validation
            if (ModelState.IsValid == false)
                return View(MVC.ProductSeller.Views.Create);

            // Result
           await _productSellerService.EditByViewModelAsync(viewModel);
            
                this.AddToastMessage("ویرایش فروشنده با موفقیت انجام شد", "", ToastType.Success);
                return RedirectToAction(MVC.ProductSeller.List());
           
        }

        public virtual async Task<JsonResult> DeleteAjax(Guid? id)
        {
            if (id == null)
                return Json(AjaxResult.Failed(AjaxErrorStatus.BadRequest), JsonRequestBehavior.AllowGet);

             await _productSellerService.DeleteByIdAsync(id.Value);
            return Json( AjaxResult.Succeeded() ,JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}