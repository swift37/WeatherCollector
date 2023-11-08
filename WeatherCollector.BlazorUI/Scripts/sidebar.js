const body = document.querySelector('body'),
    sidebar = body.querySelector('.sidebar'),
    toggle = body.querySelector('.toggle'),
    modeSwitch = body.querySelector('.toggle-switch'),
    modeState = body.querySelector('.mode-state');

toggle.addEventListener('click', () => {
    sidebar.classList.toggle('minimized')
});

modeSwitch.addEventListener('click', () => {
    body.classList.toggle('night')

    body.classList.contains('night') ?
        modeState.textContent = 'Light mode' :
        modeState.textContent = 'Night mode'
});