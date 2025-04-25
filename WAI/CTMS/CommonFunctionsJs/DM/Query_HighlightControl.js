function ResolveHighlightControl(PVID, VARIABLENAME) {

    //var pvid = $(element).closest('tr').find('td').eq(1).find('input').val()
    //var variableName = $(element).closest('tr').find('td').eq(6).text().trim();

    var CurrentPVID = $("#MainContent_hdnPVID").val(); //get current page pvid

    if (CurrentPVID === PVID) {
        //var element1 = $("." + variableName + "").attr('id');
        //controlType = $('#' + element1).closest('tr').find('td:eq(1)').find('span').html();
        //if (controlType == 'DROPDOWN') {
        //    element1 = $('#' + element1).closest('tr').find('td:eq(4)').find('select').attr('id');
        //}
        //else {
        //    element1 = $('#' + element1).closest('tr').find('td:eq(4)').find('input').attr('id');
        //}

        $("." + VARIABLENAME + "").closest('tr').find('td:eq(4)').addClass("border3pxsolidblack");
        $("." + VARIABLENAME + "").closest('tr').find('td:eq(4)').focus();

        //$("#" + element1).closest('td').addClass("border3pxsolidblack");
        //$("#" + element1).closest('td').focus();
    }

    return false;
}