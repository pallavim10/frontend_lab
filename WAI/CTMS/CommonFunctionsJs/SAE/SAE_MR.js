function FillMRDetails() {
    var count = 0;    

    $("#MainContent_grMRDETAILS tr").each(function () {
        if (count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val();
            var MRYN = $(this).find('td:eq(1)').find('input').val();
            var hfelement = $(".chkMR_" + variableName).closest('td').find('input:hidden').attr('id');

            if (MRYN == "False") {

                $(".chkMR_" + variableName).closest('td').find('input').prop("checked", false);
                $("#" + hfelement).val("False");

            }
            else if (MRYN == "True") {

                $(".chkMR_" + variableName).closest('td').find('input').prop("checked", true);
                $("#" + hfelement).val("True");

            }

        }
        count++;
    });
}