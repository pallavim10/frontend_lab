$(document).ready(function () {


    $(".numericdecimal").on("keypress keyup blur", function (event) {
        //this.value = this.value.replace(/[^0-9\.]/g,'');
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    $(".numeric").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^\d].+/, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    //only for numeric value
    $('.numeric').keydown(function (event) {
        // Allow only backspace and delete
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 ||
        event.keyCode == 96 || event.keyCode == 97 || event.keyCode == 98
        || event.keyCode == 99 || event.keyCode == 100 || event.keyCode == 101
          || event.keyCode == 102 || event.keyCode == 103 || event.keyCode == 104
            || event.keyCode == 105) {
            // let it happen, don't do anything
        }

        else {
            // Ensure that it is a number and stop the keypress
            if (event.keyCode < 48 || event.keyCode > 57) {
                event.preventDefault();
            }
        }
    });
    //only Alphabet value Accept
    $('.Alphabet').keydown(function (e) {

        //        if (e.shiftKey || e.ctrlKey || e.altKey) {
        //            e.preventDefault();
        //        } else {
        var key = e.keyCode;

        if (!((key == 8) || (key == 9) || (key == 173) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
            e.preventDefault();
            // }
        }
    });
    //only numeric and decimal value Accept
    $('.numericdecimal').keypress(function (event) {
        // Allow only backspace and delete
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
            // let it happen, don't do anything
            return true;
        }
        if (event.which < 46 || event.which >= 58 || event.which == 47) {
            event.preventDefault();
        }
        if (event.which == 46 && $(this).val().indexOf('.') != -1) {
            this.value = '';
        }
    });

    $('.txtDate').keypress(function (event) {
        if (event.keyCode == 9) {
            //let it happen, don't do anything
            return true;
        }
        event.preventDefault();
        return false;
    });

    $('.txtDateNoFuture').each(function (index, element) {
        $(element).pikaday({
            field: element,
            format: 'DD-MMM-YYYY',
            yearRange: [1910, 2050],
            maxDate: new Date()
        });
    });


    $('.Nothing').keypress(function (key) {
        if (event.keyCode == 9) {
            //let it happen, don't do anything
            return true;
        }
        event.preventDefault();
        return false;
    });

});       