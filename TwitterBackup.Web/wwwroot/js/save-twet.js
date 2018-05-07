$(document).on("submit", ".save-tweet-form",
    function (event) {

        event.preventDefault();

        var url = this.action;
        var data = $(this).serialize();

        var form = $(this);
        var tweetId = form.attr("tweet-id");
        
        $.post(url,
            data,
            function (response) {
                form.addClass("hide");
                $("#edit-form-note-" + tweetId).removeClass("hide");
                $("#retweet-" + tweetId).removeClass("hide");
                $("#remove-menu-" + tweetId).removeClass("hide");
            });
    });