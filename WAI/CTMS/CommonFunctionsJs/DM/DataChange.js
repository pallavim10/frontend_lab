function DATA_Changed(element) {

    $(element).closest('td').find("input[id*='HDN_FIELD']").val($(element).val());

    var btn = $(element).closest('td').find('.btnDATA_Changed');
    if (btn.length > 0) {
        btn[0].click(); // ✅ Use DOM `.click()` instead of `__doPostBack`
    }
}

function UpdateChangeData(VARIABLENAME, Value, CONTROLTYPE) {

    if (CONTROLTYPE == "CHECKBOX" || CONTROLTYPE == "RADIOBUTTON") {
        $('.' + SETFIELDID).closest('td').find('span:textEquals(' + Value + ')').find('input').prop('checked', true);
    }
    else {
        $('.' + VARIABLENAME + '').val(Value);
    }
}

//$(document).ready(function () {

//    $('.rightClick').mousedown(function (event) {
//        switch (event.which) {
//            case 3:
//                //checkOnChangeCrit(this);
//                DATA_Changed_RightClick(this);
//                break;
//        }
//    });

//});

function DATA_Changed_RightClick(element) {

    //Get Query String Value
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });

    let DELETED = params.DELETED;

    if (DELETED != "1") {

        if ($(element).hasClass("Mandatory") == true) {
            $(element).addClass("brd-1px-redimp");
            return false;
        }

        var pageStatus = $("#MainContent_hdn_PAGESTATUS").val();
        var Freezed = $("#MainContent_hdnFreezeStatus").val();
        var Locked = $("#MainContent_hdnLockStatus").val();

        if (pageStatus == "1" && Freezed != "1" && Locked != "1" && $("#MainContent_hdnRECID").val() != "-1") {


            if ($(element).hasClass('radio') || $(element).hasClass('checkbox')) {
                // console.log('Radio or checkbox class found.')
                /*  $(element).closest('td').find('input').prop('checked', false);*/
                $(element).closest('td').find('input[type="checkbox"], input[type="radio"]').prop('checked', false);
            }
            else {
                $(element).val('');
            }


            var prevTd = $(element).closest('td').prev('td');


            // Find the first control inside the previous <td>
            var control = prevTd.find('input, button, select, textarea')
                .filter('input[type="checkbox"], input[type="radio"], input[type="text"], input[type="password"], button, select, textarea')
                .first();

            console.log("Found Control:", control);
            var currentTd = control.closest('td'); // Get the current <td> of the radio button

            if (control.length > 0) {
                var controlValue = control.val(); // Get control value
                var controlType = ""; // Initialize control type variable
                var isMandatory = false; // Initialize mandatory check flag

                // Determine the type of control
                if (control.is('input[type="checkbox"]')) {
                    controlType = "Checkbox";

                    // Locate the entire Repeater container
                    var repeaterContainer = control.closest('table'); // Adjust if needed based on your HTML

                    // Get all checkboxes inside the Repeater
                    var allCheckboxes = repeaterContainer.find('input[type="checkbox"]');

                    // Check if at least one checkbox inside the Repeater is checked
                    var anyChecked = allCheckboxes.filter(':checked').length > 0;

                    // Find the closest <td> for highlighting
                    var currentTd = control.closest('td');

                    // Check if the checkbox is inside a Mandatory span
                    var checkboxSpan = control.closest('span.checkbox');
                    isMandatory = checkboxSpan.hasClass('Mandatory');

                    if (isMandatory) {
                        if (!anyChecked) {
                            // No checkbox is selected → Show error
                            currentTd.addClass("brd-1px-redimp");
                            alert("The checkbox is mandatory and must be checked.");
                            return false;
                        } else {
                            // At least one checkbox is selected → Show success message
                            currentTd.addClass("brd-1px-redimp");
                            alert("The checkbox is mandatory and has a value.");
                            return false;
                        }
                    }
                } if (control.is('input[type="radio"]')) {
                    controlType = "Radio Button";

                    // Locate the entire Repeater container
                    var repeaterContainer = control.closest('table'); // Adjust if needed based on your HTML

                    // Get all radio buttons inside the Repeater
                    var allRadioGroups = repeaterContainer.find('input[type="radio"]');

                    // Check if any radio button inside the Repeater is checked
                    var anyChecked = allRadioGroups.filter(':checked').length > 0;

                    // Find the closest <td> for highlighting
                    var currentTd = control.closest('td');

                    // Check if the radio button is inside a Mandatory span
                    var radioSpan = control.closest('span.radio');
                    isMandatory = radioSpan.hasClass('Mandatory');

                    if (isMandatory) {
                        if (!anyChecked) {
                            // No radio button is selected → Show error
                            currentTd.addClass("brd-1px-redimp");
                            alert("The Radio Button is mandatory and must be checked.");
                            return false;
                        } else {
                            // At least one radio button is selected → Show success message
                            currentTd.addClass("brd-1px-redimp");
                            alert("The Radio Button is mandatory and has a value.");
                            return false;
                        }
                    }
                } else if (control.is('input[type="text"], input[type="password"]')) {
                    controlType = "Text Input";
                    isMandatory = control.hasClass('Mandatory');

                } else if (control.is('button')) {
                    controlType = "Button";

                } else if (control.is('select')) {
                    controlType = "Dropdown (Select)";
                    isMandatory = control.hasClass('Mandatory');

                } else if (control.is('textarea')) {
                    controlType = "Textarea";
                    isMandatory = control.hasClass('Mandatory');

                    var editorID = control.attr("id");
                    var editorContent = "";
                    var editorInstance = null;

                    // ✅ Check if CKEditor is being used
                    if (isCKEditor(control)) {
                        editorInstance = CKEDITOR.instances[editorID];
                        editorContent = editorInstance.getData().trim(); // ✅ CKEditor HTML content (trimmed)

                        controlType = "CKEditor";

                        // ✅ Apply highlighting if blank
                        if (isMandatory && editorContent === "") {
                            $(editorInstance.container.$).addClass("brd-1px-redimp"); // Highlight CKEditor
                            alert("The Textarea field is mandatory and must not be blank.");
                            return false;
                        } else {
                            $(editorInstance.container.$).addClass("brd-1px-redimp"); // Highlight CKEditor
                            alert("The Textarea field is mandatory and has a value.");
                            return false;
                        }
                    }

                    // ✅ Regular Textarea
                    else {
                        editorContent = control.val().trim();

                        if (isMandatory && editorContent === "") {
                            control.addClass("brd-1px-redimp"); // Highlight normal textarea
                            alert("The Textarea field is mandatory and must not be blank.");
                            return false;
                        } else {
                            control.addClass("brd-1px-redimp");
                            alert("The Textarea field is mandatory and has a value.");
                            return false;
                        }
                    }
                }




                // Validate mandatory text inputs, dropdowns, and text areas
                if (isMandatory) {
                    if (controlValue === '' || controlValue === null) {
                        control.addClass("brd-1px-redimp");
                        alert("The " + controlType + " is a mandatory field.");
                    } else {
                        control.addClass("brd-1px-redimp");
                        alert("The " + controlType + " is mandatory and has a value.");
                    }
                    return false;
                }

                // If all validations pass, trigger the button click
                $(element).closest('td').find('.btnRightClick_Changed').click();
                return true;
            }

        }
        else {

            // Get the previous <td> of the current <td>
            var prevTd = $(element).closest('td').prev('td');
            var iframe = prevTd.find('iframe');

            // Check if the previous <td> contains a checkbox
            if (prevTd.find('input[type="checkbox"]').length > 0) {
                prevTd.find('input[type="checkbox"]').prop('checked', false);
            }

            // Check if the previous <td> contains a radio button
            else if (prevTd.find('input[type="radio"]').length > 0) {
                prevTd.find('input[type="radio"]').prop('checked', false);

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
            else if (prevTd.find('textarea').length > 0) {

                if (iframe.length > 0) {
                    // Access the body inside the iframe and clear its content
                    var iframeBody = iframe.contents().find('body');
                    iframeBody.html(''); // Clear the content of html > body
                }
                else {
                    prevTd.find('textarea').val('');
                }

            }
            // Check if the previous <td> contains a Textarea
            else if (prevTd.find('select').length > 0) {
                prevTd.find('select').prop('selectedIndex', 0); // Reset to the first option
            }
            else {
                // Clear any input element in the previous <td>
                prevTd.find('input').val('');
            }


            showChild(element);
            return false;
        }

    }
    else {
        return false;
    }
}


function isCKEditor(element) {
    var editorID = element.attr("id");
    return editorID && CKEDITOR.instances[editorID] !== undefined;
}

function Check_ReasonEntered() {

    if ($('#MainContent_drp_Reason').val() == '0') {
        alert('Please select reason');
        return false;
    }
    else if ($('#MainContent_drp_Reason').val() != '0' && $('#hdnQuery_OverrideId').val().trim() != '' && $('#MainContent_drpAction').val() == '0') {
        alert('Please select reason for manual query');
        return false;
    }
    else if ($('#MainContent_drp_Reason').val() != '0' && $('#hdnQuery_OverrideId').val().trim() != '' && $('#MainContent_drpAction').val() == 'Other' && $('#MainContent_txt_OverrideComm').val().trim() == '') {
        alert('Please enter comment for manual query');
        return false;
    }
    else {
        if ($('#MainContent_drp_Reason').val() == 'Other' && $('#MainContent_txt_Comments').val().trim() == '') {
            alert('Please enter comment');
            return false;
        }
        else {
            return true;
        }
    }

};


function Check_CommentEntered() {

    if ($('#MainContent_txt_Comments').val().trim() == '') {
        alert('Please enter comment');
        return false;
    }
    else {
        return true;
    }
};

function drpAction_Change_DataEntry() {

    if ($("#MainContent_drpAction")[0].value.trim() == "Other") {
        $("#MainContent_OverrideComments").removeClass("disp-none");
    }
    else {
        $("#MainContent_OverrideComments").addClass("disp-none");
    }
}