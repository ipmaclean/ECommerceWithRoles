$(document).ready(function($){
    
    // jQuery sticky Menu
    
	$(".mainmenu-area").sticky({topSpacing:0});
    
    
    $('.owl-carousel').owlCarousel({
        loop: true,
        nav: true,
        margin: 20,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 3,
            },
            1000: {
                items: 5,
            }
        },
        navText: ["<i class='fas fa-chevron-left fa-2x'></i>", "<i class='fas fa-chevron-right fa-2x'></i>"]
    });
    
    // Bootstrap Mobile Menu fix
    $(".navbar-nav li a").click(function(){
        $(".navbar-collapse").removeClass('in');
    });    
    
    // jQuery Scroll effect
    $('.navbar-nav li a, .scroll-to-up').bind('click', function(event) {
        var $anchor = $(this);
        var headerH = $('.header-area').outerHeight();
        $('html, body').stop().animate({
            scrollTop : $($anchor.attr('href')).offset().top - headerH + "px"
        }, 1200, 'easeInOutExpo');

        event.preventDefault();
    });
});

// Make basket widget jump when an item is added
function bounceCart(result) {
    $('#shopping-basket-partial').toggleClass('animate__animated animate__bounce');
    console.log("added");
    setTimeout(function () {
        $('#shopping-basket-partial').toggleClass('animate__animated animate__bounce');
        console.log("Removed");
    }, 900);
};