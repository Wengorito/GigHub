var FollowingsController = function (followingService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-delete-following", deleteFollowing);
    };

    var deleteFollowing = function (e) {
        console.log('click');
        button = $(e.target);

        var artistId = button.attr("data-artist-id");
        
        bootbox.confirm({
            size: "small",
            message: "Unfollow artist?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result) {
                    followingService.unfollowArtist(artistId, done, fail);
                }
            }
        });
    };

    var done = function () {
        button.parents("li").fadeOut(function () {
            $(this).remove();
        });
    }

    var fail = function () {
        alert("Unfollow failed");
    };

    return {
        init: init
    };

}(FollowingService);