var FollowingService = function () {
    var createFollowing = function (artistId, done, fail) {
        $.post("/api/followings", { artistId: artistId })
            .done(done)
            .fail(fail);
    };

    var deleteFollowing = function (artistId, done, fail) {
        $.ajax({
            url: "/api/followings/" + artistId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        followArtist: createFollowing,
        unfollowArtist: deleteFollowing
    }
}();