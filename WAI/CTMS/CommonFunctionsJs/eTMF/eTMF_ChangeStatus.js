
function ChangeStatus(element) {

    var row = element.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;

    var URL = $(element).closest('tr').find('td:eq(8)').find('input').val();

    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=400";
    window.location.href = URL;
    return false;
}