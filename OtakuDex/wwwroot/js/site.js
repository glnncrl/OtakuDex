document.addEventListener("DOMContentLoaded", function () {
    const themeToggleBtn = document.getElementById("themeToggle");
    const htmlElement = document.documentElement;

    if (themeToggleBtn) {
        themeToggleBtn.addEventListener("click", function () {
            // Kunin ang current theme
            let currentTheme = htmlElement.getAttribute("data-theme");

            // Mag-switch: Kung dark, gawing light. Kung light, gawing dark.
            let newTheme = currentTheme === "dark" ? "light" : "dark";

            // I-apply ang bagong theme sa HTML tag
            htmlElement.setAttribute("data-theme", newTheme);

            // I-save sa browser localStorage para hindi mawala pag nag-refresh
            localStorage.setItem("theme", newTheme);
        });
    }
});