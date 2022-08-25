using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IFollowingRepository
    {
        void Add(Following following);
        void Remove(Following following);
        Following GetFollowing(string artistId, string userId);

        // ApplicationUserRepository?
        IEnumerable<ApplicationUser> GetFollowedArtists(string userId);
    }
}