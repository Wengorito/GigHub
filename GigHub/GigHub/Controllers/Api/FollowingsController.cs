﻿using GigHub.Dtos;
using GigHub.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (dto == null)
                return BadRequest();

            if (_unitOfWork.Followings.GetFollowing(dto.ArtistId, userId) != null)
                return BadRequest("Artist already followed.");

            var following = new Following
            {
                ArtistId = dto.ArtistId,
                FollowerId = userId
            };

            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var following = _unitOfWork.Followings.GetFollowing(id, User.Identity.GetUserId());

            if (following == null)
                return NotFound();

            _unitOfWork.Followings.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
