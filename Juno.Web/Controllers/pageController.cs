using AutoMapper;
using Juno.Model.Models;
using Juno.server;
using Juno.Web.Models;
using System.Web.Mvc;

namespace Juno.Web.Controllers
{
    public class PageController : Controller
    {
        // GET: page
        private IPageService _pageService;

        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }

        public ActionResult Index(string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var model = Mapper.Map<Page, PageViewModel>(page);
            return View(model);
        }
    }
}