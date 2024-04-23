(function ($) {
    "use strict";
    // Function to set a cookie
    function setCookie(name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = ";expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + ";path=/";
    }


    // Function to get a cookie by name
    function getCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1);
            if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    // Function to delete a cookie by name
    function deleteCookie(name) {
        document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    }

    // Function to handle successful login with cookies
    function handleLoginSuccess(response) {
        var rememberMe = $('#remember').is(':checked');
        var expiresIn = rememberMe ? 30 : 1;

        // Check if response contains accountId
        if (response && response.accountId) {
            setCookie('isLoggedIn', 'true', expiresIn);
            setCookie('accountId', response.accountId, expiresIn);
            checkUserPermissions(response.accountId);
            $(".modal_close").click();
            toggleSignInOutButtons();

        } else {
            console.error('Login failed: No accountId found in response');
            // Handle login failure here, e.g., display error message
        }
    }

    // Handle logout
    function handleLogout() {
        deleteCookie('isLoggedIn');
        deleteCookie('accountId');
        deleteCookie('userType');
        toggleSignInOutButtons();
    }

    // Toggle visibility of the Sign In and Logout buttons based on authentication status
    function checkLoginStatus() {
        var isLoggedIn = getCookie('isLoggedIn') === 'true';
        return isLoggedIn;
    }
    // Toggle visibility of the Sign In and Logout buttons based on authentication status
    function toggleSignInOutButtons() {
        var isLoggedIn = checkLoginStatus();

        if (isLoggedIn) {
            $("#modal_trigger").attr('style', 'display: none !important');
            $("#logout_trigger").attr('style', 'display: block !important');
        } else {
            $("#modal_trigger").attr('style', 'display: block !important');
            $("#logout_trigger").attr('style', 'display: none !important');
        }
    }

    // Redirect to the home page
    function redirectToIndex() {
        var indexUrl = $('#login_btn').data('index-url');
        window.location.href = indexUrl;
    }

    // Initialize UI components like carousel and modal
    function initializeUIComponents() {
        $(".loop").owlCarousel({
            center: true,
            items: 1,
            loop: true,
            autoplay: true,
            nav: true,
            margin: 0,
            responsive: {
                1200: { items: 5 },
                992: { items: 3 },
                760: { items: 2 }
            }
        });

        $("#modal_trigger").leanModal({
            top: 100,
            overlay: 0.6,
            closeButton: ".modal_close"
        });
    }

    // Setup event handlers for forms and interactions
    function setupEventHandlers() {
        $("#login_form").click(function () {
            $(".social_login").hide();
            $(".user_login").show();
            $(".user_forgotpassword").hide();
            return false;
        });

        $("#register_form").click(function () {
            $(".social_login").hide();
            $(".user_register").show();
            $(".header_title").text("Sign Up");
            $(".user_forgotpassword").hide();
            return false;
        });

        $("#forgot_password").click(function () {
            $(".user_login").hide();
            $(".user_forgotpassword").show();
            $(".header_title").text("Forgot Password");
            return false;
        });

        $(".back_btn").click(function () {
            $(".user_login, .user_register, .user_forgotpassword").hide();
            $(".social_login").show();
            $(".header_title").text("Login");
            $(this).closest('form').find("input[type='text'], input[type='password'], input[type='email']").val('');
            return false;
        });

        $('#loginForm').on('submit', function (e) {
            e.preventDefault();
            var formData = {
                email: $('#email').val(),
                password: $('#log_password').val(),
                rememberMe: $('#remember').is(':checked')
            };

            $.ajax({
                url: 'https://localhost:44304/api/LoginApi/login',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData),
                success: handleLoginSuccess,
                error: function (xhr, status, error) {
                    console.error('Error during login', xhr.responseText);
                    $('#loginErrorMessage').text('Login failed: ' + xhr.responseText).show();
                }
            });
        });
        $("#registrationForm").on("submit", function (e) {
            e.preventDefault();

            var formData = {
                username: $(this).find('input[name="fullName"]').val(),
                email: $(this).find('input[name="email"]').val(),
                password: $(this).find('input[name="password"]').val(),
            };

            $.ajax({
                type: "POST",
                url: "https://localhost:44304/api/Accounts",
                contentType: "application/json",
                data: JSON.stringify(formData),
                success: function (response) {
                    console.log("Account created successfully", response);
                    alert("Registration successful!");
                },
                error: function (response) {
                    console.error("Error during registration", response);
                    alert("Registration failed: " + response.responseText);
                },
            });
        });
        $('#logout_trigger').click(function (e) {
            e.preventDefault();
            handleLogout();
        });

        // Password visibility toggle
        $('.toggle-password').click(function () {
            $(this).toggleClass('fa-eye fa-eye-slash');
            var input = $(this).prev('input');
            input.attr('type', input.attr('type') === 'password' ? 'text' : 'password');
        });
    }

    // Initialize everything on document ready
    $(document).ready(function () {
        initializeUIComponents();
        setupEventHandlers();
        toggleSignInOutButtons();
        var accountId = getCookie('accountId');
    });

    function checkUserPermissions(accountId) {
        if (!accountId) {
            console.error('No accountId provided');
            return;
        }

        $.ajax({
            url: `https://localhost:44304/api/Accounts/${accountId}/type`,
            type: 'GET',
            success: function (response) {
                if (!response || typeof response !== 'object' || !('typeName' in response)) {
                    console.error('Invalid response or TypeName missing');
                    return;
                }

                // Truy cập và kiểm tra TypeName
                var rememberMe = $('#remember').is(':checked');
                var expiresIn = rememberMe ? 30 : 1;
                var typeName = response.typeName;
                setCookie('userType', typeName, expiresIn);
                if (typeName === 'Admin') {
                    window.location.href = '/admin'
                } else if (typeName === 'Member') {
                } else {
                    console.error('Unknown account type:', typeName);
                }
            },
            error: function (xhr) {
                console.error('Error fetching account type:', xhr.responseText);
            }
        });
    }

})(jQuery);
