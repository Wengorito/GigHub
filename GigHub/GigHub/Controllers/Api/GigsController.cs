using GigHub.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigByArtistWithAttendees(User.Identity.GetUserId(), id);

            if (gig == null)
                return NotFound();

            if (gig.IsCanceled)
                return NotFound();

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
