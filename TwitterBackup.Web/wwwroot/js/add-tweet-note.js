$(document).on('submit',
    '.tweet-note-form',
    function (event) {

        event.preventDefault();

        var url = this.action;
        var data = $(this).serialize();

        var textArea = $(this.getElementsByClassName('comment-field'));
        var tweetId = $(this).attr("tweet-id");

        if (!textArea.hasClass('hidden')) {
            var comment = $('#comment-box-' + tweetId).val();

            $.post(url,
                data,
                function (response) {
                    $("#note-content-" + tweetId).html(comment);
                    $("#media-body-" + tweetId).removeClass("hide");
                });
        }

        $(textArea).toggleClass('hidden');
    });


$('#myTab a').click(function (e) {
    e.preventDefault();

    var tab = $(this);
    var tweeterId = tab.attr("tweeter-id");
    var userName = tab.attr("user-name");
    var href = tab.attr("href");
    var url = "";

    if (href === "#saved") {
        url = "https://localhost:44347/Tweets/TweeterTweetsLikedFromUser?tweeterId=" + tweeterId + "&userName=" + userName;
        $.get(url, function (response) {
            var saved = $("#saved");
            saved.html(response);
        });
    }
    else {
        url = "https://localhost:44347/Tweets/TweeterNewTweets?tweeterId=" + tweeterId;
        $.get(url, function (response) {
            var newTab = $("#new");
            newTab.html(response);
        });
    }

    $(this).tab('show');
});
