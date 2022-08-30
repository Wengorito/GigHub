var AttendancesController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-delete-attendance", deleteAttendance);
    };

    var deleteAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        bootbox.confirm({
            size: "small",
            message: "Are you sure you're not going?",
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
                    attendanceService.deleteAttendance(gigId, done, fail);  
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
        alert("Delete attendance failed");
    };

    return {
        init: init
    };

}(AttendanceService);