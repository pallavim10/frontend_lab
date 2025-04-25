$(document).ready(function () {
    var lastTabIndex = sessionStorage.getItem("lastTabIndex");
    console.log("Last saved tabindex at the top: " + lastTabIndex);

    if (lastTabIndex && !isNaN(lastTabIndex)) {
        console.log("Last saved tabindex if the condition qualifies: " + lastTabIndex);
        var nextInput = $(":input[tabindex='" + (parseInt(lastTabIndex) + 1) + "']");

        if (nextInput.length) {
            console.log("Last saved tabindex after the condition qualifies: " + lastTabIndex);
            nextInput.focus();
        } else {
            $(":input[tabindex='1']").focus(); // Default to first field
        }
    }

    // Capture Tab key event to store the correct tabindex
    $(":input:not(:hidden)").on("keydown", function (e) {
        if (e.key === "Tab" || e.keyCode === 9) {
            var currentTabIndex = parseInt($(this).attr("tabindex"));
            console.log("Tab pressed on tabindex: " + currentTabIndex);
            sessionStorage.setItem("lastTabIndex", currentTabIndex);
        }
    });
});
