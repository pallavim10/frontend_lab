

$(document).ready(function () {

    $("textarea[maxlength]").each(function (index, element) {

        $(element).MaxLength({
            MaxLength: $(element).attr("maxlength")
        });

    });

    $("[maxlength]").on('keydown', function () {
        var maxLength = $(this).attr('maxlength');
        var currentLength = $(this).val().length;
        const key = event.key;

        if (key != "Backspace" && key != "Delete" && key != "Tab" && key != "Enter") {
            if (currentLength === parseInt(maxLength)) {
                alert("Oops! You've exceeded the character limit. Please limit your input to a maximum of " + maxLength + " characters.");
            }
        }
    });

});