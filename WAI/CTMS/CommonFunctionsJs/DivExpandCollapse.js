function divexpandcollapse(divname) {
    var div = document.getElementById(divname);
    var img = document.getElementById('img' + divname);

    if (div.style.display == "none") {
        div.style.display = "inline";
        document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

    } else {
        div.style.display = "none";
        document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
    }
}

function ManipulateAll(ID) {
    var img = document.getElementById('img' + ID);

    if (img.className == 'icon-plus-sign-alt') {
        img.className = 'icon-minus-sign-alt'
        $("div[id*='" + ID + "']").css("display", "inline");
        $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
        $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
    } else {
        img.className = 'icon-plus-sign-alt'
        $("div[id*='" + ID + "']").css("display", "none");
        $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
        $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
    }
}