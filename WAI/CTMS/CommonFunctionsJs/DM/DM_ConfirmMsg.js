function ConfirmMsg(MSG) {
    var newLine = "\r\n"

    if (MSG != undefined && MSG != '') {
        var error_msg = MSG;
    }
    else {
        var error_msg = $("#MainContent_hdnError_Msg").val();
    }

    error_msg += newLine;
    error_msg += newLine;

    error_msg += "Note : Press OK to Proceed.";

    if (confirm(error_msg)) {

        $("#MainContent_btnSubmitOnsubmitData").click();

        return true;

    } else {

        return false;
    }

}