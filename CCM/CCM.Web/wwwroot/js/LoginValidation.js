$('#loginform').submit(function () {
    const username = $('#login-username').val();
    const password = $('#login-password').val();

    if (!username) {
        turnOnIncorrectLoginInput('#login-username', '#usernameWarning');
    }
    else {
        turnOffIncorrectLoginInput('#login-username', '#usernameWarning');
    }

    if (!password) {
        turnOnIncorrectLoginInput('#login-password', '#passwordWarning');
    }
    else {
        turnOffIncorrectLoginInput('#login-password', '#passwordWarning');
    }

    if (!username || !password) {
        event.preventDefault();
    }
});

function turnOnIncorrectLoginInput(inputName, warningMessage) {
    $(inputName).attr('class', 'form-control is-invalid');
    $(warningMessage).attr('style', 'display:block');
}

function turnOffIncorrectLoginInput(inputName, warningMessage) {
    $(inputName).attr('class', 'form-control');
    $(warningMessage).attr('style', 'display:none');
}
