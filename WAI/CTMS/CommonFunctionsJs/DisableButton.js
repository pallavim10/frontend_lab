function DisableButtons() {
    var inputs = document.getElementsByTagName("INPUT");
    for (var i in inputs) {
        if (inputs[i].type == "button" || inputs[i].type == "submit") {
            inputs[i].disabled = true;
          
        }
    }
}
window.onbeforeunload = DisableButtons;