// ADMIN JAVASCRIPT
document.addEventListener('DOMContentLoaded', function() {
    // Handle size selection
    const sizeCheckboxes = document.querySelectorAll('input[name="selectedSizes"]');
    const hiddenInput = document.getElementById('availableSizesHidden');

    function updateSizes() {
        const selectedSizes = Array.from(sizeCheckboxes)
            .filter(cb => cb.checked)
            .map(cb => cb.value);
        hiddenInput.value = selectedSizes.join(',');
    }

    sizeCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', updateSizes);
    });

    // Handle image preview
    const imageUrlInput = document.getElementById('imageUrlInput');
    const imagePreview = document.getElementById('imagePreview');

    imageUrlInput.addEventListener('input', function() {
        if (this.value.trim()) {
            imagePreview.src = this.value;
            imagePreview.style.display = 'block';
            imagePreview.onerror = function() {
                this.style.display = 'none';
            };
        } else {
            imagePreview.style.display = 'none';
        }
    });

    // Form validation
    const form = document.getElementById('createForm');
    form.addEventListener('submit', function(e) {
        const name = document.querySelector('input[name="Name"]').value.trim();
        const price = document.querySelector('input[name="Price"]').value;
        const stock = document.querySelector('input[name="Stock"]').value;

        if (!name) {
            alert('Please enter a product name');
            e.preventDefault();
            return;
        }

        if (!price || price <= 0) {
            alert('Please enter a valid price');
            e.preventDefault();
            return;
        }

        if (!stock || stock < 0) {
            alert('Please enter a valid stock quantity');
            e.preventDefault();
            return;
        }

        // Update sizes before submit
        updateSizes();
    });
});