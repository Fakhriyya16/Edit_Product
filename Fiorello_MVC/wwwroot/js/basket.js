$(document).ready(function () {

    $(document).on("click", "#products .add-basket", function () {
        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            type: "POST",
            url: `home/addproducttobasket?id=${id}`,
            success: function (response) {
                $(".rounded-circle").text(response.count);
                $(".rounded-circle").next().text(`CART ($${response.total})`);
            }
        })
    })

    $(document).on("click", "#products-in-cart .delete-product", function () {
        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            type: "POST",
            url: `cart/Delete?id=${id}`,
            success: function (response) {
                $(".rounded-circle").text(response.totalCount);
                $(".rounded-circle").next().text(`CART ($${response.totalPrice})`);
                $(".total").text(`$${response.totalPrice}`);
                $(`[data-id=${id}]`).closest(".product-row").remove();
            }
        })
    })

    $(document).on("click", "#products-in-cart .item-count .decrease", function () {
        let id = parseInt($(this).attr("data-id"));
        let item = $(this).siblings('.count-display');
        let button = $(this);
        $.ajax({
            type: "POST",
            url: `cart/DecreaseAmount?id=${id}`,
            success: function (response) {
                $(".rounded-circle").text(response.totalCount);
                $(".rounded-circle").next().text(`CART ($${response.totalPrice})`);
                $(".total").text(`$${response.totalPrice}`);
                item.text(`${response.itemCount}`);

                if (parseInt(item.text(), 10) <= 1) {
                    button.addClass("disabled").attr("disabled", true);
                } else {
                    button.removeClass("disabled").attr("disabled", false);
                }
            }
        })
    })

    $(document).on("click", "#products-in-cart .item-count .increase", function () {
        let id = parseInt($(this).attr("data-id"));
        let item = $(this).siblings('.count-display');
        let button = $(this).siblings('.decrease');
        $.ajax({
            type: "POST",
            url: `cart/IncreaseAmount?id=${id}`,
            success: function (response) {
                $(".rounded-circle").text(response.totalCount);
                $(".rounded-circle").next().text(`CART ($${response.totalPrice})`);
                $(".total").text(`$${response.totalPrice}`);
                item.text(`${response.itemCount}`)
                if (parseInt(item.text(), 10) > 1) {
                    button.removeClass("disabled").attr("disabled", false);
                }
            }
        })
    })
})