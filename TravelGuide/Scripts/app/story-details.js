function storyLiked() {
    notify('success', 'Story liked!');
}

function storyUnliked() {
    notify('success', 'Like removed!');
}

function commentSubmitted(args) {
    $.validator.unobtrusive.parse('#comment-box-' + args);

    notify('success', 'Comment submitted!');
    
    $('.comment-box-textarea').on('focus', () => {
        $('.btn-submit-box').show();
    });

    $('.comment-box-textarea').val('');
}

function commentDeleted(args) {
    $.validator.unobtrusive.parse('#comment-box-' + args);

    notify('success', 'Comment deleted!');

    $('.comment-box-textarea').on('focus', () => {
        $('.btn-submit-box').show();
    });
}

$('.btn-add-comment').on('click', () => {
    $('.comment-box-textarea').focus();
    $('.btn-submit-box').show();
});

$('.comment-box-textarea').on('focus', () => {
    $('.btn-submit-box').show();
});