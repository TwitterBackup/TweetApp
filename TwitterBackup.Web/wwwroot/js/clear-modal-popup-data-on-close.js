$(document).ready(function () {
    $("#edit").on("hidden.bs.modal", function () {
        $(this).removeData();
    });
});
