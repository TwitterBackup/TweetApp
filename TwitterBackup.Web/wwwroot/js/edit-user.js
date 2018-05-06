
$(function () {
    $(document).on("submit", ".edit-user-class",
        function (event) {

            event.preventDefault();

            var url = this.action;
            var data = $(this).serialize();

            var modalContent;

            $.post(url, data, function (response) {

                var userName = $("#UserName").val();
                var fName = $("#FirstName").val();
                var lName = $("#LastName").val();
                var email = $("#Email").val();

                $("#edit").modal("hide");


                if (response !== "success") {
                    console.log(response);
                    //add error modal

                    $("#alert-modal-failure-message").text(response);

                    modalContent = $("#alert-modal-failure-content");

                    modalContent.load(url, function () {
                        $("#alert-modal-failure").modal("show");
                    });
                    return;
                }

                $("td:contains('" + userName + "')").next().text(fName);
                $("td:contains('" + userName + "')").next().next().text(lName);
                $("td:contains('" + userName + "')").next().next().next().next().text(email);
                
            });

    });
});

