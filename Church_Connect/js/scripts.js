/*!
* Start Bootstrap - One Page Wonder v6.0.6 (https://startbootstrap.com/theme/one-page-wonder)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-one-page-wonder/blob/master/LICENSE)
*/
// This file is intentionally blank
// Use this file to add JavaScript to your project

document.addEventListener("DOMContentLoaded", function () {
    // Get all dropdown toggles
    var dropdownToggleList = document.querySelectorAll('.dropdown-toggle');

    // Toggle dropdown visibility
    dropdownToggleList.forEach(function (dropdownToggle) {
        dropdownToggle.addEventListener('click', function () {
            var dropdownMenu = this.nextElementSibling;
            dropdownMenu.classList.toggle('show');
        });
    });

    // Close dropdown when clicking outside
    window.addEventListener('click', function (event) {
        dropdownToggleList.forEach(function (dropdownToggle) {
            var dropdownMenu = dropdownToggle.nextElementSibling;
            if (!dropdownToggle.contains(event.target) && !dropdownMenu.contains(event.target)) {
                dropdownMenu.classList.remove('show');
            }
        });
    });
});

