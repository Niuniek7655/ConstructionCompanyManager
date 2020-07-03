$('#signupform').submit(function () {
    const isRequiredFieldsEmpty = validateIsRequiredFieldsEmpty();
    if (isRequiredFieldsEmpty) {
        event.preventDefault();
        return;
    }

    const isEmailValidate = validateIsEmailCorrect();
    if (!isEmailValidate) {
        event.preventDefault();
        return;
    }

    const isPasswordConfirmedCorrect = validateIsPasswordConfirmedCorrect();
    if (!isPasswordConfirmedCorrect) {
        event.preventDefault();
    }
});


function validateIsRequiredFieldsEmpty() {
    const username = $('#register-username').val();
    const email = $('#register-email').val();
    const password = $('#register-password').val();
    const confirmpassword = $('#register-confirmpassword').val();

    if (!username) {
        turnOnIncorrectLoginInput('#register-username', '#usernameWarning');
    }
    else {
        turnOffIncorrectLoginInput('#register-username', '#usernameWarning');
    }

    if (!email) {
        turnOnIncorrectLoginInput('#register-email', '#emailWarning');
    }
    else {
        turnOffIncorrectLoginInput('#register-email', '#emailWarning');
    }

    if (!password) {
        turnOnIncorrectLoginInput('#register-password', '#passwordWarning');
    }
    else {
        turnOffIncorrectLoginInput('#register-password', '#passwordWarning');
    }

    if (!confirmpassword) {
        turnOnIncorrectLoginInput('#register-confirmpassword', '#confirmpasswordWarning');
    }
    else {
        turnOffIncorrectLoginInput('#register-confirmpassword', '#confirmpasswordWarning');
    }

    if (!username ||
        !email ||
        !password ||
        !confirmpassword) {

        return true;
    }
    return false;
}

function turnOnIncorrectLoginInput(inputName, warningMessage) {
    $(inputName).attr('class', 'form-control is-invalid');
    $(warningMessage).attr('style', 'display:block');
}

function turnOffIncorrectLoginInput(inputName, warningMessage) {
    $(inputName).attr('class', 'form-control');
    $(warningMessage).attr('style', 'display:none');
}

function validateIsEmailCorrect() {
    const email = $('#register-email').val();
    const lowerEmail = String(email).toLowerCase();
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    const isValid = re.test(lowerEmail);
    if (!isValid) {
        $('#signupalert').attr('style', 'display:block');
        $('#signup-error-message').text('Adres email jest nie poprawny.');
    }
    return isValid;
}

function validateIsPasswordConfirmedCorrect() {
    const password = $('#register-password').val();
    const confirmpassword = $('#register-confirmpassword').val();

    const result = password.localeCompare(confirmpassword);
    if (result === -1) {
        $('#signupalert').attr('style', 'display:block');
        $('#signup-error-message').text('Hasło są różne. Spróbuj ponownie.');
        return false;
    }
    return true;
}