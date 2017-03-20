$(document).ready(function () {
    $('.tooltipped').tooltip({ delay: 50 });
});

function clearTooltips() {
    $('.tooltipped').tooltip('remove');
}

function reloadToolTips() {
    $('.tooltipped').tooltip({ delay: 50 });

    $('.btn-add-comment').on('click', () => {
        $('.comment-box-textarea').focus();
    })
}

function deleteComment(args) {
    notify('success', 'Comment deleted!');

    var selector = $(args).attr('data-target');
    var commentsCount = $('#' + selector).html();
    $('#' + selector).html(+commentsCount - 1);
}

function changed() {
    $('#searchForm').submit();
}

function likeImage(args) {
    $('.btn-like-image').addClass('disabled');

    var selector = $(args).attr('data-target');
    var likesCount = $('#' + selector).html();
    $('#' + selector).html(+likesCount + 1);

    notify('success', 'Image liked!');

    setTimeout(() => {
        $('.btn-like-image').removeClass('disabled');
    }, 2000);
}

function unlikeImage(args) {
    $('.btn-like-image').addClass('disabled');

    var selector = $(args).attr('data-target');
    var likesCount = $('#' + selector).html();
    $('#' + selector).html(+likesCount - 1);

    notify('success', 'Like removed!');

    setTimeout(() => {
        $('.btn-like-image').removeClass('disabled');
    }, 2000);
}

$('.btn-add-comment').on('click', () => {
    $('.comment-box-textarea').focus();
})

function submitComment(args) {
    $('.comment-box-textarea').val('');
    notify('success', 'Your comment has been submitted!')

    var selector = $(args).attr('data-target');
    var commentsCount = $('#' + selector).html();
    $('#' + selector).html(+commentsCount + 1);
}

function populateModal(selector) {
    $('#' + selector).submit();
}

function rebindCommentEvent() {
    $('.btn-add-comment').on('click', () => {
        $('.comment-box-textarea').focus();
    })
}

$('body').keyup((ev) => {
    if ($('#image-details').hasClass('in')) {
        if (ev.which == 37) {
            $('#btn-left').click();
        } else if (ev.which == 39) {
            $('#btn-right').click();
        }
        ev.preventDefault();
    }
});