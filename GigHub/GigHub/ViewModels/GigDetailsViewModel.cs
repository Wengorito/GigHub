using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.ViewModels
{
    public class GigDetailsViewModel
    {
        public Gig Gig { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsLogged { get; set; }
    }
}