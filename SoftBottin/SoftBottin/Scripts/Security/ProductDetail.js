

// Adicionar Producto al carrito de compras
function addProductToShoppingCart(shoeReference, shoeName, shoePrice, shoeColor, showSize) {

    var cart = $('.app-shopping');
    //var imgtodrag = $(".img-responsive").eq(4);
    var imgtodrag = $("#zoom_03").eq(0);;

    if (imgtodrag) {
        var imgclone = imgtodrag.clone()
            .offset({
                top: imgtodrag.offset().top + 200,
                left: imgtodrag.offset().left
            })
            .css({
                'opacity': '0.5',
                'position': 'absolute',
                'height': '150px',
                'width': '150px',
                'z-index': '100'
            })
            .appendTo($('body'))
            .animate({
                'top': cart.offset().top + 10,
                'left': cart.offset().left + 10,
                'width': 75,
                'height': 75
            }, 500, 'easeInOutExpo');

        setTimeout(function () {
            cart.effect("shake", {
                times: 2
            }, 100);
        }, 750);

        imgclone.animate({
            'width': 0,
            'height': 0
        }, function () {
            $(this).detach();
            $("#numberShoesPick").html((parseInt($("#numberShoesPick").html()) + 1));
        });
    }
}


