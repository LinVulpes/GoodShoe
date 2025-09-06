// Developer Card Hover Enhancement for Thet Paing Hmu - Desktop Only
document.addEventListener('DOMContentLoaded', function() {

    // Find Thet Paing Hmu's card and enhance it
    const cards = document.querySelectorAll('.card');
    let thetCard = null;

    cards.forEach(card => {
        const nameElement = card.querySelector('.name');
        if (nameElement && nameElement.textContent.includes('Thet Paing Hmu')) {
            thetCard = card;
        }
    });

    if (thetCard) {
        // Add special class to Thet's card
        thetCard.classList.add('special-card');

        // Create the dropdown overlay
        const dropdown = document.createElement('div');
        dropdown.className = 'card-dropdown';
        dropdown.innerHTML = `
            <div class="dropdown-section">
                <div class="dropdown-title">Additional Roles</div>
                <ul class="role-list">
                    <li>UI/UX Designer</li>
                    <li>Frontend Architect</li>
                    <li>Full Stack Developer</li>
                    <li>Project Manager</li>
                    <li>Quality Assurance Lead</li>
                </ul>
            </div>
            
            <div class="dropdown-divider"></div>
            
            <div class="dropdown-section">
                <div class="dropdown-title">Key Skills</div>
                <div class="skills-grid">
                    <div class="skill-tag">ASP.NET</div>
                    <div class="skill-tag">C#</div>
                    <div class="skill-tag">JavaScript</div>
                    <div class="skill-tag">Bootstrap</div>
                    <div class="skill-tag">SQL</div>
                    <div class="skill-tag">Git</div>
                </div>
            </div>
            
            <div class="dropdown-divider"></div>
            
            <div class="dropdown-section">
                <div class="dropdown-title">Project Impact</div>
                <div class="dropdown-content">
                    Led GoodShoe development, designed user interfaces, and coordinated team efforts to deliver a professional e-commerce platform.
                </div>
            </div>
        `;

        // Insert dropdown before the card-content
        const cardContent = thetCard.querySelector('.card-content');
        thetCard.insertBefore(dropdown, cardContent);

        // Enhanced hover effects for desktop
        thetCard.addEventListener('mouseenter', function() {
            this.style.zIndex = '10';
        });

        thetCard.addEventListener('mouseleave', function() {
            this.style.zIndex = '1';
        });

        console.log('Enhanced developer card loaded for Thet Paing Hmu');
    } else {
        console.warn('Thet Paing Hmu card not found');
    }
});