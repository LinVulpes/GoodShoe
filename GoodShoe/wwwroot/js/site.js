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