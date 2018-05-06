$(document).ready(function () {
    // Transition effect for navbar
    $(window).on('scroll', function () {
        // checks if window is scrolled more than 500px, adds/removes solid class
        var width = $(document).width();

        if ($(this).scrollTop() > 50 || width < 979) {
            $('.navbar').css('background-color', 'black');
        } else {
            $('.navbar').css('background-color', 'rgba(255, 255, 255, 0)');
        }
    });
});