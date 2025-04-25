function FillSDVDetails() {
    var count = 0;

    $("#MainContent_grSDVDETAILS tr").each(function () {
        if (count > 0) {

            var variableName = $(this).find('td:eq(0)').find('input').val();
            var SDVYN = $(this).find('td:eq(1)').find('input').val();
            var hfelement = $(".chkSDV_" + variableName).closest('td').find('input:hidden').attr('id');

            if (SDVYN == "False") {

                $(".chkSDV_" + variableName).closest('td').find('input').prop("checked", false);
                $("#" + hfelement).val("False");

            }
            else if (SDVYN == "True") {

                $(".chkSDV_" + variableName).closest('td').find('input').prop("checked", true);
                $("#" + hfelement).val("True");

            }

        }
        count++;
    });
}