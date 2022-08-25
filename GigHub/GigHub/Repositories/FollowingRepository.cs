using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }

        public Following GetFollowing(string artistId, string userId)
        {
            return _context.Followings.SingleOrDefault(f => f.ArtistId == artistId && f.FollowerId == userId);
        }

        public IEnumerable<ApplicationUser> GetFollowedArtists(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Artist)
                .ToList();
        }
    }
}