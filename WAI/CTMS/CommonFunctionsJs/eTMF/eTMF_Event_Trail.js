
function Event_Trail(element) {

    var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

    var test = "eTMF_Event_Trail.aspx?ID=" + ID;

    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=800";
    window.open(test, '_blank', strWinProperty);
    return false;
}