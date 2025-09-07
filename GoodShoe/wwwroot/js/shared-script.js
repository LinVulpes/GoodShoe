// Shared JavaScript for GoodShoe FAQ, Privacy, and Terms Pages

document.addEventListener('DOMContentLoaded', function() {

    // Set last updated date for all pages
    const lastUpdatedElement = document.getElementById('lastUpdated');
    if (lastUpdatedElement) {
        lastUpdatedElement.textContent = new Date().toLocaleDateString('en-SG', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    }

    // FAQ Page Functionality
    const faqQuestions = document.querySelectorAll('.faq-question');
    const categoryTabs = document.querySelectorAll('.category-tab');
    const searchInput = document.getElementById('faqSearch');
    const faqItems = document.querySelectorAll('.faq-item');
    const noResults = document.querySelector('.no-results');

    // FAQ - Toggle FAQ answers
    if (faqQuestions.length > 0) {
        faqQuestions.forEach(question => {
            question.addEventListener('click', function() {
                const answer = this.nextElementSibling;
                const isActive = this.classList.contains('active');

                // Close all other FAQ items
                faqQuestions.forEach(q => {
                    q.classList.remove('active');
                    q.nextElementSibling.classList.remove('active');
                });

                // Toggle current item
                if (!isActive) {
                    this.classList.add('active');
                    answer.classList.add('active');
                }
            });
        });
    }

    // FAQ - Category filtering
    if (categoryTabs.length > 0) {
        categoryTabs.forEach(tab => {
            tab.addEventListener('click', function() {
                const category = this.getAttribute('data-category');

                // Update active tab
                categoryTabs.forEach(t => t.classList.remove('active'));
                this.classList.add('active');

                // Filter FAQ items
                filterFAQs(category, searchInput ? searchInput.value : '');
            });
        });
    }

    // FAQ - Search functionality
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const activeCategory = document.querySelector('.category-tab.active').getAttribute('data-category');
            filterFAQs(activeCategory, this.value);
        });
    }

    // FAQ - Filter function
    function filterFAQs(category, searchTerm) {
        let visibleCount = 0;

        if (faqItems.length > 0) {
            faqItems.forEach(item => {
                const itemCategory = item.getAttribute('data-category');
                const questionText = item.querySelector('.faq-question span').textContent.toLowerCase();
                const answerText = item.querySelector('.faq-answer').textContent.toLowerCase();
                const searchMatch = questionText.includes(searchTerm.toLowerCase()) ||
                    answerText.includes(searchTerm.toLowerCase());

                const categoryMatch = category === 'all' || itemCategory === category;

                if (categoryMatch && (searchTerm === '' || searchMatch)) {
                    item.style.display = 'block';
                    visibleCount++;
                } else {
                    item.style.display = 'none';
                    // Close if it was open
                    item.querySelector('.faq-question').classList.remove('active');
                    item.querySelector('.faq-answer').classList.remove('active');
                }
            });

            // Show/hide no results message
            if (noResults) {
                noResults.style.display = visibleCount === 0 ? 'block' : 'none';
            }
        }
    }

    // Terms Page - Smooth scrolling for table of contents links
    const tocLinks = document.querySelectorAll('.table-of-contents a[href^="#"]');
    if (tocLinks.length > 0) {
        tocLinks.forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                e.preventDefault();
                const target = document.querySelector(this.getAttribute('href'));
                if (target) {
                    target.scrollIntoView({
                        behavior: 'smooth',
                        block: 'start'
                    });
                }
            });
        });
    }

    // Terms Page - Scroll to top functionality
    const scrollToTopBtn = document.getElementById('scrollToTop');
    if (scrollToTopBtn) {
        window.addEventListener('scroll', function() {
            if (window.pageYOffset > 300) {
                scrollToTopBtn.classList.add('visible');
            } else {
                scrollToTopBtn.classList.remove('visible');
            }
        });

        scrollToTopBtn.addEventListener('click', function() {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
    }

    // Terms Page - Highlight current section in table of contents
    const sections = document.querySelectorAll('.section');
    const allTocLinks = document.querySelectorAll('.table-of-contents a');

    if (sections.length > 0 && allTocLinks.length > 0) {
        function highlightCurrentSection() {
            let current = '';
            sections.forEach(section => {
                const sectionTop = section.offsetTop;
                if (window.pageYOffset >= sectionTop - 150) {
                    current = section.getAttribute('id');
                }
            });

            allTocLinks.forEach(link => {
                link.style.fontWeight = 'normal';
                if (link.getAttribute('href') === '#' + current) {
                    link.style.fontWeight = 'bold';
                }
            });
        }

        window.addEventListener('scroll', highlightCurrentSection);
    }

    // Privacy Page - Smooth scrolling for internal links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // Add fade-in animation on page load for all containers
    const containers = [
        document.querySelector('.faq-container'),
        document.querySelector('.privacy-container'),
        document.querySelector('.terms-container')
    ];

    containers.forEach(container => {
        if (container) {
            container.style.opacity = '0';
            container.style.transform = 'translateY(20px)';

            setTimeout(function() {
                container.style.transition = 'all 0.6s ease';
                container.style.opacity = '1';
                container.style.transform = 'translateY(0)';
            }, 100);
        }
    });
});