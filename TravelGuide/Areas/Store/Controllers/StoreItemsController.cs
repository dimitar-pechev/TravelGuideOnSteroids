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
        private readonly IStoreService storeService;
        private readonly IMappingService mappingService;

        public StoreItemsController(IStoreService storeService, IMappingService mappingService)
        {
            this.storeService = storeService;
            this.mappingService = mappingService;
        }

        [AllowAnonymous]
        public ActionResult Index(string query, int? page)
        {
            var pagesCount = this.storeService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var images = this.storeService.GetFilteredItemsByPage(query, currentPage, AppConstants.StorePageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<StoreItemViewModel>>(images);
            var model = this.mappingService.Map<StoreListViewModel>(mappedImages);

            model = this.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.StoreListBaseUrl);

            return this.View(model);
        }

        [AllowAnonymous]
        public ActionResult Search(string query, int? page)
        {
            var pagesCount = this.storeService.GetPagesCount(query);
            var currentPage = this.GetPage(page, pagesCount);
            var images = this.storeService.GetFilteredItemsByPage(query, currentPage, AppConstants.StorePageSize);
            var mappedImages = this.mappingService.Map<IEnumerable<StoreItemViewModel>>(images);
            var model = this.mappingService.Map<StoreListViewModel>(mappedImages);

            model = this.AssignViewParams(model, query, currentPage, pagesCount, AppConstants.StoreListBaseUrl);

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
        public ActionResult ChangeStatus(Guid? id, string isInStock)
        {
            var model = this.storeService.GetStoreItemById((Guid)id);

            this.storeService.ChangeStatus(model.Id, isInStock);

            return this.PartialView("_AddToCartPartial", model);
        }

        private int GetPage(int? page, int pagesCount)
        {
            int result;
            if (page == null || page < 1 || page > pagesCount)
            {
                result = 1;
            }
            else
            {
                result = (int)page;
            }

            return result;
        }

        private StoreListViewModel AssignViewParams(StoreListViewModel model, string query, int currentPage, int pagesCount, string baseUrl)
        {
            model.Query = query;
            model.CurrentPage = currentPage;
            model.PreviousPage = currentPage - 1;
            model.NextPage = currentPage + 1;
            model.PagesCount = pagesCount;
            model.BaseUrl = baseUrl;

            return model;
        }
    }
}