using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TravelGuide.Areas.Store.ViewModels;
using TravelGuide.Common.Contracts;
using TravelGuide.Models.Store;
using TravelGuide.Services.Store.Contracts;

namespace TravelGuide.Areas.Store.Controllers
{
    public class StoreItemsController : Controller
    {
        private const int PageSize = 6;
        private readonly IStoreService storeService;
        private readonly IMappingService mappingService;

        public StoreItemsController(IStoreService storeService, IMappingService mappingService)
        {
            this.storeService = storeService;
            this.mappingService = mappingService;
        }

        public ActionResult Index(string query, int? page)
        {
            IEnumerable<StoreItem> storeItems = new List<StoreItem>();

            if (!string.IsNullOrEmpty(query))
            {
                storeItems = this.storeService.GetItemsByKeyword(query);
                this.ViewBag.Query = query;
            }
            else
            {
                storeItems = this.storeService.GetAllNotDeletedStoreItemsOrderedByDate();
            }

            var pagesCount = Math.Ceiling((decimal)storeItems.Count() / PageSize);
            this.ViewBag.PagesCount = pagesCount;

            page = this.GetPage(page, pagesCount);

            this.ViewBag.CurrentPage = page;
            storeItems = storeItems.Skip(((int)page - 1) * PageSize).Take(PageSize).ToList();

            ICollection<IndexViewModel> itemsVieModels = new List<IndexViewModel>();

            foreach (var item in storeItems)
            {
                var indexViewModel = this.mappingService.Map<IndexViewModel>(item);
                itemsVieModels.Add(indexViewModel);
            }

            return this.View(itemsVieModels);
        }

        public ActionResult Search(string query, int? page)
        {
            IEnumerable<StoreItem> storeItems = new List<StoreItem>();

            if (!string.IsNullOrEmpty(query))
            {
                storeItems = this.storeService.GetItemsByKeyword(query);
                this.ViewBag.Query = query;
            }
            else
            {
                storeItems = this.storeService.GetAllNotDeletedStoreItemsOrderedByDate();
            }

            var pagesCount = Math.Ceiling((decimal)storeItems.Count() / PageSize);
            this.ViewBag.PagesCount = pagesCount;

            page = this.GetPage(page, pagesCount);

            this.ViewBag.CurrentPage = page;
            storeItems = storeItems.Skip(((int)page - 1) * PageSize).Take(PageSize).ToList();

            ICollection<IndexViewModel> itemsVieModels = new List<IndexViewModel>();

            foreach (var item in storeItems)
            {
                var indexViewModel = this.mappingService.Map<IndexViewModel>(item);
                itemsVieModels.Add(indexViewModel);
            }

            return this.PartialView("_StoreItemsListPartial", itemsVieModels);
        }

        protected int GetPage(int? page, decimal pagesCount)
        {
            if (page == null || page < 1 || page > pagesCount)
            {
                page = 1;
            }

            return (int)page;
        }
    }
}