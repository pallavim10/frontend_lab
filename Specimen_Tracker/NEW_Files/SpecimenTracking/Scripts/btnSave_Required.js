$(document).on("click", ".cls-btnSave", function () {
    var test = "0";

    $('.required').each(function (index, element) {
        var value = $(this).val();
        var ctrl = $(this).prop('type');

        if (ctrl == "select-one") {
            if (value == "0" || value == null || value == '') {
                if ($(this).hasClass("select") == true || $(this).hasClass("select2") == true) {
                    $(this).next("span").addClass("brd-1px-redimp");
                    test = "1";
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                    test = "1";
                }
            }
        }
        else if (ctrl == "text" || ctrl == "textarea" || ctrl == "password") {
            if (value == "") {
                if ($(this).hasClass("ckeditor")) {
                    $(this).next('div').addClass("brd-1px-redimp");
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                }
                test = "1";
            }
        }
    });

    $('.txtTime').each(function (index, element) {
        var value = $(this).val();

        if (value.indexOf('H') != -1 || value.indexOf('M') != -1) {
            $(this).addClass("brd-1px-redimp");
            test = "1";
        }
    });

    $('.initial').each(function (index, element) {
        var value = $(this).val();

        if (value.indexOf('_') != -1) {
            $(this).addClass("brd-1px-redimp");
            test = "1";
        }
    });

    if (test == "1") {
        return false;
    }
    return true;
});

$(document).on("click", ".cls-btnSave1", function () {
    var test = "0";

    $('.required1').each(function (index, element) {
        var value = $(this).val();
        var ctrl = $(this).prop('type');

        if (ctrl == "select-one") {
            if (value == "0" || value == null) {
                if ($(this).hasClass("select") == true) {
                    $(this).next("span").addClass("brd-1px-redimp");
                    test = "1";
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                    test = "1";
                }
            }
        }
        else if (ctrl == "text" || ctrl == "textarea" || ctrl == "password") {
            if (value == "") {
                if ($(this).hasClass("ckeditor")) {
                    $(this).next('div').addClass("brd-1px-redimp");
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                }
                test = "1";
            }
        }
    });

    if (test == "1") {
        return false;
    }
    return true;
});

$(document).on("click", ".cls-btnSave2", function () {
    var test = "0";

    $('.required2').each(function (index, element) {
        var value = $(this).val();
        var ctrl = $(this).prop('type');

        if (ctrl == "select-one") {
            if (value == "0" || value == null) {
                if ($(this).hasClass("select") == true) {
                    $(this).next("span").addClass("brd-1px-redimp");
                    test = "1";
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                    test = "1";
                }
            }
        }
        else if (ctrl == "text" || ctrl == "textarea" || ctrl == "password") {
            if (value == "") {
                if ($(this).hasClass("ckeditor")) {
                    $(this).next('div').addClass("brd-1px-redimp");
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                }
                test = "1";
            }
        }
    });

    if (test == "1") {
        return false;
    }
    return true;
});

$(document).on("click", ".cls-btnSave3", function () {
    var test = "0";

    $('.required3').each(function (index, element) {
        var value = $(this).val();
        var ctrl = $(this).prop('type');

        if (ctrl == "select-one") {
            if (value == "0" || value == null || value == "Select") {
                if ($(this).hasClass("select") == true) {
                    $(this).next("span").addClass("brd-1px-redimp");
                    test = "1";
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                    test = "1";
                }
            }
        }
        else if (ctrl == "text" || ctrl == "textarea") {
            if (value == "") {
                if ($(this).hasClass("ckeditor")) {
                    $(this).next('div').addClass("brd-1px-redimp");
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                }
                test = "1";
            }
        }

    });

    if (test == "1") {
        return false;
    }
    return true;
});

$(document).on("click", ".cls-btnSave4", function () {
    var test = "0";

    $('.required4').each(function (index, element) {
        var value = $(this).val();
        var ctrl = $(this).prop('type');

        if (ctrl == "select-one") {
            if (value == "0" || value == null || value == "Select") {
                if ($(this).hasClass("select") == true) {
                    $(this).next("span").addClass("brd-1px-redimp");
                    test = "1";
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                    test = "1";
                }
            }
        }
        else if (ctrl == "text" || ctrl == "textarea") {
            if (value == "") {
                if ($(this).hasClass("ckeditor")) {
                    $(this).next('div').addClass("brd-1px-redimp");
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                }
                test = "1";
            }
        }
    });

    if (test == "1") {
        return false;
    }
    return true;
});

$(document).on("click", ".cls-btnSave5", function () {
    var test = "0";

    $('.required5').each(function (index, element) {
        var value = $(this).val();
        var ctrl = $(this).prop('type');

        if (ctrl == "select-one") {
            if (value == "0" || value == null || value == "Select") {
                if ($(this).hasClass("select") == true) {
                    $(this).next("span").addClass("brd-1px-redimp");
                    test = "1";
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                    test = "1";
                }
            }
        }
        else if (ctrl == "text" || ctrl == "textarea") {
            if (value == "") {
                if ($(this).hasClass("ckeditor")) {
                    $(this).next('div').addClass("brd-1px-redimp");
                }
                else {
                    $(this).addClass("brd-1px-redimp");
                }
                test = "1";
            }
        }
    });

    if (test == "1") {
        return false;
    }
    return true;
});