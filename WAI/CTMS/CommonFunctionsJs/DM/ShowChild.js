function showChild(element) {

    var Child_Val;
    if ($(element).hasClass("radio") || $(element).hasClass("checkbox")) {
        Child_Val = $(element).text().trim();
    }
    else {
        if ($(element).val() != null) {
            Child_Val = $(element).val().trim();
        }
    }

    if (Child_Val == 'RAD_FIELD' || Child_Val == 'CHK_FIELD') {
        Child_Val = '';
    }

    var TableName = $($(element).closest('tr').next('tr').find('table').toArray()[0]).attr('id');

    if ($(element).hasClass("checkbox")) {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && $(this).closest('table').attr('id') == TableName) {

                if ($.trim($(this).find('td').eq(12).text().trim()) == "Is Not Blank") {
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
                            ClearElmentData($(this));
                            $(this).addClass('disp-none');
                        }
                    }

                }
                else if ($(this).find('td').toArray().length > 5) {
                    if (stringContains($.trim($(this).find('td').eq(12).text().trim()), Child_Val)) {
                        if ($($(element).find("input")).prop('checked') == true) {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        } else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                                $(this).addClass('disp-none');
                            }
                            else {
                                ClearElmentData($(this));
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

                    if ($.trim($(this).find('td').eq(12).text().trim()) == "Is Not Blank") {
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
                                ClearElmentData($(this));
                                $(this).addClass('disp-none');
                            }
                        }

                    }
                    else if ($(this).find('td').toArray().length > 5) {
                        //commented by reena as on 8-1-1025
                        if (stringContains($.trim($(this).find('td').eq(12).text().trim()), Child_Val)) {
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
                                ClearElmentData($(this));
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
                            ClearElmentData($(this));
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

function ClearElmentData(element) {

    var pageStatus = $("#MainContent_hdn_PAGESTATUS").val();

    if (pageStatus != "1") {
        // Get the previous <td> of the current <td>
        var prevTd = $(element).closest('td').prev('td');
        var iframe = $(element).find('iframe');

        // Check if the previous <td> contains a checkbox
        if ($(element).find('input[type="checkbox"]').length > 0) {
            $(element).find('input[type="checkbox"]').prop('checked', false);
        }

        // Check if the previous <td> contains a radio button
        else if ($(element).find('input[type="radio"]').length > 0) {
            $(element).find('input[type="radio"]').prop('checked', false);

            // Get the current <tr> and then find the next <tr>
            var nextTr = $(element).closest('tr').next('tr');

            // Check if the next <tr> exists
            if (nextTr.length > 0) {

                // Check for input[type="text"] in the next <tr>
                var textInputs = nextTr.find('input[type="text"]');

                // Check for dropdown (select) elements in the next <tr>
                var dropdowns = nextTr.find('select');

                if (textInputs.length > 0) {
                    // Clear the value of all text inputs in the next <tr>
                    textInputs.val('');
                }
                if (dropdowns.length > 0) {
                    // Reset the dropdown to the first option (or clear selection)
                    dropdowns.prop('selectedIndex', 0); // Reset to the first option
                }

            }
        }
        // Check if the previous <td> contains a Textarea
        else if ($(element).find('textarea').length > 0) {

            if (iframe.length > 0) {
                // Access the body inside the iframe and clear its content
                var iframeBody = iframe.contents().find('body');
                iframeBody.html(''); // Clear the content of html > body
            }
            else {
                $(element).find('textarea').val('');
            }

        }
        // Check if the previous <td> contains a Textarea
        else if ($(element).find('select').length > 0) {
            $(element).find('select').prop('selectedIndex', 0); // Reset to the first option
        }
        else if ($(element).find('input').val() != "") {
            // Clear any input element in the previous <td>
            $(element).find('input').val('');
        }
    }
}

function stringContains(arrString, valString) {

    var arrMain = arrString.split('¸');

    return (arrMain.indexOf(valString) > -1);
}

function showChild_ReadOnly(element) {

    var Child_Val;
    if ($(element).hasClass("radio") || $(element).hasClass("checkbox")) {
        Child_Val = $(element).text().trim();
    }
    else {
        if ($(element).val() != null) {
            Child_Val = $(element).val().trim();
        }
    }

    if (Child_Val == 'RAD_FIELD' || Child_Val == 'CHK_FIELD') {
        Child_Val = '';
    }

    var TableName = $($(element).closest('tr').next('tr').find('table').toArray()[0]).attr('id');

    if ($(element).hasClass("checkbox")) {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && $(this).closest('table').attr('id') == TableName) {

                if ($.trim($(this).find('td').eq(12).text().trim()) == "Is Not Blank") {
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
                            ClearElmentData($(this));
                            $(this).addClass('disp-none');
                        }
                    }

                }
                else if ($(this).find('td').toArray().length > 5) {
                    if (stringContains($.trim($(this).find('td').eq(12).text().trim()), Child_Val)) {
                        if ($($(element).find("input")).prop('checked') == true) {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        } else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                                $(this).addClass('disp-none');
                            }
                            else {
                                ClearElmentData($(this));
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
                            ClearElmentData($(this));
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
                        ClearElmentData($(this));
                        $(this).addClass('disp-none');
                    }
                }
            }
        })
    }
    else {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && $(this).closest('table').attr('id') == TableName) {
                if ($(this).find('td').toArray().length > 5) {

                    if ($.trim($(this).find('td').eq(12).text().trim()) == "Is Not Blank") {
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
                                ClearElmentData($(this));
                                $(this).addClass('disp-none');
                            }
                        }

                    }
                    else if ($(this).find('td').toArray().length > 5) {
                        //commented by reena as on 8-1-1025
                        if (stringContains($.trim($(this).find('td').eq(12).text().trim()), Child_Val)) {
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
                                ClearElmentData($(this));
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
                            ClearElmentData($(this));
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

function showChild_Review(element) {

    var Child_Val;
    if ($(element).hasClass("radio") || $(element).hasClass("checkbox")) {
        Child_Val = $(element).text().trim();
    }
    else {
        if ($(element).val() != null) {
            Child_Val = $(element).val().trim();
        }
    }

    if (Child_Val == 'RAD_FIELD' || Child_Val == 'CHK_FIELD') {
        Child_Val = '';
    }

    var TableName = $($(element).closest('tr').next('tr').find('table').toArray()[0]).attr('id');

    if ($(element).hasClass("checkbox")) {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && $(this).closest('table').attr('id') == TableName) {

                if ($.trim($(this).find('td').eq(6).text().trim()) == "Is Not Blank") {
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
                            ClearElmentData($(this));
                            $(this).addClass('disp-none');
                        }
                    }

                }
                else if ($(this).find('td').toArray().length > 5) {
                    if (stringContains($.trim($(this).find('td').eq(6).text().trim()), Child_Val)) {
                        if ($($(element).find("input")).prop('checked') == true) {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }

                            if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                $(element).closest('tr').next('tr').removeClass('disp-none');
                            }
                        } else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                                $(this).addClass('disp-none');
                            }
                            else {
                                ClearElmentData($(this));
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
                            ClearElmentData($(this));
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
                        ClearElmentData($(this));
                        $(this).addClass('disp-none');
                    }
                }
            }
        })
    }
    else {
        var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
            if (TableName != 'MainContent_grd_Data' && $(this).closest('table').attr('id') == TableName) {
                if ($(this).find('td').toArray().length > 5) {

                    if ($.trim($(this).find('td').eq(6).text().trim()) == "Is Not Blank") {
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
                                ClearElmentData($(this));
                                $(this).addClass('disp-none');
                            }
                        }

                    }
                    else if ($(this).find('td').toArray().length > 5) {
                        //commented by reena as on 8-1-1025
                        if (stringContains($.trim($(this).find('td').eq(6).text().trim()), Child_Val)) {
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
                                ClearElmentData($(this));
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
                            ClearElmentData($(this));
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
