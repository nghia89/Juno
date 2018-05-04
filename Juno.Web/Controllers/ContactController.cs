using AutoMapper;
using Juno.Model.Models;
using Juno.server;
using Juno.Web.Models;
using System.Web.Mvc;

namespace Juno.Web.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        private IContactDetailService _contactDetailService;

        public ContactController(IContactDetailService contactDetailService)
        {
            this._contactDetailService = contactDetailService;
        }

        public ActionResult Index()
        {
            var model = _contactDetailService.GetDefaultContact();
            var viewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return View(viewModel);
        }
    }
}