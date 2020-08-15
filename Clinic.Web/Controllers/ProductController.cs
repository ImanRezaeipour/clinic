using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Clinic.Core.Models.Product;
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
    public partial class ProductController : BaseController
    {

        #region Private Fields

        private readonly IProductFactory _productFactory;
        private readonly IProductService _productService;

        #endregion Private Fields

        #region Public Constructors

        public ProductController(IProductService productService, IProductFactory productFactory)
        {
            _productService = productService;
            _productFactory = productFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [MvcAuthorize]
        public virtual async Task<ActionResult> Create(ProductCreateViewModel viewModel)
        {
            // Check
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            // Validation
            if (ModelState.IsValid == false)
                return View(MVC.Product.Views.Create);

            // Result
             await _productService.CreateByViewModelAsync(viewModel);
          
                this.AddToastMessage("افزودن محصول با موفقیت انجام شد", "", ToastType.Success);
                return RedirectToAction(MVC.Product.List());
          }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "افزودن محصول", Key = "Panel_Product_New", ParentKey = "Panel_Product_List")]
        public virtual async Task<ActionResult> Create()
        {
            return View(MVC.Product.Views.Create);
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

             await _productService.DeleteByIdAsync(id.Value);
            return Json(AjaxResult.Succeeded() ,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "ویرایش محصول", Key = "Panel_Product_Edit", ParentKey = "Panel_Product_List", PreservedRouteParameters = "id")]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            // Check
            if (id == null)
                return View(MVC.Error.Views.BadRequest);

            // Result
            var viewModel = await _productFactory.PrepareEditViewModelAsync(id.Value);
            return viewModel == null ? View(MVC.Error.Views.InternalServerError) : View(MVC.Product.Views.Edit, viewModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [MvcAuthorize]
        [HttpPost]
        public virtual async Task<ActionResult> Edit(ProductEditViewModel viewModel)
        {
            if (viewModel == null)
                return View(MVC.Error.Views.BadRequest);

            if (ModelState.IsValid == false)
                return RedirectToAction(MVC.Product.Edit(viewModel.Id));

            await _productService.EditByViewModelAsync(viewModel);
            this.AddToastMessage("ویرایش محصول با موفقیت انجام شد", "", ToastType.Success);
            return RedirectToAction(MVC.Product.List());

        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        [MvcAuthorize]
        public virtual async Task<JsonResult> GetListAjax()
        {
            var request = JsonConvert.DeserializeObject<KendoDataSourceRequest>(Request.Url.ParseQueryString().GetKey(0));
            var list = await _productService.ListByRequestAsync(request);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [MvcAuthorize]
        [MvcSiteMapNode(Title = "لیست محصولات", Key = "Panel_Product_List", ParentKey = "Panel_Home_Dashboard")]
        public virtual async Task<ActionResult> List()
        {
            return View(MVC.Product.Views.List);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<JsonResult> Tree()
        {
            var result = await _productService.TreeAsync();
            return Json(AjaxResult.Succeeded(result), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public virtual async Task<JsonResult> GetSelectListAjax()
        {
            var products = await _productService.GetAsSelectItemListAsync();
            return Json(AjaxResult.Succeeded(products), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public virtual async Task<JsonResult> GetPrice(Guid? id)
        {
            if (id == null)
                return Json(AjaxResult.Failed(AjaxErrorStatus.BadRequest), JsonRequestBehavior.AllowGet);

            var price = await _productService.GetPriceByIdAsync(id.Value);
            return Json(AjaxResult.Succeeded(price), JsonRequestBehavior.AllowGet);
        }

        #endregion Public Methods
    }
}