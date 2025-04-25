

function VERSION_HISTORY(element) {

    var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

    var test = "eTMF_VERSION_HISTORY.aspx?ID=" + ID;

    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=1200";
    window.open(test, '_blank', strWinProperty);
    return false;
}

function DOCUMENT_HISTORY(element) {

    var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

    var test = "eTMF_Doc_History.aspx?ID=" + ID;

    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=1200";
    window.open(test, '_blank', strWinProperty);
    return false;
}