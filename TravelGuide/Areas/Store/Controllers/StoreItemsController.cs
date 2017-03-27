using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TravelGuide.Areas.Store.ViewModels;
using TravelGuide.Common;
using TravelGuide.Common.Contracts;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Areas.Store.Controllers
{
    [Authorize(Roles = "admin")]
    public class StoreItemsController : Controller
    {
        protected readonly IStoreService storeService;
        protected readonly IMappingService mappingService;
        protected readonly IUtilitiesService utils;

        public StoreItemsController(IStoreService storeService, IMappingService mappingService, IUtilitiesService utils)
        {
            if (storeService == null)
            {
                throw new ArgumentNullException();
            }

            if (mappingService == null)
            {
                throw new ArgumentNullException();
            }

            if (utils == null)
            {
                throw new ArgumentNullException();
            }

            this.storeService = storeService;
            this.mappingService = mappingService;
            this.utils = utils;
        }

        [AllowAnonymous]
        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.storeService.GetPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var images = this.storeService.GetFilteredItemsByPage(query, currentPage, AppConstants.StorePageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<StoreItemViewModel>>(images);
            var model = this.mappingService.Map<StoreListViewModel>(mappedImages);
            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.StoreListBaseUrl);

            return this.View(model);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string query, int? page)
        {
            var pagesCount = this.storeService.GetPagesCount(query);
            var currentPage = this.utils.GetPage(page, pagesCount);
            var images = this.storeService.GetFilteredItemsByPage(query, currentPage, AppConstants.StorePageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<StoreItemViewModel>>(images);
            var model = this.mappingService.Map<StoreListViewModel>(mappedImages);
            model = this.utils.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.StoreListBaseUrl);

            return this.PartialView("_StoreItemsListPartial", model);
        }

        [AllowAnonymous]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index");
            }

            var item = this.storeService.GetStoreItemById((Guid)id);

            if (item == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(item);
        }

        public ActionResult Edit(Guid? id)
        {
            var item = this.storeService.GetStoreItemById((Guid)id);
            var model = this.mappingService.Map<CreateEditItemViewModel>(item);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateEditItemViewModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(item);
            }

            this.storeService.EditItem(item.Id, item.ItemName, item.Description, item.DestinationFor, item.ImageUrl, item.Brand, item.Price.ToString());

            return this.RedirectToAction("Details", new { id = item.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid? id)
        {
            this.storeService.DeleteItem((Guid)id);

            return this.RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEditItemViewModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(item);
            }

            this.storeService.AddNewItem(item.ItemName, item.Description, item.DestinationFor, item.ImageUrl, item.Brand, item.Price.ToString());

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(Guid? id, string isInStock)
        {
            var model = this.storeService.GetStoreItemById((Guid)id);

            this.storeService.ChangeStatus(model.Id, isInStock);

            return this.PartialView("_AddToCartPartial", model);
        }
    }
}