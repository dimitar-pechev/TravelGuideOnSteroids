using System;
using System.Linq;
using System.Web.Mvc;
using TravelGuide.Services.Articles.Contracts;

namespace TravelGuide.Controllers
{
    public class ArticlesController : Controller
    {
        private const int PageSize = 1;
        private readonly IArticleService service;

        public ArticlesController(IArticleService service)
        {
            this.service = service;
        }

        public ActionResult Index(int? page)
        {
            var articles = this.service.GetAllArticles();

            var pagesCount = Math.Ceiling((decimal)articles.Count() / PageSize);
            this.ViewBag.PagesCount = pagesCount;

            if (page == null || page < 1 || page > pagesCount)
            {
                page = 1;
            }

            this.ViewBag.CurrentPage = page;
            articles = articles.Skip(((int)page - 1) * PageSize).Take(PageSize).ToList();

            return this.View(articles);
        }
    }
}