using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Clinic.Core.Models.ProductBuy;
using Clinic.Core.Utilities.Kendo;
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
    public partial class ProductBuyController : BaseController
    {
        #region Private Fields

        private readonly IProductBuyService _productBuyService;
        private readonly IProductBuyFactory _productBuyFactory;

        #endregion

        #region Public Constractor

        public ProductBuyController(IProductBuyService productBuyService, IProductBuyFactory productBuyFactory)
        {
            _productBuyService = productBuyService;
            _productBuyFactory = productBuyFactory;
        }


        #endregion

        #region Public Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAuthorize]
        public virtual async Task<ActionResult> Create(ProductBuyCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            // Validation
            if (ModelState.IsValid == false)
                return View(MVC.ProductBuy.Views.List);

            // Process
            await _productBuyService.CreateByViewModelAsync(viewModel);
              this.AddToastMessage("افزودن فروشنده با موفقیت انجام شد", "", ToastType.Success);
                return RedirectToAction(MVC.ProductBuy.List());
           
            // Result
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "ویرایش محصول فروشنده", Key = "Panel_ProductBuy_Edit", ParentKey = "Panel_ProductBuy_List", PreservedRouteParameters = "id")]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            // Check
            if (id == null)
                return View(MVC.Error.Views.BadRequest);

            // Result
            var viewModel = await _productBuyFactory.PrepareEditViewModelAsync(id.Value);
            return viewModel == null ? View(MVC.Error.Views.InternalServerError) : View(MVC.ProductBuy.Views.Edit, viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        [MvcAuthorize]
        public virtual async Task<JsonResult> GetListAjax()
        {
            // Result
            var request = JsonConvert.DeserializeObject<KendoDataSourceRequest>(Request.Url.ParseQueryString().GetKey(0));
            var list = await _productBuyService.ListByRequestAsync(request);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "لیست  محصول فروشنده ها", Key = "Panel_ProductBuy_List", ParentKey = "Panel_Home_Dashboard")]
        public virtual async Task<ActionResult> List()
        {
            //  Result
            return View(MVC.ProductBuy.Views.List);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "افزودن محصول فروشنده", Key = "Panel_ProductBuy_New", ParentKey = "Panel_ProductBuy_List")]
        public virtual async Task<ActionResult> Create()
        {
            //  Result
            // var viewModel = await _productBuyFactory.PrepareCreateViewModel();
            return View(MVC.ProductBuy.Views.Create);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [MvcAuthorize]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ProductBuyEditViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            // Validation
            if (ModelState.IsValid == false)
                return View(MVC.ProductBuy.Views.Create);

            // Result
            await _productBuyService.EditByViewModelAsync(viewModel);
               this.AddToastMessage("ویرایش فروشنده با موفقیت انجام شد", "", ToastType.Success);
                return RedirectToAction(MVC.ProductBuy.List());
           
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        [MvcAuthorize]
        public virtual async Task<JsonResult> DeleteAjax(Guid? id)
        {
            if (id == null)
                return Json(AjaxResult.Failed(AjaxErrorStatus.BadRequest), JsonRequestBehavior.AllowGet);

             await _productBuyService.DeleteByIdAsync(id.Value);
            return Json( AjaxResult.Succeeded(),JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}