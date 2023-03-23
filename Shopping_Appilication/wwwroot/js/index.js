// Owlcarousel
$(document).ready(function () {
    $(".owl-carousel").owlCarousel({
        loop: true,
        margin: 10,
        nav: true,
        autoplay: true,
        autoplayTimeout: 2000,
        autoplayHoverPause: true,
        center: true,
        navText: [
            "<i class='fa fa-angle-left'></i>",
            "<i class='fa fa-angle-right'></i>"
        ],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 3
            }
        }
    });
});

let cart = document.querySelector('.cart');
let cartfield = document.querySelector('.cart-field');
let add = document.getElementsByClassName('add');
for (let but of add) {
    but.onclick = e => {
        let item = Number(cart.getAttribute('data-count') || 0);
        cart.setAttribute('data-count', item + 1);
        cart.classList.add('on');
        //
        let parent = e.target.parentNode.parentNode.parentNode;
        let image = parent.querySelector('img');
        let span = document.createElement('span');
        span.className = 'image-carior';
        parent.insertBefore(span, parent.lastElementChild);
        let s_image = image.cloneNode(false);
        span.appendChild(s_image);
        span.classList.add('active');
        setTimeout(() => {
            span.classList.remove('active');
            span.removeChild(s_image);
        }, 500);

        let clone = parent.cloneNode(true);
        clone.querySelector('.add').style.display = "none";
        clone.querySelector('.add').removeAttribute('class');
        cartfield.appendChild(clone);
        if (clone) {
            cart.onclick = () => {
                cartfield.classList.toggle('display');
            }
        }
    }
}