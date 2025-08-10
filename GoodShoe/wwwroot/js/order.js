// only numeric input for Contact Number
const contactInput = document.querySelector('input[name="ContactNumber"]');
if (contactInput) {
    contactInput.addEventListener('input', function () {
        this.value = this.value.replace(/\D/g, '').slice(0, 11);
    });
}

// Payment method selection
document.querySelectorAll('.payment-btn').forEach(button => {
    button.addEventListener('click', function () {
        document.querySelectorAll('.payment-btn').forEach(btn => btn.classList.remove('active'));
        this.classList.add('active');
        const selectedMethod = this.getAttribute('data-method');
        document.querySelector('input[name="PaymentMethod"]').value = selectedMethod;
    });
});

// only numeric input for Card Number (max 12 digits)
const cardNumberInput = document.querySelector('input[name="CardNumber"]');
if (cardNumberInput) {
    cardNumberInput.addEventListener('input', function () {
        let digits = this.value.replace(/\D/g, '').slice(0, 16); // Max 16 digits
        this.value = digits.replace(/(.{4})/g, '$1 ').trim(); // Format: 1234 5678 9012 3456
    });
}

// only numeric input for CVV (max 3 digits)
const cvvInput = document.querySelector('input[name="CVV"]');
if (cvvInput) {
    cvvInput.addEventListener('input', function () {
        this.value = this.value.replace(/\D/g, '').slice(0, 3);
    });
}

// card expiry date MM/YY and auto show slash '/'
const expiryInput = document.querySelector('input[name="ExpiryDate"]');
if (expiryInput) {
    expiryInput.addEventListener('input', function () {
        let value = this.value.replace(/\D/g, '');

        if (value.length > 2) {
            value = value.slice(0, 2) + '/' + value.slice(2, 4);
        }

        this.value = value.slice(0, 5); // Max length "MM/YY"
    });
}