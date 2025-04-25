
function EDIT_DOCUMENTS(element) {

    var row = element.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;

    var DocID = $(element).closest('tr').find('td:eq(0)').find('span').text();

    var test = "eTMF_MODIFY_DOCS.aspx?ID=" + DocID;

    window.location.href = test;
    return false;

}