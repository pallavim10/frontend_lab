function bindOptionValues() {

    var txtFields = $(".OptionValues").toArray();
    for (a = 0; a < txtFields.length; ++a) {
        var items = "";
        items = $(txtFields[0]).closest('td').find("input[id*='hfValue1']").val().split('¸');
        $(txtFields[a]).autocomplete({
            source: items, minLength: 0
        }).on('focus', function () { $(this).keydown(); });
    }

}

$(document).ready(function () {

    var txtFields = $(".OptionValues").toArray();
    for (a = 0; a < txtFields.length; ++a) {
        var items = "";
        items = $(txtFields[0]).closest('td').find("input[id*='hfValue1']").val().split('¸');
        $(txtFields[a]).autocomplete({
            source: items, minLength: 0
        }).on('focus', function () { $(this).keydown(); });
    }

});