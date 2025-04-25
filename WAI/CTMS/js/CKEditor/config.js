/**
* @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
* For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
};

for (var i in CKEDITOR.instances) {
    (function (i) {
        CKEDITOR.instances[i].on('change', function () {
           

            CKEDITOR.instances[i].updateElement();
           

            if ($('#' + CKEDITOR.instances[i].name).closest('td').find('input:hidden').eq(2).val() != $('#' + CKEDITOR.instances[i].name).val()) {

                if ($("#MainContent_hdn_PAGESTATUS").val() == '1') {

                    $('#MainContent_bntSaveComplete').prop('disabled', true);
                    $('#MainContent_btnSaveIncomplete').prop('disabled', true);

                }

                if ($("#MainContent_hfDM_PAGESTATUS").val() == '1' && $("#MainContent_hfDM_RECID").val() != "-1") {

                    $('#MainContent_bntSaveComplete').prop('disabled', true);
                    $('#MainContent_btnSaveIncomplete').prop('disabled', true);

                }
            }

        });
    })(i);
}

for (var i in CKEDITOR.instances) {
    (function (i) {
        CKEDITOR.instances[i].on('blur', function () {
            
            CKEDITOR.instances[i].updateElement();

            if ($('#' + CKEDITOR.instances[i].name).closest('td').find('input:hidden').eq(2).val() != $('#' + CKEDITOR.instances[i].name).val()) {

                $('#' + CKEDITOR.instances[i].name + '').change();

            }

        });
    })(i);
}


for (var i in CKEDITOR.instances) {
    
    (function (i) {
        CKEDITOR.instances[i].on('focus', function () {



            if ($("#MainContent_hdn_PAGESTATUS").val() == '1') {
                var targetElement = $(this.element.$); // ✅ Wrap in jQuery
                console.log("Current Element:", targetElement);

                var $currentTd = targetElement.closest("td"); // ✅ No undefined error
                console.log("Current TD:", $currentTd);

                var $parentRow = targetElement.closest("tr"); // ✅ Correctly find <tr>
                console.log("Parent Row:", $parentRow);

                var isTextInput = false; // Flag for text input

                var editorID = this.name; // ✅ Get correct CKEditor instance ID
                var editorInstance = CKEDITOR.instances[editorID];

                if (editorInstance) {
                    var editorContent = editorInstance.getData().trim();

                    if (editorContent !== "") {
                        isTextInput = true;
                        console.log("CKEditor has content. Disabling LinkButtons...");
                    }
                }

                // ✅ Disable all <a> links inside the table
                // $("table").find("a").css({ "pointer-events": "none", "opacity": "0.5" });

                $("table").find("a").each(function () {
                    $(this).css({ "pointer-events": "none", "opacity": "0.5" });
                });

                $('#MainContent_bntSaveComplete').prop('disabled', true);
                $('#MainContent_btnSaveIncomplete').prop('disabled', true);
                $('#MainContent_btnModuleStatus').prop('disabled', true);


            }

            if ($("#MainContent_hfDM_PAGESTATUS").val() == '1' && $("#MainContent_hfDM_RECID").val() != "-1") {
                var targetElement = $(this.element.$); // ✅ Wrap in jQuery
                console.log("Current Element:", targetElement);

                var $currentTd = targetElement.closest("td"); // ✅ No undefined error
                console.log("Current TD:", $currentTd);

                var $parentRow = targetElement.closest("tr"); // ✅ Correctly find <tr>
                console.log("Parent Row:", $parentRow);

                var isTextInput = false; // Flag for text input

                var editorID = this.name; // ✅ Get correct CKEditor instance ID
                var editorInstance = CKEDITOR.instances[editorID];

                if (editorInstance) {
                    var editorContent = editorInstance.getData().trim();

                    if (editorContent !== "") {
                        isTextInput = true;
                        console.log("CKEditor has content. Disabling LinkButtons...");
                    }
                }

                // ✅ Disable all <a> links inside the table
                // $("table").find("a").css({ "pointer-events": "none", "opacity": "0.5" });

                $("table").find("a").each(function () {
                    $(this).css({ "pointer-events": "none", "opacity": "0.5" });
                });

                $('#MainContent_bntSaveComplete').prop('disabled', true);
                $('#MainContent_btnSaveIncomplete').prop('disabled', true);
                $('#MainContent_btnModuleStatus').prop('disabled', true);


            }
            
          
          
            CKEDITOR.instances[i].updateElement();

               $('#' + CKEDITOR.instances[i].name + '').mouseup();

           /* $('#' + editorID).trigger("mouseup");*/ // ✅ Use `.trigger("mouseup")` instead of `.mouseup()`

   
        });
    })(i);
}


