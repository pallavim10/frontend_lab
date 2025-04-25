function showChild(element) {

    var Child_Val;
    if ($(element).hasClass("radio") || $(element).hasClass("checkbox")) {
        Child_Val = $(element).context.lastChild.textContent.trim();
    }
    else {
        if ($(element).val() != null) {
            Child_Val = $(element).val().trim();
        }
    }

    if (Child_Val == 'RAD_FIELD' || Child_Val == 'CHK_FIELD') {
        Child_Val = '';
    }


    var SelfTableName = $(element).closest('table').attr('id');
    var TableName = $($(element).closest('tr').next('tr').find('table').toArray()[0]).attr('id');


    var Upper = $("#" + TableName + "").find('tr').eq(0).find('td').eq(7).find('input').val();
    var Lower = $("#" + TableName + "").find('tr').eq(2).find('td').eq(7).find('input').val();

    if ($(element).hasClass("checkbox")) {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && TableName != SelfTableName) {

                if ($.trim($(this).find('td').eq(11).text().trim()) == "Is Not Blank") {
                    if ($($(element).context.getElementsByTagName("input")).prop('checked') == true) {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                        }

                        if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                            $(element).closest('tr').next('tr').removeClass('disp-none');
                        }
                    }
                    else {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                            $(this).addClass('disp-none');
                        }
                        else {
                            $(this).addClass('disp-none');
                        }
                    }

                }
                else if ($.trim($(this).find('td').eq(11).text().trim()) == "Compare") {

                    if (parseInt(Child_Val) > parseInt(Upper) || parseInt(Child_Val) < parseInt(Lower)) {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                        }

                        if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                            $(element).closest('tr').next('tr').removeClass('disp-none');
                        }
                    }
                    else {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                            $(this).addClass('disp-none');
                        }
                        else {
                            $(this).addClass('disp-none');
                        }
                    }

                }
                else if ($(this).find('td').toArray().length > 5) {
                    if (stringContains($.trim($(this).find('td').eq(11).text().trim()), Child_Val)) {
                        if ($($(element).context.getElementsByTagName("input")).prop('checked') == true) {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                            }
                            else {
                                $(this).addClass('disp-none');
                            }

                        }
                    }
                }
            }
        })
    }
    else {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && TableName != SelfTableName) {
                if ($(this).find('td').toArray().length > 5) {

                    if ($.trim($(this).find('td').eq(11).text().trim()) == "Is Not Blank") {
                        if (Child_Val != "") {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                                $(this).addClass('disp-none');
                            }
                            else {
                                $(this).addClass('disp-none');
                            }
                        }

                    }
                    else if ($.trim($(this).find('td').eq(11).text().trim()) == "Compare") {

                        if (parseInt(Child_Val) > parseInt(Upper) || parseInt(Child_Val) < parseInt(Lower)) {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                                $(this).addClass('disp-none');
                            }
                            else {
                                $(this).addClass('disp-none');
                            }
                        }

                    }
                    else if ($(this).find('td').toArray().length > 5) {
                        if (stringContains($.trim($(this).find('td').eq(11).text().trim()), Child_Val)) {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                                $(this).addClass('disp-none');
                            }
                            else {
                                $(this).addClass('disp-none');
                            }
                        }
                    }
                    else {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                            $(this).addClass('disp-none');
                        }
                        else {
                            $(this).addClass('disp-none');
                        }
                    }
                }
                else {
                    if ($(this).hasClass('disp-none')) {
                        $(this).removeClass('disp-none');
                    }
                }
            }
        })
    }
}

function stringContains(arrString, valString) {

    var arrMain = arrString.split('^');

    return (arrMain.indexOf(valString) > -1);
}

function showChild_ReadOnly(element) {

    var Child_Val;
    if ($(element).hasClass("radio") || $(element).hasClass("checkbox")) {
        Child_Val = $(element).context.lastChild.textContent.trim();
    }
    else {
        if ($(element).val() != null) {
            Child_Val = $(element).val().trim();
        }
    }

    if (Child_Val == 'RAD_FIELD' || Child_Val == 'CHK_FIELD') {
        Child_Val = '';
    }


    var SelfTableName = $(element).closest('table').attr('id');
    var TableName = $($(element).closest('tr').next('tr').find('table').toArray()[0]).attr('id');


    var Upper = $("#" + TableName + "").find('tr').eq(0).find('td').eq(7).find('input').val();
    var Lower = $("#" + TableName + "").find('tr').eq(2).find('td').eq(7).find('input').val();

    if ($(element).hasClass("checkbox")) {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && TableName != SelfTableName) {

                if ($.trim($(this).find('td').eq(13).text().trim()) == "Is Not Blank") {
                    if ($($(element).context.getElementsByTagName("input")).prop('checked') == true) {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                        }

                        if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                            $(element).closest('tr').next('tr').removeClass('disp-none');
                        }
                    }
                    else {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                            $(this).addClass('disp-none');
                        }
                        else {
                            $(this).addClass('disp-none');
                        }
                    }

                }
                else if ($.trim($(this).find('td').eq(13).text().trim()) == "Compare") {

                    if (parseInt(Child_Val) > parseInt(Upper) || parseInt(Child_Val) < parseInt(Lower)) {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                        }

                        if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                            $(element).closest('tr').next('tr').removeClass('disp-none');
                        }
                    }
                    else {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                            $(this).addClass('disp-none');
                        }
                        else {
                            $(this).addClass('disp-none');
                        }
                    }

                }
                else if ($(this).find('td').toArray().length > 5) {
                    if (stringContains($.trim($(this).find('td').eq(13).text().trim()), Child_Val)) {
                        if ($($(element).context.getElementsByTagName("input")).prop('checked') == true) {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                            }
                            else {
                                $(this).addClass('disp-none');
                            }

                        }
                    }
                }
            }
        })
    }
    else {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && TableName != SelfTableName) {
                if ($(this).find('td').toArray().length > 5) {

                    if ($.trim($(this).find('td').eq(13).text().trim()) == "Is Not Blank") {
                        if (Child_Val != "") {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                                $(this).addClass('disp-none');
                            }
                            else {
                                $(this).addClass('disp-none');
                            }
                        }

                    }
                    else if ($.trim($(this).find('td').eq(13).text().trim()) == "Compare") {

                        if (parseInt(Child_Val) > parseInt(Upper) || parseInt(Child_Val) < parseInt(Lower)) {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                                $(this).addClass('disp-none');
                            }
                            else {
                                $(this).addClass('disp-none');
                            }
                        }

                    }
                    else if ($(this).find('td').toArray().length > 5) {
                        if (stringContains($.trim($(this).find('td').eq(13).text().trim()), Child_Val)) {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                                $(this).addClass('disp-none');
                            }
                            else {
                                $(this).addClass('disp-none');
                            }
                        }
                    }
                    else {
                        if ($(this).hasClass('disp-none')) {
                            $(this).removeClass('disp-none');
                            $(this).addClass('disp-none');
                        }
                        else {
                            $(this).addClass('disp-none');
                        }
                    }
                }
                else {
                    if ($(this).hasClass('disp-none')) {
                        $(this).removeClass('disp-none');
                    }
                }
            }
        })
    }
}