﻿$(document).ready(function () {
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

function commentDeleted(args) {
    notify('success', 'Comment deleted!');

    $.validator.unobtrusive.parse('#comment-box-' + args);

    var selector = 'comments-count-' + args;
    var commentsCount = $('#' + selector).html();
    $('#' + selector).html(+commentsCount - 1);

    $('.comment-box-textarea').on('focus', () => {
        $('.btn-submit-box').show();
    })
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

function commentSubmitted(args) {
    $('.comment-box-textarea').val('');
    notify('success', 'Your comment has been submitted!')

    $.validator.unobtrusive.parse('#comment-box-' + args);

    var selector = 'comments-count-' + args;
    var commentsCount = $('#' + selector).html();
    $('#' + selector).html(+commentsCount + 1);

    $('.comment-box-textarea').on('focus', () => {
        $('.btn-submit-box').show();
    })
}

function populateModal(selector) {
    $('#' + selector).submit();
}

$('.btn-add-comment').on('click', () => {
    $('.comment-box-textarea').focus();
    $('.btn-submit-box').show();
})

$('.comment-box-textarea').on('focus', () => {
    $('.btn-submit-box').show();
})

function rebindCommentEvent(args) {
    $.validator.unobtrusive.parse('#comment-box-' + args);

    $('.btn-add-comment').on('click', () => {
        $('.comment-box-textarea').focus();
        $('.btn-submit-box').show();
    });

    $('.comment-box-textarea').on('focus', () => {
        $('.btn-submit-box').show();
    });
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

$('#image-details').on('hidden.bs.modal', () => {
    $('#image-details').html('');
})