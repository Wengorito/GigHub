﻿using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        void Add(Following following);
        void Remove(Following following);
        Following GetFollowing(string artistId, string userId);

        // ApplicationUserRepository?
    }
}