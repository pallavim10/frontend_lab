function DATA_Changed(element) {

    if ($(element).attr('type') == 'radio' || $(element).attr('type') == 'checkbox') {
        $(element).parent().parent().parent().next().next().next().click();
    }
    else {
        $(element).next().next().next().click();
    }

}