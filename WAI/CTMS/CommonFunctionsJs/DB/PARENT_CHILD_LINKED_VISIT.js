function SetLinkedData() {

    $.ajaxSetup({ async: false });

    $(".ParentLinked").each(function () {

        var ParentVARIABLENAME = $(this).closest('tr').find('td').eq(0).text().trim();

        var childLinkedFields = $(".linked" + ParentVARIABLENAME + "").toArray();

        for (var a = 0; a < childLinkedFields.length; ++a) {

            (function (index) {

                var VariableName = $(childLinkedFields[a]).closest('tr').find('td').eq(0).text().trim();

                var childLinkedFieldsID = $(childLinkedFields[a]).attr('id');

                var childANS = $('#' + childLinkedFieldsID).next().val()

                if (childANS != '') {

                    $('#' + childLinkedFieldsID).val(childANS);

                }

            })(a);

        }
    });
}

function BindLinkedData() {

    $.ajaxSetup({ async: false });

    $(".ParentLinked").each(function () {

        var ParentVARIABLENAME = $(this).closest('tr').find('td').eq(0).text().trim();
        var ParentANS = $(this).val().trim();
        var VISITNUM = $("#MainContent_drpModuleVisit").val();

        var childLinkedFields = $(".linked" + ParentVARIABLENAME + "").toArray();

        for (var a = 0; a < childLinkedFields.length; ++a) {

            (function (index) {

                var VariableName = $(childLinkedFields[a]).closest('tr').find('td').eq(0).text().trim();

                var childLinkedFieldsID = $(childLinkedFields[a]).attr('id');

                var childANS = $('#' + childLinkedFieldsID).next().val()

                $('#' + childLinkedFieldsID).empty();

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction_DM.aspx/GetLinkedOptions",
                    data: '{VariableName: "' + VariableName + '",ParentANS : "' + ParentANS + '",ParentVARIABLENAME: "' + ParentVARIABLENAME + '",VISITNUM: "' + VISITNUM + '"}',
                    dataType: "json",
                    contentType: "application/json",
                    success: function (res) {
                        $.each(res.d, function (data, value) {

                            $('#' + childLinkedFieldsID).append($("<option></option>").val(value.VALUE).html(value.TEXT));
                        })
                    }

                });

            })(a);

        }
    });
}

$(function () {

    $(".ParentLinked").change(function () {

        var ParentVARIABLENAME = $(this).closest('tr').find('td').eq(0).text().trim();
        var ParentANS = $(this).val().trim();
        var VISITNUM = $("#MainContent_drpModuleVisit").val();

        var childLinkedFields = $(".linked" + ParentVARIABLENAME + "").toArray();

        for (var a = 0; a < childLinkedFields.length; ++a) {

            (function (index) {

                var VariableName = $(childLinkedFields[a]).closest('tr').find('td').eq(0).text().trim();

                var childLinkedFieldsID = $(childLinkedFields[a]).attr('id');

                $('#' + childLinkedFieldsID).empty();

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction_DM.aspx/GetLinkedOptions",
                    data: '{VariableName: "' + VariableName + '",ParentANS : "' + ParentANS + '",ParentVARIABLENAME: "' + ParentVARIABLENAME + '",VISITNUM: "' + VISITNUM + '"}',
                    dataType: "json",
                    contentType: "application/json",
                    success: function (res) {
                        $.each(res.d, function (data, value) {

                            $('#' + childLinkedFieldsID).append($("<option></option>").val(value.VALUE).html(value.TEXT));
                        })
                    }

                });

            })(a);

        }
    });

});