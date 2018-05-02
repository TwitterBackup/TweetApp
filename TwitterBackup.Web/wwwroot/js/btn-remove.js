////Favorite - (hover) - Remove 
////Remove - (click) - Add To Favorite

//$(function () {
//    $(document).on("click", ".btn-remove",
//        function (event) {
//            //event.preventDefault();

//            var url = this.action;
//            var data = $(this).serialize();
//            var tweeterId = this.getAttribute("btn-remove-id");
//            var tweeterBtn = $("#remove" + tweeterId);

//            $.get(url, data, function (response) {
//                if (response !== "success") {
//                    console.log(response);
//                    //add error modal
//                    var modalContent = $("#error-modal");
//                    var modalErrorMessage = $(".modal-error-message");
//                    modalErrorMessage.text(response);

//                    modalContent.load(url,
//                        function () {
//                            $("#add-to-favorite-error").modal("show");
//                        });
//                    return;
//                }

//                //tweeterBtn.hide();

//                //$("#follow" + tweeterId).show();
//            });

//            $("#delete-tweeter-modal-container").modal("show");


//        });
//});

