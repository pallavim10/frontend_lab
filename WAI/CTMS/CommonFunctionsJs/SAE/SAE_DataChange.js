function DATA_Changed(element) {

    $(element).closest('td').find("input[id*='HDN_FIELD']").val($(element).val());

    var pageStatus = $("#MainContent_hdn_PAGESTATUS").val();
    var SYNC_COUNT = $(element).closest('tr').find('td').find("span[id*='SYNC_COUNT']").text();

    if (pageStatus == "1" && SYNC_COUNT == "0") {
        $(element).closest('td').find('.btnDATA_Changed').click();
    }
}

$(document).ready(function () {

    $('.rightClick').mousedown(function (event) {
        switch (event.which) {
            case 3:
                DATA_Changed_RightClick(this);
                break;
        }
    });

});

function DATA_Changed_RightClick(element) {

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let DELETED = params.DELETED;

    if (DELETED != "1") {

        if (($(element).attr("readonly") != "readonly") || ($(element).attr("readonly") == "readonly") && ($(element).hasClass("txtDate") || $(element).hasClass("txtDateNoFuture"))) {

            var pageStatus = $("#MainContent_hdn_PAGESTATUS").val();
            var SYNC_COUNT = $(element).closest('tr').find('td').find("span[id*='SYNC_COUNT']").text();

            if (pageStatus == "1" && SYNC_COUNT == "0") {

                if ($(element).hasClass("Mandatory") == true) {
                    $(element).addClass("brd-1px-redimp");
                    return false;
                }
                else {
                    if ($(element).hasClass('radio')) {
                        $(element).closest('td').find('input').prop('checked', false);
                    }
                    else {
                        $(element).val('');
                    }

                    $(element).closest('td').find('.btnRightClick_Changed').click();
                }
            }
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}


function Check_ReasonEntered() {

    if ($('#MainContent_drp_Reason').val() == '0') {
        alert('Please select Reason');
        return false;
    }
    else {
        if ($('#MainContent_drp_Reason').val() == 'Other' && $('#MainContent_txt_Comments').val().trim() == '') {
            alert('Please enter Comments');
            return false;
        }
        else {
            return true;
        }
    }

};



