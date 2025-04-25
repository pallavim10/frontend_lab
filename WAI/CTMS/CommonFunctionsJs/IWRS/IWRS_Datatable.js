
$(function () {
    $(".Datatable").dataTable({
        "bSort": true,
        "ordering": false,
        "bDestroy": true,
        "lengthMenu": [[10, 25, 50, "All"]],
        stateSave: true,
        fixedHeader: true
    });
});