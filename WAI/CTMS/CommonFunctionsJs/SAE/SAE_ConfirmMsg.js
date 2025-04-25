var Msg;
function ConfirmMsg(Msg) {
    var Confirmed = confirm(Msg);
    if (Confirmed) {
        return true;
    } else {
        return false;
    }
}
