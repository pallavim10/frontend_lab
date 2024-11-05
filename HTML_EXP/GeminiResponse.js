
/*
const usernameInput = document.getElementById('username');
const passwordInput = document.getElementById('password');
const loginBtn = document.getElementById('login-btn');
const ftueModal = document.getElementById('ftue-modal');
const nextBtn = document.getElementById('next-btn');

// Function to show the FTUE modal
function showFTUE() {
    ftueModal.classList.add('active');
}

// Function to hide the FTUE modal
function hideFTUE() {
    ftueModal.classList.remove('active');
}

// Event listener for the next button in the FTUE modal
nextBtn.addEventListener('click', () => {
    hideFTUE();
    usernameInput.focus();
});

// Event listener for the login button
loginBtn.addEventListener('click', () => {
    // Your login logic here
});

// Show the FTUE modal on page load
window.addEventListener('load', showFTUE);
*/

const usernameInput = document.getElementById('username');
const passwordInput = document.getElementById('password');
const loginBtn = document.getElementById('login-btn');
const usernameHelp = document.getElementById('username-help');

// Function to show FTUE with Bootstrap modal
function showFTUE() {
  const myModal = new bootstrap.Modal(document.getElementById('ftueModal'));
  myModal.show();
}

// Event listener for login button
loginBtn.addEventListener('click', () => {
  // Your login logic here
});

// Show FTUE modal on page load (optional)
 window.addEventListener('load', showFTUE);

