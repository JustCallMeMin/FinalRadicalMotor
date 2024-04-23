(function ($) {
    "use strict";

    function deleteCookie(name) {
        document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    }
    // Handle logout
    function handleLogout() {
        deleteCookie('isLoggedIn');
        deleteCookie('accountId');
        deleteCookie('userType');
        window.location.href = '/home'
    }

        $('#logout').click(function (e) {
            e.preventDefault();
            handleLogout();
        });

})(jQuery);
