function ConfirmMsg() {
    var newLine = "\r\n"

    var error_msg = $("#MainContent_hdnError_Msg").val();

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

var Msg;
function ConfirmDeleteMsg(Msg) {
    var Confirmed = confirm(Msg);
    if (Confirmed) {
        return true;
    } else {
        return false;
    }
}