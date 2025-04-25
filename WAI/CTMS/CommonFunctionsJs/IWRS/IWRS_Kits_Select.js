
function UpdateSelectedKits(element) {

    var btnIndex = $(element).closest('th').index();
    var kitIndex = $(element).closest('tr').find('th:contains(Kit)').index();

    var selectedKitNos = "";

    var table = $(element).closest('table').DataTable();

    table.rows().every(function () {
        var $row = $(this.node());
        var checkbox = $($row.find('td')[btnIndex]).find('input');
        var KitNumber = $($row.find('td')[kitIndex]).find('span').text();

        if (checkbox.length > 0 && KitNumber.length > 0 && checkbox.prop('checked')) {
            selectedKitNos += "," + KitNumber;
        }
    });

    $('#<%= hfKITS.ClientID %>').val(selectedKitNos);

}