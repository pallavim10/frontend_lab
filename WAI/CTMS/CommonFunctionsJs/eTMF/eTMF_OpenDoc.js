

function OpenDoc(element) {

    var SysFileName = $(element).closest('tr').find('td:eq(1)').text().trim();
    var DOCID = $(element).closest('tr').find('td:eq(0)').text().trim();

    const supportedExtensions = [
        "html", "htm", "css", "js", "json", "xml", "csv", "pdf",
        "txt", "md", "jpg", "jpeg", "png", "gif", "webp", "svg",
        "mp4", "webm", "ogg", "mp3", "wav"
    ];

    var fileExtension = SysFileName.split('.').pop().toLowerCase();

    if (supportedExtensions.includes(fileExtension)) {

        $.ajax({
            type: "POST",
            url: "AjaxFunction.aspx/INSERT_DOC_OPEN_LOGS",
            data: '{DOCID: "' + DOCID + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.d == 'Object reference not set to an instance of an object.') {
                    alert("Session Expired");
                    var url = "SessionExpired.aspx";
                    $(location).attr('href', url);
                }
            },
            failure: function (response) {
                if (response.d == 'Object reference not set to an instance of an object.') {
                    alert("Session Expired");
                    var url = "SessionExpired.aspx";
                    $(location).attr('href', url);
                }
            }
        });

        var test = "eTMF_Docs/" + SysFileName + "#toolbar=0";

        var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=1200";
        window.open(test, '_blank', strWinProperty);
        return false;
    }
    else {
        alert('This file can not be opened in the browser, please click on Download.');
        return false;
    }
}