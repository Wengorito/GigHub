var GigsController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, doneAttend, fail);
        else
            attendanceService.deleteAttendance(gigId, doneUnattend, fail);
    };

    var doneAttend = function () {
        //var text = (button.text() == "Going") ? "Going?" : "Going";
        var text = "Going";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var doneUnattend = function () {
        //var text = (button.text() == "Going") ? "Going?" : "Going";
        var text = "Going?";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Toggle attendance failed");
    };

    return {
        init: init
    };

}(AttendanceService);