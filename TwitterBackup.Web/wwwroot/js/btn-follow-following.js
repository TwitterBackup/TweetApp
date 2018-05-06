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
            
            //if (tweeterBtn.hasClass("btn-follow")) {
                $.post(url, data, function (response) {
                    if (response !== "success") {
                        console.log(response);
                        //add error modal
                        var modalContent = $("#error-modal");
                        var modalErrorMessage = $(".modal-error-message");
                        modalErrorMessage.text(response);

                        modalContent.load(url,
                            function () {
                                $("#add-to-favorite-error").modal("show");
                            });
                        return;
                    }

                    tweeterBtn.hide();

                    $("#following" + tweeterId).show();

                    //tweeterBtn.toggleClass("btn-following");
                    //tweeterBtn.toggleClass("btn-follow");


                    //if (tweeterBtn.hasClass("btn-follow")) {
                    //form.attr("action", "/Tweeters/AddTweeterToFavourite");
                    //tweeterBtn.html('<i class="fa fa-twitter"></i> Add To Favorite');
                    //} else {
                    //    //tweeterBtn.hide;

                    //    form.attr("action", "/Tweeters/Remove"); //RemoveTweeterFromFavourite
                    //    //form.load("/Tweeters/Remove", tweeterId = 123);
                    //    tweeterBtn.html('<i class="fa fa-twitter"></i> Favorite');
                    //}

                    //var tweeterCard = $('#' + tweeterId).closest('.tweeter-card');
                    //tweeterCard.replaceWith(response);
                });
            //}

            //else if (tweeterBtn.hasClass("btn-remove")) {

            //    $.get(url, data, function (response) {

            //    if (response !== "success") {
            //        console.log(response);
            //        //add error modal
            //        var modalContent = $("#error-modal");
            //        var modalErrorMessage = $(".modal-error-message");
            //        modalErrorMessage.text(response);

            //        modalContent.load(url,
            //            function () {
            //                var modal = $(".modal");
            //                modal.modal("show");
            //            });
            //        return;
            //    }

            //    //var tweeterBtn = $("#" + tweeterId);

            //    tweeterBtn.toggleClass("btn-following");
            //    tweeterBtn.toggleClass("btn-follow");

            //    if (tweeterBtn.hasClass("btn-follow")) {
            //        form.attr("action", "/Tweeters/AddTweeterToFavourite");
            //        //tweeterBtn.html('<i class="fa fa-twitter"></i> Add To Favorite');
            //    } else {
            //        form.attr("action", "/Tweeters/Remove"); //RemoveTweeterFromFavourite
            //        tweeterBtn.html('<i class="fa fa-twitter"></i> Favorite');
            //    }

            //    //var tweeterCard = $('#' + tweeterId).closest('.tweeter-card');
            //    //tweeterCard.replaceWith(response);
            //    });
                
            //}



        });
    });

//Favorite - (hover) - Remove 
//Remove - (click) - Add To Favorite