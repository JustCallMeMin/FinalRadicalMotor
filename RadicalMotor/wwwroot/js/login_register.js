(function ($) {
    "use strict";
    
    function ajaxSetup() {
        $.ajaxSetup({
            beforeSend: function (xhr) {
                var token = localStorage.getItem('jwtToken') || sessionStorage.getItem('jwtToken');
                if (token) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token);
                }
            }
        });
    }

    function checkLoginStatus() {
        var token = localStorage.getItem('jwtToken') || sessionStorage.getItem('jwtToken');
        return token != null;
    }

    function handleLoginSuccess(response) {
        if (response && response.token) {
            var rememberMe = $('#remember').is(':checked');
            if (rememberMe) {
                localStorage.setItem('jwtToken', response.token);
            } else {
                sessionStorage.setItem('jwtToken', response.token);
            }
            checkUserPermissions();
            $(".modal_close").click();
            toggleSignInOutButtons();
        } else {
            console.error('Login failed: No token found in response');
        }
    }


    // Handle logout
    //function handleLogout() {
    //    deleteCookie('isLoggedIn');
    //    deleteCookie('accountId');
    //    deleteCookie('userType');
    //    toggleSignInOutButtons();
    //}
    function handleLogout() {
        localStorage.removeItem('jwtToken');
        sessionStorage.removeItem('jwtToken');
        toggleSignInOutButtons();
    }

    // Toggle visibility of the Sign In and Logout buttons based on authentication status
    //function checkLoginStatus() {
    //    var isLoggedIn = getCookie('isLoggedIn') === 'true';
    //    return isLoggedIn;
    //}
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
                url: 'https://localhost:44304/api/LoginApi',
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
                url: "https://localhost:44304/api/AccountsApi",
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
        ajaxSetup();
        initializeUIComponents();
        setupEventHandlers();
        toggleSignInOutButtons();
    });

    function checkUserPermissions() {
        var token = localStorage.getItem('jwtToken') || sessionStorage.getItem('jwtToken');
        if (!token) {
            console.error('No token found');
            return;
        }

        var decoded = jwt_decode(token);
        console.log('Decoded token:', decoded);

        if (decoded && decoded.typeId) {
            var typeId = decoded.typeId;
            console.log('User type:', typeId);

            if (typeId === '1') {
                console.log('Redirecting to /admin');
                window.location.href = '/admin';
            } else if (typeId !== '2') {
                console.error('Unknown account type:', typeId);
            }
        } else {
            console.error('Invalid token or typeId missing');
        }
    }



})(jQuery);
