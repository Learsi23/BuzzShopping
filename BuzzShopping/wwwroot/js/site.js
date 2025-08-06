//-------------------------------------------------------
//-------------------HEADER------------------------------
//-------------------------------------------------------
document.addEventListener('DOMContentLoaded', function () {
    // Mobile menu toggle
    const hamburgerButton = document.querySelector('.hamburger-button');
    const mobileMenu = document.getElementById('mobile-menu');
    const hamburgerIcon = document.querySelector('.hamburger-icon');
    const closeIcon = document.querySelector('.close-icon');

    if (hamburgerButton && mobileMenu) {
        hamburgerButton.addEventListener('click', function (e) {
            e.stopPropagation();

            // Toggle aria-expanded
            const isExpanded = this.getAttribute('aria-expanded') === 'true';
            this.setAttribute('aria-expanded', !isExpanded);

            // Toggle menu visibility
            mobileMenu.classList.toggle('is-open');

            // Toggle icons
            if (hamburgerIcon && closeIcon) {
                hamburgerIcon.classList.toggle('hidden');
                closeIcon.classList.toggle('hidden');
            }
        });

        // Close menu when clicking outside
        document.addEventListener('click', function (e) {
            if (!mobileMenu.contains(e.target) &&
                !hamburgerButton.contains(e.target) &&
                mobileMenu.classList.contains('is-open')) {

                hamburgerButton.setAttribute('aria-expanded', 'false');
                mobileMenu.classList.remove('is-open');

                if (hamburgerIcon && closeIcon) {
                    hamburgerIcon.classList.remove('hidden');
                    closeIcon.classList.add('hidden');
                }
            }
        });
    }

    // User dropdown for desktop
    const userButton = document.querySelector('.user-button');
    const userDropdown = document.querySelector('.user-dropdown');

    if (userButton && userDropdown) {
        userButton.addEventListener('click', function (e) {
            e.stopPropagation();
            const isVisible = userDropdown.style.display === 'block';
            userDropdown.style.display = isVisible ? 'none' : 'block';
        });

        // Close dropdown when clicking outside
        document.addEventListener('click', function () {
            if (userDropdown && userDropdown.style.display === 'block') {
                userDropdown.style.display = 'none';
            }
        });
    }
});