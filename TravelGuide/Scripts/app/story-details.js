function storyLiked() {
    notify('success', 'Story liked!');
}

function storyUnliked() {
    notify('success', 'Like removed!');
}

function commentSubmitted() {
    notify('success', 'Comment submitted!');

    $.validator.unobtrusive.parse("#comment-box");

    $('.comment-box-textarea').on('focus', () => {
        $('.btn-submit-box').show();
    });

    $('.comment-box-textarea').val('');
}

function commentDeleted() {
    notify('success', 'Comment deleted!');

    $.validator.unobtrusive.parse("#comment-box");

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