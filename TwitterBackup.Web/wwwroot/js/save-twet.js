$(document).on("submit", ".save-tweet-form",
    function (event) {

        event.preventDefault();

        var url = this.action;
        var data = $(this).serialize();

        var form = $(this);
        
        $.post(url,
            data,
            function (response) {
                form.toggleClass("hide");
                $(".tweet-note-form").toggleClass("hide");
            });
});