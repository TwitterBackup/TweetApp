$(document).on('submit',
        '.tweet-form',
        function (event) {

            event.preventDefault();

            var url = this.action;
            var data = $(this).serialize();
            console.log(url);

            var textArea = $(this.getElementsByClassName('comment-field'));

            if (!textArea.hasClass('hidden')) {
                var comment = $('.comment-box').val();

                $.post(url,
                    data,
                    function (responce) {

                        console.log(responce);
                    });
            }

            $(textArea).toggleClass('hidden');
});


$('#myTab a').click(function (e) {
    e.preventDefault();
    $(this).tab('show');
});
