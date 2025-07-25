function redirectToProduct(url) {
    window.location.href = url;
}

// Remove focus from dropdown buttons after click
document.addEventListener('DOMContentLoaded', function () {
    const dropdownButtons = document.querySelectorAll('.dropdown-toggle');
    dropdownButtons.forEach(button => {
        button.addEventListener('click', function () {
            // Remove focus after a short delay
            setTimeout(() => {
                this.blur();
            }, 150);
        });
    });

    // Remove focus when dropdown closes
    const dropdowns = document.querySelectorAll('.dropdown');
    dropdowns.forEach(dropdown => {
        dropdown.addEventListener('hidden.bs.dropdown', function () {
            const button = this.querySelector('.dropdown-toggle');
            if (button) {
                button.blur();
            }
        });
    });
});

//** ====== JS for Product details ====== **//
// Keep track of selected size and variant
let selectedSizeValue = null;
let selectedVariantId = null;

document.addEventListener('DOMContentLoaded', function () {
    const addToCartBtn = document.getElementById('addToCartBtn');
    const addToCartForm = document.getElementById('addToCartForm');

    if (!addToCartBtn || !addToCartForm || !productId) {
        console.warn('Product Details JS: Required elements not found.');
        return;
    }

    // Initialize size availability
    updateSizeAvailability();

    // Attach click handlers to size buttons
    document.querySelectorAll('.size-button').forEach(button => {
        button.addEventListener('click', function () {
            const sizeValue = parseInt(this.dataset.size);
            const sizeLabel = this.dataset.sizeLabel;
            const stockCount = parseInt(this.dataset.stock);
            const variantId = parseInt(this.dataset.variantId);

            selectSize(this, sizeValue, sizeLabel, stockCount, variantId);
        });
    });

    // Attach click handler to Add to Cart button
    addToCartBtn.addEventListener('click', function () {
        addToCart();
    });
});

function selectSize(button, sizeValue, sizeLabel, stockCount, variantId) {
    console.log('Selected Size:', sizeLabel, 'Stock:', stockCount, 'VariantId:', variantId);

    // Clear previous selection
    document.querySelectorAll('.size-button').forEach(btn => btn.classList.remove('selected'));

    // Highlight selected button
    button.classList.add('selected');

    // Store selected values
    selectedSizeValue = sizeValue;
    selectedVariantId = variantId;

    // Update hidden form fields
    document.getElementById('selectedSize').value = sizeValue;
    document.getElementById('selectedVariantId').value = variantId;

    // Enable Add to Cart button
    const addToCartBtn = document.getElementById('addToCartBtn');
    addToCartBtn.disabled = false;

    // Update button text with stock info
    if (stockCount > 0 && stockCount <= 5) {
        addToCartBtn.textContent = `Add US ${sizeValue} to cart (${stockCount} left)`;
    } else {
        addToCartBtn.textContent = `Add US ${sizeValue} to cart`;
    }
}

function addToCart() {
    // Check stock availability before submitting
    fetch(`/Products/CheckSizeAvailability?productId=${productId}&size=${selectedSizeValue}`)
        .then(response => {
            if (!response.ok) throw new Error('Network response was not ok');
            return response.json();
        })
        .then(data => {
            if (data.available && data.stock > 0) {
                console.log('Stock confirmed, submitting form...');
                document.getElementById('addToCartForm').submit();
            } else {
                alert('Sorry, this size is no longer available.');
                location.reload();
            }
        })
        .catch(error => {
            console.error('Error checking availability:', error);
            // Fallback: Submit the form anyway
            document.getElementById('addToCartForm').submit();
        });
}

function updateSizeAvailability() {
    fetch(`/Products/GetAvailableSizes?productId=${productId}`)
        .then(response => {
            if (!response.ok) throw new Error('Network response was not ok');
            return response.json();
        })
        .then(sizes => {
            console.log('Available sizes:', sizes);

            document.querySelectorAll('.size-button').forEach(button => {
                const size = parseInt(button.dataset.size);
                const sizeData = sizes.find(s => s.sizeValue === size);

                if (sizeData && sizeData.available) {
                    button.disabled = false;
                    button.classList.remove('disabled');
                    button.dataset.stock = sizeData.stock;

                    // Update stock indicator
                    const stockIndicator = button.querySelector('.stock-indicator');
                    if (stockIndicator) {
                        stockIndicator.textContent = `(${sizeData.stock} left)`;
                    } else if (sizeData.stock <= 5) {
                        button.innerHTML += `<small class="stock-indicator">(${sizeData.stock} left)</small>`;
                    }
                } else {
                    button.disabled = true;
                    button.classList.add('disabled');
                    button.classList.remove('selected');

                    // Remove stock indicator if exists
                    const stockIndicator = button.querySelector('.stock-indicator');
                    if (stockIndicator) stockIndicator.remove();
                }
            });
        })
        .catch(error => {
            console.error('Error loading size availability:', error);
        });
}

//** ====== JS of Animation and transition for Contact page ====== **//
// Contact Form Submission
document.getElementById('contactForm').addEventListener('submit', function(e) {
    e.preventDefault();

    const form = this;
    const submitBtn = document.getElementById('submitBtn');
    const btnText = submitBtn.querySelector('.btn-text');
    const btnLoading = submitBtn.querySelector('.btn-loading');
    const btnSuccess = submitBtn.querySelector('.btn-success');
    const successAlert = document.getElementById('successAlert');

    // Show loading state
    btnText.classList.add('d-none');
    btnLoading.classList.remove('d-none');
    submitBtn.disabled = true;

    // Simulate form submission
    setTimeout(() => {
        // Show success state on button
        btnLoading.classList.add('d-none');
        btnSuccess.classList.remove('d-none');
        submitBtn.classList.add('btn-success-state');

        // Show floating success alert with animation
        successAlert.style.display = 'block';

        // Auto-hide success alert after 1 seconds
        setTimeout(() => {
            successAlert.style.display = 'none';
        }, 1000);

        // Reset button state after 2 seconds
        setTimeout(() => {
            btnSuccess.classList.add('d-none');
            btnText.classList.remove('d-none');
            submitBtn.classList.remove('btn-success-state');
            submitBtn.disabled = false;

            // Reset form
            form.reset();
        }, 2000);

    }, 1500);
});

// Newsletter Form Submission
document.getElementById('newsletterForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const emailInput = this.querySelector('.newsletter-input');
    const submitBtn = this.querySelector('.btn-newsletter');
    const originalText = submitBtn.innerHTML;

    // Show loading state
    if (submitBtn.disabled) return; // this will prevent from multiple clicks of a button
    submitBtn.disabled = true;
    submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Subscribing...';

    // Simulate subscription
    setTimeout(() => {
        submitBtn.innerHTML = '<i class="bi bi-check-circle me-2"></i>Subscribed!';
        submitBtn.style.background = '#28a745';

        // Reset loading after 2 seconds
        setTimeout(() => {
            submitBtn.innerHTML = originalText;
            submitBtn.style.background = '';
            submitBtn.disabled = false;
            emailInput.value = '';
        }, 2000);

    }, 1500);
});

// Smooth hover effects for contact cards
document.querySelectorAll('.contact-card').forEach(card => {
    card.addEventListener('mouseenter', function() {
        this.style.transform = 'translateY(-10px)';
    });

    card.addEventListener('mouseleave', function() {
        this.style.transform = 'translateY(0)';
    });
});