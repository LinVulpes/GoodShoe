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

let selectedSizeInfo = null;

function selectSize(button, size, sizeLabel, stockCount, variantId) {
    // Don't allow selection if out of stock
    if (stockCount <= 0) {
        console.log('Size out of stock:', size);
        return;
    }

    // Remove selection from all other buttons
    document.querySelectorAll('.size-button').forEach(btn => {
        btn.classList.remove('selected');
    });

    // Add selection to current button
    button.classList.add('selected');

    // Store selected size information
    selectedSizeInfo = {
        size: size,
        sizeLabel: sizeLabel,
        stockCount: stockCount,
        variantId: variantId
    };

    // Update hidden form fields
    document.getElementById('selectedSize').value = size;
    document.getElementById('selectedVariantId').value = variantId;

    // Enable add to cart button
    const addToCartBtn = document.getElementById('addToCartBtn');
    addToCartBtn.disabled = false;
    addToCartBtn.textContent = `Add to cart - ${sizeLabel}`;

    console.log('Selected size:', selectedSizeInfo);
}

function addToCart() {
    if (!selectedSizeInfo) {
        alert('Please select a size first.');
        return;
    }

    if (selectedSizeInfo.stockCount <= 0) {
        alert('Sorry, this size is out of stock.');
        return;
    }

    // Show loading state
    const addToCartBtn = document.getElementById('addToCartBtn');
    const originalText = addToCartBtn.textContent;
    addToCartBtn.disabled = true;
    addToCartBtn.innerHTML = '<i class="spinner-border spinner-border-sm me-2"></i>Adding...';

    // Submit the form
    document.getElementById('addToCartForm').submit();
}

// Enhanced real-time stock checking with out-of-stock styling
function updateSizeAvailability() {
    if (typeof productId === 'undefined') {
        console.warn('productId not defined');
        return;
    }

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

                // Remove all previous state classes
                button.classList.remove('disabled', 'low-stock');

                // Remove any stock indicators
                let stockIndicator = button.querySelector('.stock-indicator');
                if (stockIndicator) {
                    stockIndicator.remove();
                }

                if (sizeData && sizeData.stock > 0) {
                    // Size is available
                    button.disabled = false;
                    button.dataset.stock = sizeData.stock;
                    button.dataset.variantId = sizeData.variantId || button.dataset.variantId;

                    // Remove any existing stock indicators
                    let stockIndicator = button.querySelector('.stock-indicator');
                    if (stockIndicator) {
                        stockIndicator.remove();
                    }
                } else {
                    // Size is out of stock
                    button.disabled = true;
                    button.classList.add('disabled', 'out-of-stock');
                    button.dataset.stock = '0';

                    // Remove selection if this was the selected size
                    if (button.classList.contains('selected')) {
                        button.classList.remove('selected');
                        selectedSizeInfo = null;

                        // Reset form and button state
                        const addToCartBtn = document.getElementById('addToCartBtn');
                        if (addToCartBtn) {
                            addToCartBtn.disabled = true;
                            addToCartBtn.textContent = 'Add to cart';
                        }

                        const selectedSizeInput = document.getElementById('selectedSize');
                        const selectedVariantInput = document.getElementById('selectedVariantId');
                        if (selectedSizeInput) selectedSizeInput.value = '';
                        if (selectedVariantInput) selectedVariantInput.value = '';
                    }

                    // Remove any existing stock indicators
                    let stockIndicator = button.querySelector('.stock-indicator');
                    if (stockIndicator) {
                        stockIndicator.remove();
                    }
                }
            });
        })
        .catch(error => {
            console.error('Error loading size availability:', error);
        });
}


//** ====== JS of Animation and transition for Contact page ====== **//
// Contact Form Submission
document.addEventListener('DOMContentLoaded', function() {
    const contactForm = document.getElementById('contactForm');
    if (contactForm) {
        contactForm.addEventListener('submit', function(e) {
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
    }
});

// Newsletter Form Submission
document.addEventListener('DOMContentLoaded', function() {
    const newsletterForm = document.getElementById('newsletterForm');
    if (newsletterForm) {
        newsletterForm.addEventListener('submit', function (e) {
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
    }
});

// Smooth hover effects for contact cards
document.addEventListener('DOMContentLoaded', function() {
    const contactCards = document.querySelectorAll('.contact-card');
    contactCards.forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-10px)';
        });

        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });
});