function DATA_Changed(element) {

    $(element).closest('td').find("input[id*='btnDATA_Changed']").click();

}



$(document).ready(function () {

    $('.txtDate').mousedown(function (event) {
        switch (event.which) {
            case 3:
                $(this).val('');
                break;
        }
    });


    $('.txtDateNoFuture').mousedown(function (event) {
        switch (event.which) {
            case 3:
                $(this).val('');
                break;
        }
    });

});