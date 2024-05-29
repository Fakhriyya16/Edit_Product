$(document).ready(function () {
    $(document).on("click", "#archive .add-archive", function () {
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `archive/archive?id=${id}`,
            success: function (response) {
                button.closest('.category-data').remove();
            },
        });
    });

    $(document).on("click", "#dearchive .restore", function () {
        let button = $(this);
        let id = parseInt(button.attr("data-id"));

        $.ajax({
            type: "POST",
            url: `archive?id=${id}`,
            success: function (response) {
                button.closest('.category-data').remove();
            },
        });
    });
});