// document.addEventListener("DOMContentLoaded", function() {
  // // Select elements
  // const startButton = document.getElementById("startButton");
  // const toastMessage = document.getElementById("toastMessage");

  // // Add event listener to startButton
  // startButton.addEventListener("click", function() {
    // // Show Bootstrap toast
    // var toast = new bootstrap.Toast(toastMessage);
    // toast.show();
  // });
// });

document.addEventListener("DOMContentLoaded", function() {
  // Select elements
  const startButton = document.getElementById("startButton");
  const nextButton = document.getElementById("nextButton");
  const nameInput = document.getElementById("nameInput");
  const emailInput = document.getElementById("emailInput");
  const toastMessage = document.getElementById("toastMessage");

  // Add event listener to startButton
  startButton.addEventListener("click", function() {
    showToast("Welcome to the Homepage! You can start by filling out the form.");
  });

  // Add event listener to nextButton
  nextButton.addEventListener("click", function() {
    // Validate inputs
    if (nameInput.value === "" || emailInput.value === "") {
      showToast("Please fill out all the fields before proceeding.");
    } else {
      showToast("Form submission successful! You can continue navigating.");
      // Reset form
      document.getElementById("exampleForm").reset();
    }
  });

  // Add event listeners to navigation components
  const navLinks = document.querySelectorAll(".navbar-nav .nav-link");
  navLinks.forEach(function(navLink) {
    navLink.addEventListener("click", function() {
      showToast("You clicked on a navigation link.");
    });
  });

  // Function to show Bootstrap toast
  function showToast(message) {
    var toast = new bootstrap.Toast(toastMessage);
    toast.show();
    // Update toast message
    document.querySelector(".toast-body").textContent = message;
  }
});
