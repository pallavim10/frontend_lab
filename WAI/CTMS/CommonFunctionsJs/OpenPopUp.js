function OpenPopUp(element, title) {

    $("#" + element + "").removeClass("disp-none");

    $("#" + element + "").dialog({
        title: title,
        width: 900,
        height: 500,
        modal: true,
        buttons: {
            "Close": function () { $(this).dialog("close"); }
        }
    });

    return false;
}