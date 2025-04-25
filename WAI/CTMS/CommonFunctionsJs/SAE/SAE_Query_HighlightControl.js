function ResolveHighlightControl(element) {

    var SAEID = $(element).closest('tr').find('td').eq(1).find('input').val()
    var variableName = $(element).closest('tr').find('td').eq(5).text().trim();

    var CurrentSAEID = $("#MainContent_hdnSAEID").val(); //get current page pvid

    if (CurrentSAEID === SAEID) {
        //var element1 = $("." + variableName + "").attr('id');
        $("." + variableName + "").closest('tr').find('td:eq(4)').addClass("border3pxsolidblack");
        $("." + variableName + "").closest('tr').find('td:eq(4)').focus();
    }

    return false;
}