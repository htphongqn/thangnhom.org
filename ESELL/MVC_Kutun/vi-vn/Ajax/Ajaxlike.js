﻿$(function () {
    $('.addlike').click(function () {
        var seourl = $(this).attr("newsurl");
        var getthis = $(this);
       
        $.ajax({
            type: "POST",
            url: "../vi-vn/Ajax-customer.aspx/addLike",
            data: "{seourl:'" + seourl + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (e) {

                if (e.d == "success") {
                    getthis.addClass("active");
                    getthis.html("<button class='add-to-wishlist  is-css' type='button'><span class='icon'> <i class='fa fa-heart'></i> </span> <span class='text'>Đã thêm vào yêu thích</span></button>");
                    //getthis.html("Đã thêm vào yêu thích");
                    //addListlike();
                }
                else
                    alert("Bạn đã thích sản phẩm này");

            }
        });
    });
});
function addListlike() {
    var seourl = "like";
    $.ajax({
        type: "POST",
        url: "../vi-vn/Ajax-customer.aspx/likepro",
        data: "{seourl:'" + seourl + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (e) {
            $('#likepro').html(e.d);
        }
    });
}