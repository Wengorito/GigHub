using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        Gig GetGig(int id);
        Gig GetGigWithAttendances(int id);
        Gig GetGigByArtistWithAttendees(string artistId, int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId);
        IQueryable<Gig> QueryUpcomingGigs();
    }
}