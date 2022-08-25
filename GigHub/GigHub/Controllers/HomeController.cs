using GigHub.Persistence;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.QueryUpcomingGigs();

            if (!String.IsNullOrEmpty(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.Contains(query) ||
                        g.Genre.Name.Contains(query) ||
                        g.Venue.Contains(query));
            }

            var attendances = _unitOfWork.Attendances.GetFutureAttendances(User.Identity.GetUserId()).ToLookup(a => a.GigId);

            var viewModel = new HomeViewModel
            {
                ShowActions = User.Identity.IsAuthenticated,
                UpcomingGigs = upcomingGigs,
                SearchTerm = query,
                Attendances = attendances
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Search(HomeViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}