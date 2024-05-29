$(document).ready(function () {
    $(document).on("click", "#edit-product .images .product-card .delete", function () {
        let button = $(this); 
        let id = parseInt(button.attr("data-id"));
        if (button.is(':disabled')) {
            return; 
        }

        $.ajax({
            type: "POST",
            url: `/product/deleteimage?id=${id}`,
            success: function (response) {
                $(`.product-card[data-id="${id}"]`).remove();
            },
        });
    });

    $(document).on("click", "#edit-product .make-main", function () {
        let button = $(this); 
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `/product/makemain?id=${id}`,
            success: function (response) {

            },
        });
    });
});