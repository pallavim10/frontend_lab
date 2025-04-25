$(document).ready(function () {
    $("#hideshow").click(function () {
        if ($('#MainContent_SAE_grdQUERYDETAILS').hasClass("disp-none")) {
            $('#MainContent_SAE_grdQUERYDETAILS').removeClass("disp-none");
            $('#MainContent_SAE_grdQUERYDETAILS').addClass("disp-block");
            $("#hideshow").val("HIDE QUERY");

        }
        else {
            $('#MainContent_SAE_grdQUERYDETAILS').removeClass("disp-block");
            $('#MainContent_SAE_grdQUERYDETAILS').addClass("disp-none");
            $("#hideshow").val("SHOW QUERY");
        }
    });
});