// Landing Page JavaScript
document.addEventListener('DOMContentLoaded', function() {
    initializeHeroSlider();
    initializeProductCards();
});

function initializeHeroSlider() {
    const slides = document.querySelectorAll('.slide');
    const prevBtn = document.getElementById('prevBtn');
    const nextBtn = document.getElementById('nextBtn');

    let currentSlide = 0;
    let isTransitioning = false;

    // Ensure only the first slide is active initially
    function initializeSlides() {
        slides.forEach((slide, index) => {
            if (index === 0) {
                slide.classList.add('active');
            } else {
                slide.classList.remove('active');
            }
        });
    }

    // Function to show a specific slide
    function showSlide(index) {
        if (isTransitioning || index === currentSlide) return;

        isTransitioning = true;

        // Remove active class from current slide
        slides[currentSlide].classList.remove('active');

        // Update current slide index
        currentSlide = index;

        // Add active class to new slide
        slides[currentSlide].classList.add('active');

        // Reset transition flag after animation completes
        setTimeout(() => {
            isTransitioning = false;
        }, 600); // Match CSS transition duration
    }

    // Function to go to next slide
    function nextSlide() {
        const nextIndex = (currentSlide + 1) % slides.length;
        showSlide(nextIndex);
    }

    // Function to go to previous slide
    function prevSlide() {
        const prevIndex = (currentSlide - 1 + slides.length) % slides.length;
        showSlide(prevIndex);
    }

    // Event listeners for navigation buttons
    if (nextBtn) {
        nextBtn.addEventListener('click', (e) => {
            e.preventDefault();
            nextSlide();
        });
    }

    if (prevBtn) {
        prevBtn.addEventListener('click', (e) => {
            e.preventDefault();
            prevSlide();
        });
    }

    // Keyboard navigation
    document.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowLeft') {
            prevSlide();
        } else if (e.key === 'ArrowRight') {
            nextSlide();
        }
    });

    // Touch/swipe support for mobile
    let touchStartX = 0;
    let touchEndX = 0;
    const swipeThreshold = 50;

    const sliderContainer = document.querySelector('.slider-container');

    if (sliderContainer) {
        // Touch events
        sliderContainer.addEventListener('touchstart', (e) => {
            touchStartX = e.changedTouches[0].screenX;
        }, { passive: true });

        sliderContainer.addEventListener('touchend', (e) => {
            touchEndX = e.changedTouches[0].screenX;
            handleSwipe();
        }, { passive: true });

        // Mouse events for desktop dragging (optional)
        let isDragging = false;
        let mouseStartX = 0;

        sliderContainer.addEventListener('mousedown', (e) => {
            isDragging = true;
            mouseStartX = e.clientX;
            sliderContainer.style.cursor = 'grabbing';
        });

        document.addEventListener('mousemove', (e) => {
            if (!isDragging) return;
            e.preventDefault();
        });

        document.addEventListener('mouseup', (e) => {
            if (!isDragging) return;

            isDragging = false;
            sliderContainer.style.cursor = 'grab';

            const mouseEndX = e.clientX;
            const diff = mouseStartX - mouseEndX;

            if (Math.abs(diff) > swipeThreshold) {
                if (diff > 0) {
                    nextSlide();
                } else {
                    prevSlide();
                }
            }
        });
    }

    function handleSwipe() {
        const diff = touchStartX - touchEndX;

        if (Math.abs(diff) > swipeThreshold) {
            if (diff > 0) {
                // Swipe left - next slide
                nextSlide();
            } else {
                // Swipe right - previous slide
                prevSlide();
            }
        }
    }

    // Initialize slider
    if (slides.length > 0) {
        initializeSlides();
        console.log(`Hero slider initialized with ${slides.length} slide(s)`);
    } else {
        console.warn('No slides found for hero slider');
    }
}

// Product Cards Functionality
function initializeProductCards() {
    const productCards = document.querySelectorAll('.product-card-new');

    productCards.forEach((card, index) => {
        // Add click functionality to product cards
        card.addEventListener('click', function() {
            const productName = card.querySelector('.product-name').textContent;
            console.log(`Clicked on product: ${productName}`);

            // Navigate to products page
            window.location.href = '~/Home/Products';
        });

        // Add hover effects
        card.addEventListener('mouseenter', function() {
            this.style.cursor = 'pointer';
        });

        // Intersection Observer for scroll animations
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    // Add staggered animation delay
                    setTimeout(() => {
                        entry.target.classList.add('animate-in');
                    }, index * 100);
                    observer.unobserve(entry.target);
                }
            });
        }, {
            threshold: 0.1,
            rootMargin: '0px 0px -50px 0px'
        });

        observer.observe(card);
    });
}

// Utility function for debouncing
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Add smooth loading animation
window.addEventListener('load', function() {
    document.body.classList.add('loaded');

    // Add fade-in effect for the hero slider
    const heroSlider = document.querySelector('.hero-slider');
    if (heroSlider) {
        heroSlider.style.opacity = '0';
        heroSlider.style.transition = 'opacity 0.5s ease';

        setTimeout(() => {
            heroSlider.style.opacity = '1';
        }, 100);
    }
});

//   Video AutoPlay
const text = "Discover the Latest Trends";
const autotypeEl = document.querySelector(".autotype");
let index = 0;

function typeWriter() {
    if (index < text.length) {
        autotypeEl.textContent += text.charAt(index);
        index++;
        setTimeout(typeWriter, 100); // typing speed
    }
}

// Add CSS for loading states and animations
const style = document.createElement('style');
style.textContent = `
    body {
        opacity: 0;
        transition: opacity 0.5s ease;
    }
    
    body.loaded {
        opacity: 1;
    }
    
    .slider-container {
        cursor: grab;
        user-select: none;
    }
    
    .slider-container:active {
        cursor: grabbing;
    }
    
    .slide {
        user-select: none;
        -webkit-backface-visibility: hidden;
        backface-visibility: hidden;
        transform: translateZ(0);
    }
    
    .slide-image {
        -webkit-backface-visibility: hidden;
        backface-visibility: hidden;
        transform: translateX(-50%) translateZ(0);
    }
    
    .hero-slider.visible .slide-image {
        will-change: transform;
    }
    
    .hero-slider:not(.visible) .slide-image {
        will-change: auto;
    }
    
    @media (prefers-reduced-motion: reduce) {
        .slide,
        .slide-image,
        .slider-nav {
            animation: none !important;
            transition: none !important;
        }
    }
`;
document.head.appendChild(style);