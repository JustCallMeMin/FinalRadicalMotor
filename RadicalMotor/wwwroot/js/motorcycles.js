(function ($) {
// Carousel navigation
    document.addEventListener('DOMContentLoaded', function () {
        var myCarousel = document.querySelector('#header-carousel');
        var carousel = new bootstrap.Carousel(myCarousel, {
            interval: 2000,
            wrap: true
        });

        document.querySelector('.carousel-control-prev').addEventListener('click', function () {
            carousel.prev();
        });

        document.querySelector('.carousel-control-next').addEventListener('click', function () {
            carousel.next();
        });
    });

//Swiper function
    document.addEventListener("DOMContentLoaded", function () {
        var swiper = new Swiper('.swiper-container', {
            loop: true,
            pagination: {
                el: '.swiper-pagination',
            },
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            autoplay: {
                delay: 5000,
            },
        });
    });



//Mototorcycles render
    document.addEventListener('DOMContentLoaded', function () {
        fetch('https://localhost:44304/api/Vehicles')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(vehicles => {
                console.log(vehicles);
                const container = document.querySelector('.wrap_product');
                vehicles.forEach(vehicle => {
                    const vehicleTypeName = vehicle.vehicleType || 'Unknown Type';

                    const vehicleDiv = document.createElement('div');
                    vehicleDiv.className = 'item_product';

                    vehicleDiv.innerHTML = `
                        <div class="box_img">
                            <img
                                src="${vehicle.imageUrls}"
                                height="auto"
                                max-width="100%"
                                alt="${vehicle.vehicleName}"
                                decoding="async"
                                loading="lazy"
                            />
                        </div>
                        <div class="box_content">
                            <span class="text_up">${vehicleTypeName}</span>
                            <div class="wrap display_flex">
                                <h3 class="text_up">${vehicle.vehicleName}</h3>
                                <div class="price text_up text_bold">${vehicle.price.toLocaleString()} VND</div>
                            </div>
                        </div>
                        <a
                            href="${vehicle.detailUrl}"
                            title="${vehicle.vehicleName}"
                            class="style_center max"></a>
                    `;
                    container.appendChild(vehicleDiv);
                });
            })
            .catch(error => {
                console.error('Error fetching vehicles:', error);
            });
    });
})(jQuery);
