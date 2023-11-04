const optionalFields = document.querySelectorAll('.modal .optional-fields')

optionalFields.forEach((element) => {
    element.addEventListener('click', () => {
        element.classList.toggle('minimized')
    });
})