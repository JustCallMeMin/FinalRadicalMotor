$(document).ready(function () {
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

    // Function to fetch services
    function fetchServices() {
        fetch('https://localhost:44304/api/Service', {
            credentials: 'include' // Ensure cookies are sent with the request
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(services => {
                const selectElement = document.getElementById('service-select');
                services.forEach(function (service) {
                    var option = document.createElement("option");
                    option.value = service.serviceId;
                    option.text = service.serviceName;
                    selectElement.appendChild(option);
                });
            })
            .catch(error => {
                console.error('Error loading services:', error);
                alert('Failed to load services. Please try again later.');
            });
    }

    // Function to submit the appointment form
    function submitAppointmentForm(formData) {
        $.ajax({
            url: 'https://localhost:44304/api/AppointmentDetails/book',
            type: 'POST',
            data: JSON.stringify(formData),
            contentType: 'application/json; charset=utf-8',
            xhrFields: {
                withCredentials: true
            },
            success: function (response) {
                alert('Your appointment has been successfully booked!');
            },
            error: function (xhr, status, error) {
                alert('Failed to book your appointment. Please try again.');
            }
        });
    }

    // Get the value of the cookie isLoggedIn
    var isLoggedIn = getCookie('isLoggedIn');
    if (isLoggedIn === 'true') {
        fetchServices();
        $('form').submit(function (event) {
            event.preventDefault();
            var dateElement = $('#date1').datetimepicker('viewDate');
            var formattedDate = dateElement.format('YYYY-MM-DDTHH:mm:ss.SSS[Z]'); 
            var formData = {
                FullName: $('input[placeholder="Your Name"]').val(),
                PhoneNumber: $('input[placeholder="Your Phone Number"]').val(),
                ServiceId: $('#service-select').val(),
                ServiceDate: formattedDate,
                Notes: $('textarea[placeholder="Notes"]').val()
            };
            submitAppointmentForm(formData);
        });
    } else {
    }
});
