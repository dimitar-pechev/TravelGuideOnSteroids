function dismissModal() {
    $('body > div.modal-backdrop.fade.in').hide();
    notify('success', 'Request sent! An administrator will contact you for confirmation!');
}