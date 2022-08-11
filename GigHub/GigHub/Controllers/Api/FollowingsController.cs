using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto)
        {
            var userId = User.Identity.GetUserId();

            var exists = _context.Followings.Any(f => f.ArtistId == dto.ArtistId && f.FollowerId == userId);

            if (exists)
                return BadRequest("Artist already followed.");

            var following = new Following
            {
                ArtistId = dto.ArtistId,
                FollowerId = userId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
