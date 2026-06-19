document.addEventListener("DOMContentLoaded", function () {

    const themeToggleBtn = document.getElementById("themeToggle");
    const themeToggleBtnDesktop = document.getElementById("themeToggleDesktop");

    const themeIcon = document.getElementById("themeIcon");
    const themeIconDesktop = document.getElementById("themeIconDesktop");

    const htmlElement = document.documentElement;

    function getTheme() {
        return localStorage.getItem("theme") ||
            htmlElement.getAttribute("data-theme") ||
            "light";
    }

    function applyTheme(theme) {
        htmlElement.setAttribute("data-theme", theme);
        localStorage.setItem("theme", theme);

        const icon = theme === "dark" ? "☀️" : "🌙";

        if (themeIcon) themeIcon.textContent = icon;
        if (themeIconDesktop) themeIconDesktop.textContent = icon;
    }

    function toggleTheme() {
        const currentTheme = getTheme();
        const newTheme = currentTheme === "dark" ? "light" : "dark";
        applyTheme(newTheme);
    }

    // INIT (VERY IMPORTANT)
    applyTheme(getTheme());

    if (themeToggleBtn) {
        themeToggleBtn.addEventListener("click", toggleTheme);
    }

    if (themeToggleBtnDesktop) {
        themeToggleBtnDesktop.addEventListener("click", toggleTheme);
    }
});