function RadioCheck(rb) {
    //            var gv = document.getElementById($(rb).closest('table').attr('id'));
    var rbs = rb.parentNode.parentNode.parentNode.getElementsByTagName("input");
    var row = rb.parentNode.parentNode.parentNode;
    for (var i = 0; i < rbs.length; i++) {
        if (rbs[i].type == "radio") {
            if (rbs[i].checked && rbs[i] != rb) {
                rbs[i].checked = false;
                break;
            }
        }
    }
}