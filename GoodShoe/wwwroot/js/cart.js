// Store stock information for each cart item
let stockInfo = {};

// Load stock information when page loads
document.addEventListener('DOMContentLoaded', function () {
    loadStockInformation();

    // Add event listener to plus buttons to check stock before allowing form submission
    document.querySelectorAll('.plus-form').forEach(form => {
        form.addEventListener('submit', function (e) {
            const productId = this.getAttribute('data-product-id');
            const size = this.getAttribute('data-size');
            const key = `${productId}-${size}`;

            if (stockInfo[key] && !stockInfo[key].canIncrease) {
                e.preventDefault();
                showStockAlert(productId, size);
                return false;
            }
        });
    });
});

async function loadStockInformation() {
    const cartItems = document.querySelectorAll('.cart-product-card');

    for (let item of cartItems) {
        const productId = item.getAttribute('data-product-id');
        const size = item.getAttribute('data-size');

        try {
            const response = await fetch(`/Cart/GetStockInfo?productId=${productId}&size=${encodeURIComponent(size)}`);
            const data = await response.json();

            if (data.success) {
                const key = `${productId}-${size}`;
                stockInfo[key] = data;

                // Update stock info display
                const stockInfoElement = item.querySelector('.stock-info');
                if (data.availableToAdd > 0) {
                    stockInfoElement.textContent = `${data.availableToAdd} more available (${data.totalStock} total stock)`;
                    stockInfoElement.classList.remove('text-danger');
                    stockInfoElement.classList.add('text-muted');
                } else {
                    stockInfoElement.textContent = `Maximum stock reached (${data.totalStock} total)`;
                    stockInfoElement.classList.remove('text-muted');
                    stockInfoElement.classList.add('text-danger');
                }

                // Update plus button state
                const plusBtn = item.querySelector('.plus-btn');
                if (!data.canIncrease) {
                    plusBtn.disabled = true;
                    plusBtn.classList.add('disabled');
                    plusBtn.title = `Maximum stock (${data.totalStock}) reached`;
                } else {
                    plusBtn.disabled = false;
                    plusBtn.classList.remove('disabled');
                    plusBtn.title = `Add one more (${data.availableToAdd} available)`;
                }

                // DON'T disable minus button - let it work even when quantity = 1
                // This allows the item to be removed when quantity goes to 0
                const minusBtn = item.querySelector('.minus-btn');
                minusBtn.disabled = false;
                minusBtn.classList.remove('disabled');

                // Update title based on current quantity
                if (data.currentCartQuantity === 1) {
                    minusBtn.title = 'Remove item from cart';
                } else {
                    minusBtn.title = 'Decrease quantity';
                }
            }
        } catch (error) {
            console.error('Error loading stock info:', error);
            const stockInfoElement = item.querySelector('.stock-info');
            stockInfoElement.textContent = 'Stock info unavailable';
            stockInfoElement.classList.add('text-warning');
        }
    }
}

function showStockAlert(productId, size) {
    const key = `${productId}-${size}`;
    const info = stockInfo[key];

    if (info) {
        alert(`Cannot add more items. You have ${info.currentCartQuantity} items in cart out of ${info.totalStock} total stock available.`);
    } else {
        alert('Cannot add more items. Stock limit reached.');
    }
}

// Refresh stock info after any cart updates
window.addEventListener('pageshow', function (event) {
    if (event.persisted) {
        // Page was loaded from cache, refresh stock info
        setTimeout(loadStockInformation, 100);
    }
});