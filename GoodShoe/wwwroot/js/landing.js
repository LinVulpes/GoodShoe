// Hero Slider JavaScript
document.addEventListener('DOMContentLoaded', function() {
    initializeHeroSlider();
});

function initializeHeroSlider() {
    const slides = document.querySelectorAll('.slide');
    const prevBtn = document.getElementById('prevBtn');
    const nextBtn = document.getElementById('nextBtn');

    let currentSlide = 0;
    let isTransitioning = false;
    
    /*
    let autoplayInterval;
    const autoplayDelay = 5000; // 5 seconds*/

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
            resetAutoplay();
        });
    }

    if (prevBtn) {
        prevBtn.addEventListener('click', (e) => {
            e.preventDefault();
            prevSlide();
            resetAutoplay();
        });
    }

    // Keyboard navigation
    document.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowLeft') {
            prevSlide();
            resetAutoplay();
        } else if (e.key === 'ArrowRight') {
            nextSlide();
            resetAutoplay();
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
                resetAutoplay();
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
            resetAutoplay();
        }
    }

    // Autoplay functionality
    function startAutoplay() {
        if (slides.length <= 1) return; // Don't autoplay if only one slide

        autoplayInterval = setInterval(() => {
            nextSlide();
        }, autoplayDelay);
    }

    function stopAutoplay() {
        if (autoplayInterval) {
            clearInterval(autoplayInterval);
            autoplayInterval = null;
        }
    }

    function resetAutoplay() {
        stopAutoplay();
        startAutoplay();
    }

    // Pause autoplay on hover and focus
    const heroSlider = document.querySelector('.hero-slider');
    if (heroSlider) {
        heroSlider.addEventListener('mouseenter', stopAutoplay);
        heroSlider.addEventListener('mouseleave', startAutoplay);
        heroSlider.addEventListener('focusin', stopAutoplay);
        heroSlider.addEventListener('focusout', startAutoplay);
    }

    // Pause autoplay when page is not visible
    document.addEventListener('visibilitychange', () => {
        if (document.hidden) {
            stopAutoplay();
        } else if (!document.hidden && slides.length > 1) {
            startAutoplay();
        }
    });

    // Pause autoplay when window loses focus
    window.addEventListener('blur', stopAutoplay);
    window.addEventListener('focus', () => {
        if (slides.length > 1) {
            startAutoplay();
        }
    });

    // Initialize slider
    if (slides.length > 0) {
        initializeSlides();

        // Start autoplay only if there are multiple slides
        if (slides.length > 1) {
            startAutoplay();
        }

        console.log(`Hero slider initialized with ${slides.length} slide(s)`);
    } else {
        console.warn('No slides found for hero slider');
    }

    // Add resize handler to ensure proper display
    window.addEventListener('resize', debounce(() => {
        // Recalculate any position-dependent elements if needed
        // Currently not needed but good for future enhancements
    }, 250));

    // Return public methods for external control (optional)
    return {
        nextSlide,
        prevSlide,
        showSlide: (index) => {
            if (index >= 0 && index < slides.length) {
                showSlide(index);
                resetAutoplay();
            }
        },
        getCurrentSlide: () => currentSlide,
        getTotalSlides: () => slides.length,
        startAutoplay,
        stopAutoplay
    };
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

// Add CSS for loading states
const style = document.createElement('style');
style.textContent = `
    body {
        opacity: 0;
        transition: opacity 0.3s ease;
    }
    
    body.loaded {
        opacity: 1;
    }
    
    .slider-container {
        cursor: grab;
    }
    
    .slider-container:active {
        cursor: grabbing;
    }
    
    .slide {
        user-select: none;
    }
`;
document.head.appendChild(style);