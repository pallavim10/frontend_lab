function checkOnChangeCrit(element) {

    $.ajaxSetup({ async: false });

    var variablename = $(element).closest('tr').find('td').eq(0).text().trim();

    var msg = true;

    $("#MainContent_SAE_grdOnPageSpecs tr").each(function () {

        if (msg == true) {

            if ($(this).find('td').length > 0) {

                var grd_VARIABLENAME = $(this).find('td').eq(7).find('input').val();

                var Formulas = '', RESTRICTED = '', ERR_MSG = '', SET_VALUE_DATA = '', CritCode = '';

                var ISDERIVED = $(this).find('td').eq(3).find('input').val();

                if (ISDERIVED == 'True') {
                    Formulas = $(this).find('td').eq(4).find('input').val().replace("'[", '[').replace("]'", ']');
                    CritCode = $(this).find('td').eq(0).find('input').val();
                }
                else {
                    Formulas = $(this).find('td').eq(0).find('input').val().replace("'[", '[').replace("]'", ']');
                }

                if (grd_VARIABLENAME == variablename || Formulas.indexOf(variablename) != -1) {

                    var SETFIELDID = $(this).closest('tr').find('td').eq(5).find('input').val().trim();

                    var formualArr = Formulas.split('[');

                    for (i = 1; i < formualArr.length; i++) {

                        if (formualArr[i].indexOf(']') != -1) {

                            var DerVariablename = formualArr[i].substring(0, formualArr[i].indexOf("]"));

                            var DerVALUE = '';

                            if ($('.' + DerVariablename).hasClass('radio')) {

                                DerVALUE = $('.' + DerVariablename).closest('td').find('.' + DerVariablename + ' :checked').next().text();

                            }
                            else {
                                DerVALUE = $('.' + DerVariablename + '').val();
                            }

                            $.ajax({
                                type: "POST",
                                url: "AjaxFunction.aspx/CheckDatatype",
                                data: '{Val: "' + DerVALUE + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    if (data.d == 'Object reference not set to an instance of an object.') {
                                        alert("Session Expired");
                                        var url = "SessionExpired.aspx";
                                        $(location).attr('href', url);
                                    }
                                    else {
                                        if (data.d != undefined) {

                                            Formulas = Formulas.replace('[' + DerVariablename + ']', "" + data.d + "");

                                        }
                                    }
                                },
                                failure: function (response) {
                                    if (response.d == "Object reference not set to an instance of an object.") {
                                        alert("Session Expired");
                                        var url = "SessionExpired.aspx";
                                        $(location).attr('href', url);
                                    }
                                    else {
                                        alert("Contact administrator not suceesfully updated")
                                    }

                                }
                            });

                        }
                    }

                    if (CritCode != '') {

                        var CritCodeArr = CritCode.split('[');

                        for (i = 1; i < CritCodeArr.length; i++) {

                            if (CritCodeArr[i].indexOf(']') != -1) {

                                var CritCodeVariablename = CritCodeArr[i].substring(0, CritCodeArr[i].indexOf("]"));

                                var CritCodeVALUE = '';

                                if ($('.' + CritCodeVariablename).hasClass('radio')) {

                                    CritCodeVALUE = $('.' + CritCodeVariablename).closest('td').find('.' + CritCodeVariablename + ' :checked').next().text();

                                }
                                else {
                                    CritCodeVALUE = $('.' + CritCodeVariablename + '').val();
                                }

                                if (CritCodeVALUE != undefined) {

                                    CritCode = CritCode.replace('[' + CritCodeVariablename + ']', "'" + CritCodeVALUE + "'");

                                }

                            }
                        }
                    }

                    if (ISDERIVED == 'True') {

                        $.ajax({
                            type: "POST",
                            url: "AjaxFunction_SAE.aspx/EXECUTE_FORMULA",
                            data: '{FORMULA: "' + Formulas + '",CritCode:"' + CritCode + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == 'Object reference not set to an instance of an object.') {
                                    alert("Session Expired");
                                    var url = "SessionExpired.aspx";
                                    $(location).attr('href', url);
                                }
                                else {
                                    if ($('.' + SETFIELDID).hasClass('radio')) {

                                        $('.' + SETFIELDID).closest('td').find('span:textEquals(' + data.d + ')').find('input').prop('checked', true);

                                    }
                                    else {
                                        $('.' + SETFIELDID + '').val(data.d);
                                    }

                                    return true;
                                }
                            },
                            failure: function (response) {
                                if (response.d == 'Object reference not set to an instance of an object.') {
                                    alert("Session Expired");
                                    var url = "SessionExpired.aspx";
                                    $(location).attr('href', url);
                                }
                                else {
                                    alert(data.d);
                                }
                            }
                        });

                    }
                    else {

                        ERR_MSG = $(this).find('td').eq(1).find('input').val();

                        RESTRICTED = $(this).find('td').eq(2).find('input').val();

                        SET_VALUE_DATA = $(this).find('td').eq(6).find('input').val();

                        $.ajax({
                            type: "POST",
                            url: "AjaxFunction_SAE.aspx/CHECK_CONDITION",
                            data: '{FORMULA: "' + Formulas + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                if (data.d == 'Object reference not set to an instance of an object.') {
                                    alert("Session Expired");
                                    var url = "SessionExpired.aspx";
                                    $(location).attr('href', url);
                                }
                                else {
                                    if (data.d == '0') {

                                        msg = true;
                                    }
                                    else if (data.d == '1') {

                                        if (ERR_MSG != '' && grd_VARIABLENAME == variablename) {

                                            alert(ERR_MSG);
                                        }

                                        if (SET_VALUE_DATA != '') {

                                            if ($('.' + SETFIELDID).hasClass('radio')) {

                                                $('.' + SETFIELDID).closest('td').find('span:textEquals(' + SET_VALUE_DATA + ')').find('input').prop('checked', true);

                                            }
                                            else {
                                                $('.' + SETFIELDID + '').val(SET_VALUE_DATA);
                                            }
                                        }

                                        if (RESTRICTED == 'True' && grd_VARIABLENAME == variablename) {

                                            if ($(element).closest('tr').find('td').find("input[id*='HDN_OLD_VALUE']").val() != "") {

                                                $(element).val($(element).closest('tr').find('td').find("input[id*='HDN_OLD_VALUE']").val());
                                                $(element).focus();
                                            }
                                            else {

                                                $(element).val('');
                                                $(element).focus();
                                            }

                                            callChange();
                                            msg = false;
                                        }
                                        else {
                                            msg = true;
                                        }
                                    }
                                }
                            },
                            failure: function (response) {
                                if (response.d == 'Object reference not set to an instance of an object.') {
                                    alert("Session Expired");
                                    var url = "SessionExpired.aspx";
                                    $(location).attr('href', url);
                                }
                                else {
                                    alert(data.d);
                                }
                            }
                        });
                    }

                }
            }

        }
    });

    if (msg == true) {

        DATA_Changed(element);

    }
}