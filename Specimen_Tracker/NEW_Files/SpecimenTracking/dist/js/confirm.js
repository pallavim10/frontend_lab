function confirm(event) {
    event.preventDefault();

    swal({
        title: "Warning!",
        text: "Are you sure you want to delete this Record?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            var linkButton = event.target;
            if (linkButton.tagName.toLowerCase() === 'i') {
                linkButton = linkButton.parentElement;
            }
            linkButton.onclick = null;
            linkButton.click();
        } else {
            Response.redirect(this);
        }
    });
    return false;