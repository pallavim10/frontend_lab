function OpenModule(element) {
    var MODULE_ID = $(element).closest('tr').find('td:eq(13)').find('span').text();
    var MODULE_NAME = $(element).closest('tr').find('td:eq(14)').find('span').text();
    var MULTIPLE = $(element).closest('tr').find('td:eq(15)').find('span').text();
    var VARIABLENAME = $(element).closest('tr').find('td:eq(0)').find('span').text();

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let INVID = params.INVID;
    let VISITID = params.VISITID;
    let VISIT = params.VISIT;
    let SUBJID = params.SUBJID;

    var REF = $('.REFERENCE').val();

    if (REF != '' && REF != undefined) {

        if (MODULE_ID != "0") {

            if (MULTIPLE == "True") {
                var test = "DM_DataEntry_MultipleData.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VARIABLENAME=" + VARIABLENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + REF;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
            else {
                var test = "DM_DataEntry.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + REF;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

    }
    else {

        if (MODULE_ID != "0") {

            if (MULTIPLE == "True") {
                var test = "DM_DataEntry_MultipleData.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VARIABLENAME=" + VARIABLENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
            else {
                var test = "DM_DataEntry.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

    }
}

function OpenModule_Inv_ReadOnly(element) {
    var MODULE_ID = $(element).closest('tr').find('td:eq(12)').find('span').text();
    var MODULE_NAME = $(element).closest('tr').find('td:eq(13)').find('span').text();
    var MULTIPLE = $(element).closest('tr').find('td:eq(14)').find('span').text();
    var VARIABLENAME = $(element).closest('tr').find('td:eq(0)').find('span').text();

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let INVID = params.INVID;
    let VISITID = params.VISITID;
    let VISIT = params.VISIT;
    let SUBJID = params.SUBJID;

    var REF = $('.REFERENCE').val();

    if (REF != '' && REF != undefined) {

        if (MODULE_ID != "0") {

            if (MULTIPLE == "True") {
                var test = "DM_DataEntry_MultipleData.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VARIABLENAME=" + VARIABLENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + REF;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
            else {
                var test = "DM_DataEntry.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + REF;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

    }
    else {

        if (MODULE_ID != "0") {

            if (MULTIPLE == "True") {
                var test = "DM_DataEntry_MultipleData.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VARIABLENAME=" + VARIABLENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
            else {
                var test = "DM_DataEntry.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

    }
}

function OpenModule_ReadOnly(element) {
    var MODULE_ID = $(element).closest('tr').find('td:eq(13)').find('span').text();
    var MODULE_NAME = $(element).closest('tr').find('td:eq(14)').find('span').text();
    var MULTIPLE = $(element).closest('tr').find('td:eq(15)').find('span').text();
    var VARIABLENAME = $(element).closest('tr').find('td:eq(0)').find('span').text();

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let INVID = params.INVID;
    let VISITID = params.VISITID;
    let VISIT = params.VISIT;
    let SUBJID = params.SUBJID;

    var REF = $('.REFERENCE').val();

    if (REF != '' && REF != undefined) {

        if (MODULE_ID != "0") {

            if (MULTIPLE == "True") {
                var test = "DM_DataEntry_MultipleData_ReadOnly.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VARIABLENAME=" + VARIABLENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + REF;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
            else {
                var test = "DM_DataEntry_ReadOnly.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + REF;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

    }
    else {

        if (MODULE_ID != "0") {

            if (MULTIPLE == "True") {
                var test = "DM_DataEntry_MultipleData_ReadOnly.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VARIABLENAME=" + VARIABLENAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
            else {
                var test = "DM_DataEntry_ReadOnly.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

    }
}