//onfocus of any control this function will call    
function myFocus() {
  
    if ($("#MainContent_hdn_PAGESTATUS").val() == '1') {

        $('#MainContent_bntSaveComplete').prop('disabled', true);
        $('#MainContent_btnSaveIncomplete').prop('disabled', true);
        $('#MainContent_btnModuleStatus').prop('disabled', true);
        $('#MainContent_btnDeleteData').prop('disabled', true);
        $('#MainContent_lblApplicableStatus').prop('disabled', true);
        $('#MainContent_btnNotApplicable').prop('disabled', true);

        var targetElement = document.activeElement; // Get the focused element
        console.log("Current Element:", targetElement);

        var currentTd = $(targetElement).closest("td"); // Find the closest <td>
        console.log("Current TD:", currentTd);

        var parentRow = $(targetElement).closest("tr"); // Find the parent <tr>
        console.log("Parent Row:", parentRow);


        // ✅ Disable all controls inside the same <td>
        currentTd.find("input, select, textarea, button, a").each(function () {
            var $control = $(this);
            var tag = $control.prop("tagName").toLowerCase();
            var type = $control.attr("type") ? $control.attr("type").toLowerCase() : "";
        });

        //// ✅ Find the first visible control the user is interacting with
        //var activeControl = null;
        var isTextInput = false; // Flag for text input

        if ($(targetElement).is("input[type='text'], input[type='number'], input[type='email'], input[type='password'], textarea, select, input[type='radio'], input[type='checkbox']") && $(targetElement).is(":visible")) {
            activeControl = targetElement;
            console.log("Active Control Detected: ", targetElement.tagName,
                "ID:", $(targetElement).attr("id"),
                "Type:", $(targetElement).attr("type"),
                "Value:", $(targetElement).val());


            //   ✅ Check if the control is a textbox and user has entered some text
            if ($(targetElement).is("input[type='text'], textarea") && $(targetElement).val().trim() !== "") {
                isTextInput = true;
                console.log("Textbox is NOT empty. Disabling LinkButtons...");
            }

            if ($(targetElement).is("select") && $(targetElement).val() !== "") {
                isTextInput = true;
                console.log("Textbox is NOT empty. Disabling LinkButtons...");
            }

            if ($(targetElement).is("input[type='checkbox'], input[type='radio']") && $(targetElement).is(":checked")) {
                isTextInput = true;
                console.log("Checkbox/Radiobutton is NOT empty. Disabling LinkButtons...");
            }

            // ✅ Handle CKEditor Content
            if (isCKEditor(targetElement)) {
                var editorID = $(targetElement).attr("id");
                var editorInstance = CKEDITOR.instances[editorID];

                if (editorInstance) {
                    var editorContent = editorInstance.getData().trim();

                    if (editorContent !== "") {
                        isTextInput = true;
                        console.log("CKEditor has content. Disabling LinkButtons...");
                    }
                }
            }
        }

        // ✅ Disable ALL LinkButtons in EVERY row
        $("table").find("a").each(function () {
            $(this).css({ "pointer-events": "none", "opacity": "0.5" });
        });
    }

    if ($("#MainContent_hdn_PAGESTATUS").val() == '1' && $("#MainContent_hdnRECID").val() != "-1") {
        
        $('#MainContent_bntSaveComplete').prop('disabled', true);
        $('#MainContent_btnSaveIncomplete').prop('disabled', true);
        $('#MainContent_btnModuleStatus').prop('disabled', true);
        $('#MainContent_btnDeleteData').prop('disabled', true);
        $('#MainContent_lblApplicableStatus').prop('disabled', true);
        $('#MainContent_btnNotApplicable').prop('disabled', true);
    }
}

function isCKEditor(element) {
        var editorID = element.attr("id");
        return editorID && CKEDITOR.instances[editorID] !== undefined;
    }



