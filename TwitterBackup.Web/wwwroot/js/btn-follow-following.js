//Add To Favorite - (hover) - darker Add To Favorite
//darker Add To Favorite - (click) - Favorite

$(function () {
        $(document).on("click", ".form-follow",
        function (event) { event.preventDefault();

                var url = this.action;
                var data = $(this).serialize();
            var tweeterId = this.getAttribute("tweeter-id");
            var form = $(this);
            var tweeterBtn = $("#follow" + tweeterId);
            
            $.post(url, data, function (response) {
                    if (response !== "success") {
                        console.log(response);
                        //add error modal
                        var modalContent = $("#error-modal");
                        var modalErrorMessage = $(".modal-error-message");
                        modalErrorMessage.text(response);

                        modalContent.load(url,
                            function () {
                                $("#alert-modal-failure").modal("show");
                            });
                        return;
                    }

                    tweeterBtn.hide();

                    $("#following" + tweeterId).show();
            });
        });
    });
