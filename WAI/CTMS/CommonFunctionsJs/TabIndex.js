$(document).ready(function () {
    var pageKey = window.location.pathname; // Unique key for each page
    var lastTabIndex = sessionStorage.getItem("lastTabIndex_" + pageKey);
    var isPageRefresh = performance.navigation.type === 1 || window.performance.getEntriesByType("navigation")[0].type === "reload";

    console.log("Last saved tabindex for " + pageKey + ": " + lastTabIndex);

    // 🔹 Clear sessionStorage on page refresh or new page load
    if (isPageRefresh || !document.referrer.includes(window.location.hostname)) {
        console.log("Page refreshed or new page loaded - Clearing session storage");
        sessionStorage.removeItem("lastTabIndex_" + pageKey);
        lastTabIndex = null;
    }

    // 🔹 Focus logic
    if (lastTabIndex && !isNaN(lastTabIndex)) {
        var nextInput = $(":input[tabindex='" + (parseInt(lastTabIndex) + 1) + "']");
        if (nextInput.length) {
            nextInput.focus();
        } else {
            $(":input[tabindex='1']").focus();
        }
    } else {
        $(":input[tabindex='1']").focus();
    }

    // 🔹 Store the last focused control's tabindex before postback
    $(":input:not(:hidden)").on("focus", function () {
        sessionStorage.setItem("lastTabIndex_" + pageKey, $(this).attr("tabindex"));
    });
});

// 🔹 Handle AJAX postbacks if using UpdatePanel in ASP.NET
if (typeof Sys !== "undefined" && Sys.WebForms) {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        var pageKey = window.location.pathname;
        var lastTabIndex = sessionStorage.getItem("lastTabIndex_" + pageKey);

        if (lastTabIndex && !isNaN(lastTabIndex)) {
            var nextInput = $(":input[tabindex='" + (parseInt(lastTabIndex) + 1) + "']");
            if (nextInput.length) {
                nextInput.focus();
            } else {
                $(":input[tabindex='1']").focus();
            }
        }
    });
}
