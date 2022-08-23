var GigDetailsController = function (followingService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-follow", toggleFollowing);
    };

    var toggleFollowing = function (e) {
        button = $(e.target);

        var artistId = button.attr("data-artist-id");

        if (button.hasClass("btn-default"))
            followingService.followArtist(artistId, doneFollow, fail);
        else
            followingService.unfollowArtist(artistId, doneUnfollow, fail);
    };

    var doneFollow = function () {
        // Honestly no idea why this condition does not function properly.
        // Using 2 separate methods instead
        //var text = (button.text() == "Following") ? "Follow?" : "Following";
        var text = "Following";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var doneUnfollow = function () {
        //var text = (button.text() == "Following") ? "Follow?" : "Following";
        var text = "Follow?";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Toggle follow failed");
    };

    return {
        init: init
    };

}(FollowingService);