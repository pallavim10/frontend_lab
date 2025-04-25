function setNavigationPath(element) {

    var Navigation_Path = $(element).next().val();

    $('#lblnavmenuuName').text(Navigation_Path);

    $.ajax({
        type: "POST",
        url: "AjaxFunction.aspx/setNavigationPath",
        data: '{NavigationPath: "' + Navigation_Path + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            return true;
        },
        failure: function (response) {
            return true;
        },
        error: function (response) {
            return true;
        }
    });

}