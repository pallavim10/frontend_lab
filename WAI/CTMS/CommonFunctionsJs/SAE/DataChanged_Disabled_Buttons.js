//onfocus of any control this function will call    
function myFocus() {

    if ($("#MainContent_hdn_PAGESTATUS").val() == '1') {

        $('#MainContent_bntSaveComplete').prop('disabled', true);
        $('#MainContent_btnSaveIncomplete').prop('disabled', true);
        $("#MainContent_lblPageStatus").attr("style", "opacity: .65; box-shadow: none; cursor: not-allowed;");
        $("#MainContent_lbtnPageComment").attr("style", "opacity: .65; box-shadow: none; cursor: not-allowed;");
        $('#MainContent_btnDeleteData').prop('disabled', true);
        $('#MainContent_lblApplicableStatus').prop('disabled', true);
        $('#MainContent_btnNotApplicable').prop('disabled', true);

    }

    if ($("#MainContent_hdn_PAGESTATUS").val() == '1' && $("#MainContent_hdnRECID").val() != "-1") {

        $('#MainContent_bntSaveComplete').prop('disabled', true);
        $('#MainContent_btnSaveIncomplete').prop('disabled', true);
        $("#MainContent_lblPageStatus").attr("style", "opacity: .65; box-shadow: none; cursor: not-allowed;");
        $("#MainContent_lbtnPageComment").attr("style", "opacity: .65; box-shadow: none; cursor: not-allowed;");
        $('#MainContent_btnDeleteData').prop('disabled', true);
        $('#MainContent_lblApplicableStatus').prop('disabled', true);
        $('#MainContent_btnNotApplicable').prop('disabled', true);

    }

}