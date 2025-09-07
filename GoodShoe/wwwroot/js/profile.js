// Profile JavaScript - for both Index.cshtml and Edit.cshtml

// Common DOM Content Loaded Event
document.addEventListener('DOMContentLoaded', function() {
    initializeProfilePage();
    initializeEditPage();
    initializeDeleteModal();
});

// Initialize Profile Page (Index.cshtml) Functions
function initializeProfilePage() {
    // Only run if we're on the profile page
    if (!document.getElementById('passwordModal')) return;

    // Initialize delete confirmation input listener
    const deleteInput = document.getElementById('deleteConfirmation');
    if (deleteInput) {
        deleteInput.addEventListener('input', updateDeleteButton);
        deleteInput.addEventListener('keyup', function(e) {
            if (e.key === 'Enter') {
                finalDeleteAccount();
            }
        });
    }
}

// Initialize Edit Page (Edit.cshtml) Functions
function initializeEditPage() {
    // Only run if we're on the edit page
    const firstInput = document.querySelector('.form-control');
    if (!firstInput) return;

    // Focus on first input field
    firstInput.focus();

    // Add real-time validation feedback
    const inputs = document.querySelectorAll('.form-control');
    inputs.forEach(function(input) {
        input.addEventListener('input', function() {
            // Remove error styling when user starts typing
            this.style.borderColor = '#d4c4a8';

            // Find associated error span
            const errorSpan = this.parentNode.querySelector('.validation-error');
            if (errorSpan && this.value.trim()) {
                errorSpan.style.display = 'none';
            }
        });

        input.addEventListener('blur', function() {
            // Basic validation on blur (server will handle email validation)
            if (this.hasAttribute('required')) {
                if (!this.value.trim()) {
                    this.style.borderColor = '#d54335';
                } else {
                    this.style.borderColor = '#b3a085';
                }
            }
        });
    });

    // Form submission validation
    const form = document.querySelector('form');
    if (form) {
        form.addEventListener('submit', function(e) {
            let hasErrors = false;

            // Check required fields only (let server handle email validation)
            const requiredInputs = form.querySelectorAll('input[data-val-required]');
            requiredInputs.forEach(function(input) {
                if (!input.value.trim()) {
                    input.style.borderColor = '#d54335';
                    hasErrors = true;
                }
            });

            if (hasErrors) {
                e.preventDefault();

                // Scroll to first error
                const firstError = form.querySelector('input[style*="border-color: rgb(213, 67, 53)"]');
                if (firstError) {
                    firstError.scrollIntoView({ behavior: 'smooth', block: 'center' });
                    firstError.focus();
                }
            } else {
                // Show loading state on submit button
                const submitBtn = form.querySelector('button[type="submit"]');
                if (submitBtn) {
                    submitBtn.innerHTML = 'Saving Changes...';
                    submitBtn.style.opacity = '0.7';
                    submitBtn.disabled = true;
                }
            }
        });
    }
}

// Password Modal Functions
function openPasswordModal() {
    const modal = document.getElementById('passwordModal');
    if (modal) {
        modal.style.display = 'block';
    }
}

function closePasswordModal() {
    const modal = document.getElementById('passwordModal');
    const form = document.getElementById('passwordForm');
    if (modal) {
        modal.style.display = 'none';
    }
    if (form) {
        form.reset();
    }
}

// Delete Account Functions
function confirmDelete() {
    const modal = document.getElementById('deleteModal');
    if (modal) {
        modal.style.display = 'block';
    }
}

function closeDeleteModal() {
    const modal = document.getElementById('deleteModal');
    const input = document.getElementById('deleteConfirmation');
    if (modal) {
        modal.style.display = 'none';
    }
    if (input) {
        input.value = '';
        updateDeleteButton();
    }
}

function updateDeleteButton() {
    const input = document.getElementById('deleteConfirmation');
    const button = document.getElementById('confirmDeleteBtn');
    const requiredText = 'DELETE MY ACCOUNT';

    if (input && button) {
        if (input.value.trim().toUpperCase() === requiredText) {
            button.classList.add('enabled');
        } else {
            button.classList.remove('enabled');
        }
    }
}

function finalDeleteAccount() {
    const input = document.getElementById('deleteConfirmation');
    const requiredText = 'DELETE MY ACCOUNT';

    if (!input) return;

    if (input.value.trim().toUpperCase() === requiredText) {
        const button = document.getElementById('confirmDeleteBtn');
        if (button) {
            button.innerHTML = 'Processing Deletion...';
            button.style.opacity = '0.7';
            button.style.pointerEvents = 'none';
        }

        // Create and submit form
        const form = document.createElement('form');
        form.method = 'POST';

        // Get the delete action URL from the page
        const deleteUrl = getDeleteAccountUrl();
        form.action = deleteUrl;

        // Add anti-forgery token
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        if (token) {
            const hiddenToken = document.createElement('input');
            hiddenToken.type = 'hidden';
            hiddenToken.name = '__RequestVerificationToken';
            hiddenToken.value = token.value;
            form.appendChild(hiddenToken);
        }

        document.body.appendChild(form);
        form.submit();
    } else {
        alert('Please type "DELETE MY ACCOUNT" exactly as shown to confirm deletion.');
        input.focus();
    }
}

function getDeleteAccountUrl() {
    // Try to get the URL from a data attribute or construct it
    // This assumes the standard ASP.NET MVC routing
    const baseUrl = window.location.origin;
    return baseUrl + '/Profile/DeleteAccount';
}

function initializeDeleteModal() {
    // Initialize delete confirmation functionality
    const deleteInput = document.getElementById('deleteConfirmation');
    if (deleteInput) {
        deleteInput.addEventListener('input', updateDeleteButton);
        deleteInput.addEventListener('keyup', function(e) {
            if (e.key === 'Enter') {
                finalDeleteAccount();
            }
        });
    }
}

// Password Form Validation
document.addEventListener('DOMContentLoaded', function() {
    const passwordForm = document.getElementById('passwordForm');
    if (passwordForm) {
        passwordForm.addEventListener('submit', function(e) {
            const newPassword = document.getElementById('newPassword');
            const confirmPassword = document.getElementById('confirmPassword');

            if (newPassword && confirmPassword) {
                if (newPassword.value !== confirmPassword.value) {
                    e.preventDefault();
                    alert('New password and confirm password do not match!');
                    return false;
                }

                if (newPassword.value.length < 6) {
                    e.preventDefault();
                    alert('Password must be at least 6 characters long!');
                    return false;
                }
            }
        });
    }
});

// Modal Click Outside to Close
window.onclick = function(event) {
    const passwordModal = document.getElementById('passwordModal');
    const deleteModal = document.getElementById('deleteModal');

    if (passwordModal && event.target == passwordModal) {
        closePasswordModal();
    }
    if (deleteModal && event.target == deleteModal) {
        closeDeleteModal();
    }
}