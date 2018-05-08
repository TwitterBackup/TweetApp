$(document).on("click", ".remove-tweet",
    function (event) {
        event.preventDefault();

        var tweetId = $(this).attr("tweetId");
        $('#modal-' + tweetId).modal('show');

    });

$(document).on("submit", ".remove-tweet-form",
    function (event) {

        event.preventDefault();

        var url = this.action;
        var data = $(this).serialize();

        var form = $(this);
        var tweetId = form.attr("tweet-id");
        console.log(data);
        $.post(url,
            data,
            function (response) {
                $('#modal-' + tweetId).modal("hide");
                form.addClass("hide");
                $("#edit-form-note-" + tweetId).addClass("hide");
                $("#retweet-" + tweetId).addClass("hide");
                $("#remove-menu-" + tweetId).addClass("hide");
                $("#save-" + tweetId).removeClass("hide");
            });
    });