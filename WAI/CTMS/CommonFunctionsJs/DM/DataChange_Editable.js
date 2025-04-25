//onfocus of any control this function will call
var counter = 0;
function TBLmyFocus(element) {
    var test = '';

    if ($(element).is("select")) {

        if ($(element).val().trim() == '0') {
            test = '';
        }
        else {
            test = $(element).val().trim();
        }
    }
    else {
        test = $(element).val().trim();
    }

    $('#MainContent_hdn_TBLValue').val(test);
    counter++;
    // alert(test);
}

function DATA_Changed_Editable_Set_Value_Data(tblVARIABLENAME, tblPVID, tblRECID, tblTABLENAME, SET_VALUE_DATA) {
    $.ajax({
        type: "POST",
        url: "DM_DataEntry_MultipleData.aspx/DATA_Changed_Editable",
        data: '{PVID: "' + tblPVID + '",RECID: "' + tblRECID + '",SUBJID: "",VISITID:"",MODULENAME: "" , FIELDNAME: "",TABLENAME: "' + tblTABLENAME + '",VARIABLENAME: "' + tblVARIABLENAME + '",Reason: "Data Entry Error",Comment: "",OldValue: "",NewValue: "' + SET_VALUE_DATA + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

        },
        failure: function (response) {
            if (response.d == 'Object reference not set to an instance of an object.') {
                alert(response.d);
            }
            else {
                alert("Contact administrator not suceesfully updated")
            }
        }
    });
}

//onchange event of any control in gridview this function will call
function DATA_Changed_Editable(element, tblVARIABLENAME, tblPVID, tblRECID, tblTABLENAME, tblFIELDNAME) {

    var hdn_TBLValue_DATA = $('#MainContent_hdn_TBLValue').val();

    PVID = tblPVID;
    RECID = tblRECID;

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let SUBJID = params.SUBJID;
    let VISITID = params.VISITID;
    let MODULENAME = params.MODULENAME;
    var hdnValue = hdn_TBLValue_DATA;

    //used for onfirstime binding grid  date control change event is called so prevent that
    if (($(element).hasClass("txtDate") || $(element).hasClass("txtDateNoFuture")) && hdnValue == "" && counter == 0) {
        return false;
    }
    //for add new row function on add click hdnvalue 0 the poup up open so avoid that this function is used
    if (($(element).hasClass("txtDate") || $(element).hasClass("txtDateNoFuture")) && hdnValue == "0") {
        return false;
    }

    var elementValue = $(element).val().trim();

    if ($(element).is("select")) {
        if ($(element).val().trim() == '0' || $(element).val().trim() == '--Select--') {
            elementValue = '';
        }
    }

    if (hdn_TBLValue_DATA != '' && hdn_TBLValue_DATA != '--Select--') {

        if (hdnValue == elementValue) {
            return false;
        }
        else {

            $.ajax({
                type: "POST",
                url: "DM_DataEntry_MultipleData.aspx/DATA_Changed_Editable",
                data: '{PVID: "' + PVID + '",RECID: "' + RECID + '",SUBJID: "' + SUBJID + '",VISITID:"' + VISITID + '",MODULENAME: "' + MODULENAME + '" , FIELDNAME: "' + tblFIELDNAME + '",TABLENAME: "' + tblTABLENAME + '",VARIABLENAME: "' + tblVARIABLENAME + '",Reason: "Data Entry Error",Comment: "",OldValue: "' + hdn_TBLValue_DATA + '",NewValue: "' + elementValue + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert(response.d);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
        }
        return false;
    }
    else {

        $.ajax({
            type: "POST",
            url: "DM_DataEntry_MultipleData.aspx/DATA_Changed_Editable",
            data: '{PVID: "' + PVID + '",RECID: "' + RECID + '",SUBJID: "' + SUBJID + '",VISITID:"' + VISITID + '",MODULENAME: "' + MODULENAME + '" , FIELDNAME: "' + tblFIELDNAME + '",TABLENAME: "' + tblTABLENAME + '",VARIABLENAME: "' + tblVARIABLENAME + '",Reason: "Data Entry Error",Comment: "",OldValue: "' + hdn_TBLValue_DATA + '",NewValue: "' + elementValue + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

            },
            failure: function (response) {
                if (response.d == 'Object reference not set to an instance of an object.') {
                    alert(response.d);
                }
                else {
                    alert("Contact administrator not suceesfully updated")
                }
            }
        });

    }
}