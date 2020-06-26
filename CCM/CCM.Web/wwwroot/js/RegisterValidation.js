$('#signupform').submit(function () {

   
    var name = $.trim($('#log').val());

    
    if (name === '') {
        alert('Text-field is empty.');
        return false;
    }
});