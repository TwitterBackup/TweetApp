$(document).on("mouseenter", ".btn-following", function () {
    $(this).hide();
    $("#remove" + $(this).attr("btn-following-id")).show();
});

$(document).on("mouseleave", ".btn-remove", function () {
    $(this).hide();

    var btnFollowing = $("#following" + $(this).attr("btn-remove-id"));
    btnFollowing.show();

    btnFollowing.html('<i class="fa fa-twitter"></i> Favorite');
    btnFollowing.css("background-color", "#427fed");
});




//$(".btn-follow").on("click", function() {
//        var btn = $(this);
//        btn.html('<i class="fa fa-twitter"></i> Favorite');
//        //btn.css("background-color", "#427fed");
//    });

$(document).on("mouseenter", ".btn-follow", function () {
    var btn = $(this);
    console.log("enter");
    btn.css("background-color", "#E8F5FD");
});

$(document).on("mouseleave", ".btn-follow", function () {
    var btn = $(this);
    btn.css("background-color", "white");
});
