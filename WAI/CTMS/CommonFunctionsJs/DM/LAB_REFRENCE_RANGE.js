//Lab Data
$(function () {

    if ($("#MainContent_divLabID").length != "0") {

        if ($("#MainContent_drpLab").children("option:selected").val() != '0') {
            $("#MainContent_drpLab").val($('.' + $('#MainContent_hdnLABID_VAR').val() + '').closest('tr').find('td').eq(4).find('input').val());
        }
    }

    $(".LabDefault").change(function () {

        var LabSUBJID = /[^:]*$/.exec($("#MainContent_lblSubjectId").text())[0].trim();
        var LabID = $("#MainContent_drpLab").children("option:selected").val();
        var LabTest = $('.LabDefault').val();

        //Get Query String Value
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });

        let VISIT_ID = params.VISITID;
        let MODULE_ID = params.MODULEID;

        $.ajax({
            type: "POST",
            url: "AjaxFunction_DM.aspx/GET_LAB_DATA",
            data: '{SUBJID: "' + LabSUBJID + '",LABID : "' + LabID + '",LBTEST: "' + LabTest + '",VISITID:"' + VISIT_ID + '",MODULEID:"' + MODULE_ID + '"}',
            dataType: "json",
            contentType: "application/json",
            success: function (res) {

                var xmlDoc = $.parseXML(res.d);
                var xml = $(xmlDoc);
                var tblLabData = xml.find("tblLabData");

                $(tblLabData).each(function () {

                    $('.' + $('#MainContent_hdnLABID_VAR').val() + '').closest('tr').find('td').eq(4).find('input').val($(this).find("LABID").text());
                    $('.' + $('#MainContent_hdnUNIT_VAR').val() + '').closest('tr').find('td').eq(4).find('input').val($(this).find("LBORRESU").text());
                    $('.' + $('#MainContent_hdnLL_VAR').val() + '').closest('tr').find('td').eq(4).find('input').val($(this).find("LBORNRLO").text());
                    $('.' + $('#MainContent_hdnUL_VAR').val() + '').closest('tr').find('td').eq(4).find('input').val($(this).find("LBORNRHI").text());

                });
            }
        });
    });
});

function GET_LAB_DATA() {

    var LabSUBJID = /[^:]*$/.exec($("#MainContent_lblSubjectId").text())[0].trim();
    var LabID = $("#MainContent_drpLab").children("option:selected").val();
    var LabTest = $('.LabDefault').val();

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let VISIT_ID = params.VISITID;
    let MODULE_ID = params.MODULEID;

    $.ajax({
        type: "POST",
        url: "AjaxFunction_DM.aspx/GET_LAB_DATA",
        data: '{SUBJID: "' + LabSUBJID + '",LABID : "' + LabID + '",LBTEST: "' + LabTest + '",VISITID:"' + VISIT_ID + '",MODULEID:"' + MODULE_ID + '"}',
        dataType: "json",
        contentType: "application/json",
        success: function (res) {

            var xmlDoc = $.parseXML(res.d);
            var xml = $(xmlDoc);
            var tblLabData = xml.find("tblLabData");

            $(tblLabData).each(function () {

                $('.' + $('#MainContent_hdnLABID_VAR').val() + '').closest('tr').find('td').eq(4).find('input').val($(this).find("LABID").text());
                $('.' + $('#MainContent_hdnUNIT_VAR').val() + '').closest('tr').find('td').eq(4).find('input').val($(this).find("LBORRESU").text());
                $('.' + $('#MainContent_hdnLL_VAR').val() + '').closest('tr').find('td').eq(4).find('input').val($(this).find("LBORNRLO").text());
                $('.' + $('#MainContent_hdnUL_VAR').val() + '').closest('tr').find('td').eq(4).find('input').val($(this).find("LBORNRHI").text());

            });

        }

    });

}
        //End Lab Data