function dismissModal() {
    $('#dismiss-modal').click();
    notify('success', 'Items status successfully altered!');
}

function itemsAdded() {
    notify('success', 'Item(s) added to cart!');
}

function addingFailed() {
    notify('error', 'You must specify the desired quantity (in the range between 1 and 10)!');
}

$('#btn-add-item').on('click', () => {
    $('#btn-add-item').addClass('disabled');

    setTimeout(() => {
        $('#btn-add-item').removeClass('disabled');
    }, 3000);
});