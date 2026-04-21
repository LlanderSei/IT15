document.addEventListener("DOMContentLoaded", () => {
  document.querySelectorAll('[data-bs-toggle="tooltip"]').forEach((element) => {
    new bootstrap.Tooltip(element);
  });

  document.querySelectorAll("form[data-loading-form='true']").forEach((form) => {
    form.addEventListener("submit", () => {
      const button = form.querySelector("[data-loading-button='true']");
      if (!button) {
        return;
      }

      button.disabled = true;

      const idleText = button.querySelector("[data-button-text]");
      const loadingText = button.querySelector("[data-loading-text]");
      const spinner = button.querySelector(".spinner-border");

      if (idleText) {
        idleText.classList.add("d-none");
      }

      if (loadingText) {
        loadingText.classList.remove("d-none");
      }

      if (spinner) {
        spinner.classList.remove("d-none");
      }
    });
  });
});
