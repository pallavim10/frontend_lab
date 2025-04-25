function OpenModule(element) {
    var INVID = getUrlVars()["INVID"];
    var SUBJID = getUrlVars()["SUBJID"];
    var SAE = getUrlVars()["SAE"];
    var STATUS = getUrlVars()["STATUS"];
    var SAEID = getUrlVars()["SAEID"];
    var MODULEID = getUrlVars()["MODULEID"];

    var REF = $('.REFERENCE').val()

    if (REF != '' && REF != undefined) {

        if (MODULE_ID != "0") {

            if (MULTIPLE == "True") {
                var test = "NSAE_MULTIPLE_DATA_ENTRY.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&MODULEID=" + MODULEID + "&OPENLINK=1&REFERENCE=" + REF;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
            else {
                var test = "NSAE_DATA_ENTRY.aspx?INVID=" + INVID + "&SUBJID=" + SUBJID + "&SAE=" + SAE + "&STATUS=" + STATUS + "&SAEID=" + SAEID + "&MODULEID=" + MODULEID + "&OPENLINK=1&REFERENCE=" + REF;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

    }
    else {

        if (MODULE_ID != "0") {

            if (MULTIPLE == "True") {
                var test = "DM_DataEntry_MultipleData.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&Indication=" + Indication + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
            else {
                var test = "DM_DataEntry.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&Indication=" + Indication + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

    }

}

$(document).ready(function () {
    $("#MainContent_lblPageStatus").click(function () {

        ShowStatusPopUP();
        if ($('#pop_PageStatus').hasClass("disp-none")) {
            $('#pop_PageStatus').removeClass("disp-none");
            $('#pop_PageStatus').addClass("disp-block");
            $('#MainContent_divPageStatus').removeClass("disp-none");
            $('#MainContent_divPageStatus').addClass("disp-block");
        }
        else {
            $('#pop_PageStatus').removeClass("disp-block");
            $('#pop_PageStatus').addClass("disp-none");
            $('#MainContent_divPageStatus').removeClass("disp-none");
            $('#MainContent_divPageStatus').addClass("disp-block");
        }
    });
});

function ShowStatusPopUP() {
    $("#pop_PageStatus").dialog({
        title: "Module Status",
        width: 1020.28,
        height: 323.28,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}