function showChild(element) {

    var Child_Val;

    if ($(element).closest('tr').hasClass('disp-none')) {
        Child_Val = "";
    }
    else {
        if ($(element).hasClass("radio") || $(element).hasClass("checkbox")) {
            Child_Val = $(element).text().trim();
        }
        else {
            if ($(element).val() != null) {
                Child_Val = $(element).val().trim();
            }
        }
    }

    var SelfTableName = $(element).closest('table').attr('id');
    var TableName = $($(element).closest('tr').next('tr').find('table').toArray()[0]).attr('id');

    if ($(element).hasClass("checkbox")) {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && $(this).closest('table').attr('id') == TableName) {

                if ($.trim($(this).find('td').eq(5).text().trim()) == "Is Not Blank") {
                    if ($($(element).find("input")).prop('checked') == true) {
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
                else if ($.trim($(this).find('td').eq(5).text().trim()) == "Compare") {

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
                    if (stringContains($.trim($(this).find('td').eq(5).text().trim()), Child_Val)) {
                        if ($($(element).find("input")).prop('checked') == true) {
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
            if (TableName != 'MainContent_grd_Data' && $(this).closest('table').attr('id') == TableName) {
                if ($(this).find('td').toArray().length > 5) {

                    if ($.trim($(this).find('td').eq(5).text().trim()) == "Is Not Blank") {
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
                    else if ($.trim($(this).find('td').eq(5).text().trim()) == "Compare") {

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
                        if (stringContains($.trim($(this).find('td').eq(5).text().trim()), Child_Val)) {
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