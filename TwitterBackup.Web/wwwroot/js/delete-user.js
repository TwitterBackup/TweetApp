$(function () {
    $(document).on("click", ".delete-user",
        function (event) {
            event.preventDefault();


            var url = this.action;
            var data = $(this).serialize();
            var form = $(this);

            var modalContent;

            $.post(url, data, function (response) {

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
                modalContent = $("#alert-modal-success-content");
                modalContent.load(url, function () {
                    $("#alert-modal-success").modal("show");
                    return;
                });

                form.closest("tr").remove();

            });
        });
});
