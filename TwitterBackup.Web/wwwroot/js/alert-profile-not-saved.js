$(function () {
    $(document).on("click", ".twName",
        function (event) {

            event.preventDefault();

            var url = this.href;
            
            $.get(url, function (response) {

                if (response == "This tweeter is not saved. Please first save the tweeter in order to see its profile!") {
                    console.log(response);

                    //add error to selector in modal
                    $("#alert-modal-failure-message").text(response);

                    //show modal popup window
                    $("#alert-modal-failure").modal("show");

                    return;
                }

                window.location.href = url;

            });
        });
});

