$(function () {

    $('.select').select2();

    $('.txtTime').inputmask(
        "hh:mm", {
        placeholder: "HH:MM",
        insertMode: false,
        showMaskOnHover: false,
        hourFormat: "24"
    }
    );

    $('.numericdecimal').inputmask({
        clearIncomplete: true
        //allowLeadingZero: true,
        //autoGroup: false,
        //rightAlign: false,
        //clearIncomplete: true,
        //onBeforePaste: function (pastedValue) {
        //    return pastedValue.replace(/[^0-9.]/g, ''); // Remove invalid characters on paste
        //}
        
    });

$('.txtDate').each(function (index, element) {
    $(element).pikaday({
        field: element,
        format: 'DD-MMM-YYYY',
        yearRange: [1910, 2050]
    });
});

$('.txtDateNoFuture').each(function (index, element) {
    $(element).pikaday({
        field: element,
        format: 'DD-MMM-YYYY',
        yearRange: [1910, 2050],
        maxDate: new Date()
    });
});

$('.txtuppercase').keyup(function () {
    $(this).val($(this).val().toUpperCase());
});

$('.txtuppercase').keydown(function (e) {

    var key = e.keyCode;
    if (key === 189 && e.shiftKey === true) {
        return true;
    }
    else if ((key == 189) || (key == 109)) {
        return true;
    }
    else if (e.ctrlKey || e.altKey) {
        e.preventDefault();
    }
    else {
        if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
            e.preventDefault();
        }
    }

});

$(".numeric").on("keypress keyup blur", function (event) {
    $(this).val($(this).val().replace(/[^\d].+/, ""));
    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

//only for numeric value
$('.numeric').keypress(function (event) {

    if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
        || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
        // let it happen, don't do anything
        return true;
    }
    else {
        event.preventDefault();
    }
});

$('.txtDate').keypress(function (event) {
    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
        //let it happen, don't do anything
        return true;
    }
    event.preventDefault();
    return false;
});

$('.txtDateNoFuture').keypress(function (event) {
    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
        //let it happen, don't do anything
        return true;
    }
    event.preventDefault();
    return false;
});
});


function RadioCheck(rb) {
    //            var gv = document.getElementById($(rb).closest('table').attr('id'));
    var rbs = rb.parentNode.parentNode.parentNode.parentNode.getElementsByTagName("input");
    for (var i = 0; i < rbs.length; i++) {
        if (rbs[i].type == "radio") {
            if (rbs[i].checked && rbs[i] != rb) {
                rbs[i].checked = false;
                break;
            }
        }
    }
}

function tab(e) {
    if (e.which == 13) {
        e.preventDefault();
    }
}