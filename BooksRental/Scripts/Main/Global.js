function validateFileType(fileId, ErrorLblId, submitId) {
    var ext = $('#' + fileId).val().split('.').pop().toLowerCase();
    var is = $('#' + fileId).val();
    if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1 && is != "") {
        $('#' + ErrorLblId).text('The image can be only png, jpg or jpeg');
        $('#' + ErrorLblId).show();
        $('#' + submitId).prop('disabled', true);
    } else {
        $('#'+submitId).prop('disabled', false);
        $('#' + ErrorLblId).hide();
    }
}

