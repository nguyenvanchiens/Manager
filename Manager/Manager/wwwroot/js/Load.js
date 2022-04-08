$(document).ready(function () {
    getBaseURL();
    function getBaseURL() {
        // Base Url for localhost
       /* var url = location.href;  // window.location.href;*/
        var pathname = location.pathname;  // window.location.pathname;
        var lisa = $('.sidebar-menu li a');
        $.each(lisa, function (i, ele) {
            var herf = $(ele).attr('href');
            if (pathname == herf) {
                $(ele).css('color', 'white')
                $(ele).parents('.sidebar-dropdown').addClass('active');
                $(ele).parents('.sidebar-submenu').css('display', 'block');
            }
        })
    } 
})