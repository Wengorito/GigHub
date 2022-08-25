using GigHub.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FollowingsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        public ActionResult Index()
        {
            IEnumerable<ApplicationUser> artists = _unitOfWork.Followings.GetFollowedArtists(User.Identity.GetUserId());

            return View(artists);
        }
    }
}