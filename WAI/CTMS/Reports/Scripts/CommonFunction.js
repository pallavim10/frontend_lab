
//For ajax function call on update data button click
function SAE_UpdateData() {
    if ($("#drp_Reason option:selected").val() == 0) {
        //  alert("Please select reason");
        // return false;
    }
    console.log('{ContId: "' + $("#txt_ContId")[0].value.trim() + '",ModuleName: "' + $("#txt_ModuleName")[0].value.trim() + '" ,FieldName: "' + $("#txt_FieldName")[0].value.trim() + '",TableName: "' + $("#txt_TableName")[0].value.trim() + '",VariableName: "' + $("#txt_VariableName")[0].value.trim() + '",OldValue: "' + $("#txt_OldValue")[0].value.trim() + '",NewValue: "' + $("#txt_NewValue")[0].value.trim() + '",ModuleName: "' + $("#txt_ModuleName")[0].value.trim() + '",Reason: "' + $("#drp_Reason option:selected").text() + '",Comments: "' + $("#txt_Comments")[0].value.trim() + '",ControlType: "' + controlType + '"}');
    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/SAE_Audit_Trail",
        data: '{ContId: "' + $("#txt_ContId")[0].value.trim() + '",ModuleName: "' + $("#txt_ModuleName")[0].value.trim() + '" ,FieldName: "' + $("#txt_FieldName")[0].value.trim() + '",TableName: "' + $("#txt_TableName")[0].value.trim() + '",VariableName: "' + $("#txt_VariableName")[0].value.trim() + '",OldValue: "' + $("#txt_OldValue")[0].value.trim() + '",NewValue: "' + $("#txt_NewValue")[0].value.trim() + '",ModuleName: "' + $("#txt_ModuleName")[0].value.trim() + '",Reason: "' + $("#drp_Reason option:selected").text() + '",Comments: "' + $("#txt_Comments")[0].value.trim() + '",ControlType: "' + controlType + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
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

//For ajax function call on update data button click
function DM_UpdateData() {
    if ($("#drp_Reason option:selected").val() == 0) {
        alert("Please select reason");
        return false;
    }
    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/DM_UpdateData",
        data: '{ContId: "' + $("#txt_ContId")[0].value.trim() + '",ModuleName: "' + $("#txt_ModuleName")[0].value.trim() + '" ,FieldName: "' + $("#txt_FieldName")[0].value.trim() + '",TableName: "' + $("#txt_TableName")[0].value.trim() + '",VariableName: "' + $("#txt_VariableName")[0].value.trim() + '",OldValue: "' + $("#txt_OldValue")[0].value.trim() + '",NewValue: "' + $("#txt_NewValue")[0].value.trim() + '",ModuleName: "' + $("#txt_ModuleName")[0].value.trim() + '",Reason: "' + $("#drp_Reason option:selected").text() + '",Comments: "' + $("#txt_Comments")[0].value.trim() + '",Query: "' + query + '",ControlType: "' + controlType + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: DM_OnSuccess,
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

//For ajax function call on update data button click
function AddManualQuery() {
    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/SetManualQuery",
        data: '{ContId: "' + $("#txt_MQContId")[0].value.trim() + '",QID: "' + $("#txt_MQQID")[0].value.trim() + '",QueryText: "' + $("#txt_MQQueryText")[0].value.trim() + '",ModuleName: "' + $("#txt_MQModule")[0].value.trim() + '" ,FieldName: "' + $("#txt_MQFieldName")[0].value.trim() + '",TableName: "' + $("#txt_MQTable")[0].value.trim() + '",VariableName: "' + $("#txt_MQVariable")[0].value.trim() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { alert(data.d); location.reload(true) },
        failure: function (response) {
            if (response.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                alert("Contact administrator not suceesfully updated")
            }

        }
    });
}
function OnSuccess(response) {

    var text = response.d;
    text.includes("/")

    // if (response.d == "Record Updated successfully.")
    if (text.includes("Record Updated successfully.")) {
        console.log(response.d);
        alert("Record Updated successfully.");

        if (CurrentObjType == "checkbox") {
            if ($("#txt_OldValue")[0].value == "True")
                $('#' + CurrentObj + '').attr('checked', true);
            else
                $('#' + CurrentObj + '').attr('checked', false);
        }
        $('#' + CurrentObj + '').val($("#txt_NewValue")[0].value.trim()); //on cancel click set old value to control
        $("#popup_SAE_UpdateData").dialog('close');
        window.location = window.location;
        return false;
    }
    if (response.d == 'Object reference not set to an instance of an object.') {
        alert("Session Expired");
        var url = "SessionExpired.aspx";
        $(location).attr('href', url);
    }
    else {
        alert("Some error occured contact administartor");
        console.log(response.d);
        if (CurrentObjType == "checkbox") {
            if ($("#txt_OldValue")[0].value == "True")
                $('#' + CurrentObj + '').attr('checked', true);
            else
                $('#' + CurrentObj + '').attr('checked', false);
        }
        $('#' + CurrentObj + '').val($("#txt_OldValue")[0].value.trim()); //on cancel click set old value to control
        $("#popup_SAE_UpdateData").dialog('close');
        return false;
    }
}

//on cancel click of update reason for change popup
function SAE_CancelUpdate() {
    if (CurrentObjType == "checkbox") {
        if ($("#txt_OldValue")[0].value == "True")
            $('#' + CurrentObj + '').attr('checked', true);
        else
            $('#' + CurrentObj + '').attr('checked', false);
    }
    $('#' + CurrentObj + '').val($("#txt_OldValue")[0].value.trim()); //on cancel click set old value to control
    $("#popup_SAE_UpdateData").dialog('close');
    //$(this).closest(".ui-dialog").next(".ui-widget-overlay").remove();
    return false;
}


//on cancel click of update reason for change popup
function DM_CancelUpdate() {
    if (CurrentObjType == "checkbox") {
        if ($("#txt_OldValue")[0].value == "True")
            $('#' + CurrentObj + '').attr('checked', true);
        else
            $('#' + CurrentObj + '').attr('checked', false);
    }
    $('#' + CurrentObj + '').val($("#txt_OldValue")[0].value.trim()); //on cancel click set old value to control
    $("#popup_UpdateData").dialog('close');
    //$(this).closest(".ui-dialog").next(".ui-widget-overlay").remove();
    return false;
}
function DM_OnSuccess(response) {

    var text = response.d;
    text.includes("/")

    // if (response.d == "Record Updated successfully.")
    if (text.includes("Record Updated successfully.")) {
        console.log(response.d);
        alert("Record Updated successfully.");

        if (CurrentObjType == "checkbox") {
            if ($("#txt_OldValue")[0].value == "True")
                $('#' + CurrentObj + '').attr('checked', true);
            else
                $('#' + CurrentObj + '').attr('checked', false);
        }
        $('#' + CurrentObj + '').val($("#txt_NewValue")[0].value.trim()); //on cancel click set old value to control
        $("#popup_UpdateData").dialog('close');
        window.location = window.location;
        return false;
    }
    if (response.d == 'Object reference not set to an instance of an object.') {
        alert("Session Expired");
        var url = "SessionExpired.aspx";
        $(location).attr('href', url);
    }
    else {
        alert("Some error occured contact administartor");
        console.log(response.d);
        if (CurrentObjType == "checkbox") {
            if ($("#txt_OldValue")[0].value == "True")
                $('#' + CurrentObj + '').attr('checked', true);
            else
                $('#' + CurrentObj + '').attr('checked', false);
        }
        $('#' + CurrentObj + '').val($("#txt_OldValue")[0].value.trim()); //on cancel click set old value to control
        $("#popup_UpdateData").dialog('close');
        return false;
    }
}


function GetOverrideCommnets() {
    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/GetOverrideComments",
        data: '{Id: "' + overrideid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                // alert(data.d);
                $('#grdOverrideComments').html(data.d)
            }
        },
        failure: function (response) {
            if (response.d == 'Object reference not set to an instance of an object.') {
                alert("Session Expired");
                var url = "SessionExpired.aspx";
                $(location).attr('href', url);
            }
            else {
                alert("Contact administrator not suceesfully updated")
            }

        }
    });
}




function DrpOverrideReasonChange() {
    var drptext = $('#drpoverrideReason option:selected').text();
    if (drptext == 'Open') {
        $('#OverrideComments').removeClass("disp-none");
        $('#OverrideComments').addClass("disp-block");
    }
    else {
        if ($('#OverrideComments').hasClass("disp-block")) {
            $('#OverrideComments').removeClass("disp-block");
            $('#OverrideComments').addClass("disp-none");
        }
    }
}


//on cancel click of update reason for change popup
function CancelOverrideData() {
    $("#popup_Override").dialog('close');
    //$(this).closest(".ui-dialog").next(".ui-widget-overlay").remove();
    return false;
}

//show audit details icon of field 
function FillAuditDetails() {
    var count = 0;
    $("#grdAUDITTRAILDETAILS tr").each(function () {
        if (count > 0) {
            var contId = $(this).find('td:eq(2)').find('input').val().trim();
            var tableName = 'grd_Data';
            var variableName = $(this).find('td:eq(1)').find('input').val().trim();

            var element = $("img[id*='_AD_" + variableName + "_" + contId + "']").attr('id');
            $("#" + element).removeClass("disp-none");
        }
        count++;
    });

}

//fill comments details icon change
function FillCommentsDetails() {
    var count = 0;
    $("#grdComments tr").each(function () {
        if (count > 0) {
            var contId = $(this).find('td:eq(0)').find('input').val().trim();
            var tableName = 'grd_Data';
            var variableName = $(this).find('td:eq(2)').find('input').val().trim();

            var element = $("img[id*='_CM_" + variableName + "_" + contId + "']").attr('id');
            $("#" + element).attr("src", "Images/index_3.png");
        }
        count++;
    });

}




////For ajax function call on update data button click
//function saveFieldComments() {
//    $.ajax({
//        type: "POST",
//        url: "AjaxFunction.aspx/SetFielsComments",
//        data: '{VARIABLENAME: "' + $("#txtVariableName")[0].value + '",ContId: "' + $("#txtFieldContID")[0].value.trim() + '",TableName: "' + $("#txtFieldTableName")[0].value.trim() + '",Comments: "' + $("#txtFieldComments")[0].value.trim() + '",FieldName: "' + $("#txtcolumnName")[0].value.trim() + '"}',
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            if (data.d == 'Object reference not set to an instance of an object.') {
//                alert("Session Expired");
//                var url = "SessionExpired.aspx";
//                $(location).attr('href', url);
//            }
//            else {
//                alert(data.d);
//                location.reload(true)
//            }
//        },
//        failure: function (response) {
//            if (response.d == "Object reference not set to an instance of an object.") {
//                alert("Session Expired");
//                var url = "SessionExpired.aspx";
//                $(location).attr('href', url);
//            }
//            else {
//                alert("Contact administrator not suceesfully updated")
//            }

//        }
//    });
//}

function checkVerifyAll(chk_Verify_All) {
    $('.sdvCheckbox').each(function (index, element) {
        if (!$(element).closest('tr').hasClass("disp-none")) {

            if ($(chk_Verify_All).is(":checked")) {
                $(element).find("input").prop("checked", true);
            }
            else {
                $(element).find("input").prop("checked", false);
            }
        }
    });
}



