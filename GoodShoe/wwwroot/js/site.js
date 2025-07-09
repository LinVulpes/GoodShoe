function redirectToProduct(url) {
    window.location.href = url;
}

// Remove focus from dropdown buttons after click
 document.addEventListener('DOMContentLoaded', function() {
     const dropdownButtons = document.querySelectorAll('.dropdown-toggle');
     dropdownButtons.forEach(button => {
         button.addEventListener('click', function() {
             // Remove focus after a short delay
             setTimeout(() => {
                 this.blur();
             }, 150);
         });
     });

     // Remove focus when dropdown closes
     const dropdowns = document.querySelectorAll('.dropdown');
     dropdowns.forEach(dropdown => {
         dropdown.addEventListener('hidden.bs.dropdown', function() {
             const button = this.querySelector('.dropdown-toggle');
             if (button) {
                 button.blur();
             }
         });
     });
 });

//** JS for Product details **//

let selectedSize = null;
let selectedSizeLabel = null;

// Enhanced selectSize function with debugging
function selectSize(button, size, sizeLabel) {
    console.log(`selectSize called: size=${size}, sizeLabel=${sizeLabel}`);
    console.log('Button element:', button);
    console.log('Button classes:', button.className);

    if (button.classList.contains('disabled') || button.classList.contains('loading')) {
        console.log('Button is disabled or loading - ignoring click');
        return;
    }

    // To unselect the clicked button
    if (button.classList.contains('selected')) {
        button.classList.remove('selected');
        selectedSize = null;
        selectedSizeLabel = null;

        // Disable the add to cart button
        const addToCartBtn = document.getElementById('addToCartBtn');
        if (addToCartBtn) addToCartBtn.disabled = true;

        console.log('Size unselected');
        return;
    }

    // Remove selection from all buttons
    document.querySelectorAll('.size-button').forEach(b => {
        b.classList.remove('selected');
        console.log('Removed selected from button:', b.textContent.trim());
    });

    // Add selection to clicked button
    button.classList.add('selected');
    console.log('Added selected to button:', button.textContent.trim());

    // Store selected size info
    selectedSize = size;
    selectedSizeLabel = sizeLabel;

    // Enable add to cart button
    const addToCartBtn = document.getElementById('addToCartBtn');
    addToCartBtn.disabled = false;

    console.log(`Selected size: ${sizeLabel}`);
}

function addToCart() {
    console.log('addToCart called');
    if (!selectedSize || !selectedSizeLabel) {
        alert('Please select a size first.');
        return;
    }

    // Set the selected size in the hidden input
    document.getElementById('selectedSize').value = selectedSizeLabel;
    console.log('Set hidden input value to:', selectedSizeLabel);

    // Submit the form
    document.getElementById('addToCartForm').submit();
}

//** Just Some animation & transition for Contact Page **//
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