$(document).ready(function () {
    $("#hideshow").click(function () {
        if ($('#MainContent_grdQUERYDETAILS').hasClass("disp-none")) {
            $('#MainContent_grdQUERYDETAILS').removeClass("disp-none");
            $('#MainContent_grdQUERYDETAILS').addClass("disp-block");
            $("#hideshow").val("HIDE QUERY");

        }
        else {
            $('#MainContent_grdQUERYDETAILS').removeClass("disp-block");
            $('#MainContent_grdQUERYDETAILS').addClass("disp-none");
            $("#hideshow").val("SHOW QUERY");
        }
    });
});