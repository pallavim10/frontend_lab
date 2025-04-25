
//cls-btnSkipNext - > btnSkipNext 
//cls-btnSaveNext - > btnSaveNext
//cls-btnClose    - > btnClose
//cls-btnSave     - > btnSave
//cls-btnCancel   - > btnCancel
//cls-btnDelete   - > btnDelete
//cls-DisabledOne - > apply this class for copy button those pages having validate contrain one(1)
//cls-DisabledTwo - > apply this class for copy button those pages having validate contrain one(2)
//cls-DisabledThree - > apply this class for copy button those pages having validate contrain one(3)
//cls-btnSave-Validate -> btnSave - > apply this class save data in contrain one(1)
//cls-UpperCase    -> upper case letters apply char -id 
//cls-enabled
//cls-topFilter    -> use master page involve infotypes
//cls-DisabledControl      -> use all control disabled controls
//cls-EnabledControls
//cls-DecNumber            -> use number and dot -> decimal allow
//cls-CommaNumber          -> use number and comma -> decimal allow

var asInitVals = new Array();
var oTable = null;
var arrTr = [];
var currActDvId = "#fragment-1";
var currActDvId1 = "#fmnt-1"; // for birthday message


//$(function () {
//    $('#txtNumeric').keydown(function (e) {

//    });
//});

var varShowDateTimePicker = false;



$(document).ready(function () {


    //$("input[class*=cls-btnNext]").addClass("fa fa-forward");
    //$("input[class*=cls-btnPrev]").addClass("fa fa-backward");
    PreventCaretKey();

    $(document).on('keydown', 'input[class*=cls-CommaNumber]', function (event) {
        if (event.shiftKey == true) {
            event.preventDefault();
        }

        if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 8 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {

        } else {
            event.preventDefault();
        }

        if ($(this).val().indexOf(',') !== -1)
            event.preventDefault();

    });

    $(document).on('keydown', 'input[class*=cls-DecNumber]', function (event) {
        if (event.shiftKey == true) {
            event.preventDefault();
        }

        if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 8 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 46 || event.keyCode == 190) {

        } else {
            event.preventDefault();
        }

        if ($(this).val().indexOf('.') !== -1 && event.keyCode == 190)
            event.preventDefault();

    });

    $(document).on('keydown', 'input[class*=cls-LetterOnly]', function (e) {
        //if (e.shiftKey || e.ctrlKey || e.altKey) {
        //    e.preventDefault();
        //} else {
        //    var key = e.keyCode;
        //    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
        //        e.preventDefault();
        //    }
        //}
        if (e.shiftKey || e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                e.preventDefault();
            }
        }

        /// $("input[class*=cls-UpperCase").val(($("input[class*=cls-UpperCase").val()).max());
    });

    //$("select").addClass("select2");

    $("input[class*=cls-UpperCase").on('keyup', function (e) {
        if (e.which >= 97 && e.which <= 122) {
            var newKey = e.which - 32;
            // I have tried setting those
            e.keyCode = newKey;
            e.charCode = newKey;
        }

        $("input[class*=cls-UpperCase").val(($("input[class*=cls-UpperCase").val()).toUpperCase());
    });

    //inetger validation
    $(document).on('keydown', 'input[class*=integer]', function (e) {

       
        -1 !== $.inArray(e.keyCode, [46, 8, 9, 27, 13, 110])
                    || /65|67|86|88/.test(e.keyCode) && (!0 === e.ctrlKey || !0 === e.metaKey)
                        || 33 <= e.keyCode && 40 >= e.keyCode
                                || (e.shiftKey || 48 > e.keyCode || 57 < e.keyCode) && (96 > e.keyCode || 105 < e.keyCode) && e.preventDefault()
        
    });
        
    $(document).on('keyup', 'input[class*=required]', function () {


        var value = $(this).val();

        if (value == "" || value == null)
            $(this).addClass("brd-1px-redimp");
        else
            $(this).removeClass("brd-1px-redimp");

       
        //.css("border", "2px solid red");

    });



    $(document).on('textarea[class*=required]', function () {
        var value = $(this).val();

        if (value == "")
            $(this).addClass("brd-1px-redimp");
        else
            $(this).removeClass("brd-1px-redimp");
        //.css("border", "2px solid red");
       // $(this).setCursorPosition(1);
    });

    //datepicker 
    $(document).on('change', 'input[class*=datePicker]', function () {
        var value = $(this).val();

        if (value == "")
            $(this).addClass("brd-1px-redimp");
        else
            $(this).removeClass("brd-1px-redimp");
    })


    //timepicker

    $(document).on('change', 'input[class*=timePicker]', function () {
        var value = $(this).val();

        if (value == "")
            $(this).addClass("brd-1px-redimp");
        else
            $(this).removeClass("brd-1px-redimp");
    })


    $(document).on('keyup', 'textarea[class*=required]', function () {
        var value = $(this).val();

        if (value == "")
            $(this).addClass("brd-1px-redimp");
        else
            $(this).removeClass("brd-1px-redimp");
        //.css("border", "2px solid red");
    });



    $(document).on('change', 'select[class*=required]', function () {
        //var value = $(this).val();
        //alert('in change');
        $(".MainContains").find("select[class*=required]").each(function () {
            var value = $(this).val(); // remove and apply red highlight for each select
            if ($(this).attr("disabled") == "disabled") {
                if (value == "" || value == "0"|| value == null) {
                    $(this).addClass("brd-1px-redimp");
                    //$('#form1').validationEngine('validate');
                }
                else
                    $(this).removeClass("brd-1px-redimp");
            }
            else {
                if (value == "" || value == "0" || value == null)                
                    $(this).addClass("brd-1px-redimp");
                else
                    
                    $(this).removeClass("brd-1px-redimp");

            }

        });
        //.css("border", "2px solid red");
    });



    $(document).on("keydown", function (e) {
        if (e.which === 8 && !$(event.target).is("input, textarea")) {
            e.preventDefault();
        }
    });


    $(document).on("keydown", function (e) {
        //alert('in keydown event');
        if ((e.which === 13) && ($(e.target).is("input[type=text]"))) {

            e.preventDefault();
        }
    });

  
  
    //$(document).on('click', 'btn[class*=glyphicon-eye-open]', function () {
    //    highLightRequiredFields();
    //});

    //$(document).on('click', 'btn[class*=glyphicon-edit]', function () {
    //    highLightRequiredFields();
    //});

});

function InitializeDataTable(noColumns, pageName, arrCols) {


    if ($('.grdDataTable').find("tr").length > 0) {
        oTable = $(".grdDataTable").DataTable({
            //dom: "<'row'<'col-sm-6'l><'col-sm-4'B>>tp",
            dom: "<'row'<'col-sm-6'l><'col-sm-4'B><'col-sm-6'f>><'row'<'col-sm-12'tr>><'row'<'col-sm-5'i><'col-sm-7'p>>",
            //"dom": '<lf<t>ip>',
            "columnDefs": [{
                "width": "120px !important",
                "targets": [0],
                "searchable": false,
                "orderable": false
            }],
            "order": [[1, "desc"]],
            "bFilter": true,
            "destroy": true,
            //"bPaginate": true,
            //"bProcessing": true,
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            "pagingType": "full_numbers",
            buttons: [

                {

                    className: 'btn-sm fa fa-plus-circle grdBtnPlus',
                    titleAttr: 'Add Records',
                    action: function (e, dt, node, config) {


                        //ClearContents();

                        console.log($(".panel-body").is(":visible"));
                        if ($("div[class*=panel-body]").hasClass("disp-none")) {
                            $("div[class*=panel-body]").toggle("slow");
                            $("div[class*=panel-body]").removeClass("disp-none");
                            console.log("panel-body visible");

                            $("a[class*=grdBtnPlus]").removeClass("fa-plus-circle");
                            $("a[class*=grdBtnPlus]").addClass("fa-minus-circle");


                        }
                        else {
                            console.log("panel-body not visible");
                            $("div[class*=panel-body]").toggle("slow");
                            $("div[class*=panel-body]").addClass("disp-none");
                            $("a[class*=grdBtnPlus]").removeClass("fa-minus-circle");
                            $("a[class*=grdBtnPlus]").addClass("fa-plus-circle");

                            ClearContents();
                            $(".cls-disabledOne").attr("disabled", "disabled");
                            $(".cls-disabledOne").val("31-12-9999");

                        }

                        callHighlightFields();

                        return false;
                        // $("div[class*=panel-body").toggle("slow");

                        //  addDiv(();
                    }
                },
               {

                   className: 'btn-sm fa fa-refresh',
                   titleAttr: 'Refresh',
                   action: function (e, dt, node, config) {

                       location.reload(true);
                   }
               },
               {
                   extend: '', className: 'btn-sm fa fa-files-o', titleAttr: 'Copy Records',
                   action: function (e, dt, node, config) {


                       console.log('in click event of copy');
                       var ct = 0;
                       $(".grdDataTable").find("tr").find("td:eq(0)").each(function () {
                           if ($(this).hasClass("bgLightBlue")) {
                               ct = ct + 1;
                               arrTr.push($(this).parent("tr").index());
                           }
                       });

                       console.log("array length - " + arrTr.length);

                       for (var i = 0; i < arrTr.length; i++) {
                           console.log("arr value - " + arrTr[i]);
                       }




                       if (ct == 0) {
                           var img = "<i class='fa fa-times' aria-hidden='true'></i>";
                           errorAlert(img + ' ' + 'Error', "Please select at least one record to copy");
                       }
                       else {




                           $("#hdnTblArrayIndex").val("0");
                           DisplayRecords(arrTr[0]);


                           $("div[class*=panel-body]").each(function () {
                               $(this).removeClass("disp-none");
                               $(this).addClass("margin-top30");
                           });


                           $("#divPanel").addClass("divPopup");
                           if ($("div[class*=panel-body]").find("#topFilter")) {
                               $("#divPanel").addClass("height60per");
                               $(".copyPanel").addClass("height95per overflow-y-auto margin-top20");
                               $("div[id*=topFilter]").removeClass("disp-block");
                               $("div[id*=topFilter]").addClass("disp-none");
                               $("div[class*=panel-body]").removeClass("margin-top-30imp");
                               $("div[class*=panel-body]").addClass("margin-top-10imp");
                           }
                           else {
                               $("#divPanel").addClass("height60per overflow-y-auto");
                           }
                           //$("#divPanel").addClass("");
                           //$("#divPanel").addClass("");
                           //$(".cls-btnSave-Validate").addClass("disp-noneimp");
                           $(".cls-btnSave").addClass("disp-noneimp");
                           $(".cls-btnCancel").addClass("disp-noneimp");
                           $(".cls-btnSaveNext").removeClass("disp-noneimp");
                           $(".cls-btnSkipNext").removeClass("disp-noneimp");
                           $(".cls-btnClose").removeClass("disp-noneimp");
                           $(".cls-btnDelete").addClass("disp-noneimp");




                           $("div[class*=panel-body]").find("input").each(function () {
                               $(this).removeAttr("readonly", "true");
                           });

                           $("div[class*=panel-body]").find("input").each(function () {
                               $(this).removeAttr("disabled", "disabled");
                           });

                           $("div[class*=panel-body]").find("select").each(function () {
                               $(this).removeAttr("readonly", "true");
                           });


                           $("div[class*=panel-body]").find("select").each(function () {
                               $(this).removeAttr("disabled", "disabled");
                           });

                           $("div[class*=panel-body]").find("checkbox").each(function () {
                               $(this).removeAttr("disabled", "disabled");
                           });

                           $("div[class*=panel-body]").find("textarea").each(function () {
                               $(this).removeAttr("disabled", "disabled");
                           });

                           $(".cls-disabledOne").attr("disabled", "disabled");
                           $(".cls-disabledOne").val("31-12-9999");

                           $(".cls-disabledmessageid").attr("disabled", "disabled");

                           if (arrTr.length == 1)
                               $(".cls-btnSkipNext").attr("disabled", "disabled");

                           $("input[class*=cls-DisabledControl]").attr("disabled", "disabled");//disabled control amruta 22-08-2016
                           $("div[class*=panel-body]").find(".cls-DisabledControl").attr("disabled", "disabled");//disabled control amruta 2-09-2016


                       }
                       console.log("count - " + ct);

                       //role menu action mapping use this condition chnages by amruta 
                       if ($("select[id*=drpMenuId]") != "") {

                       }
                       else {
                           callHighlightFields();//common function in all pages  change by amruta 
                       }
                       numberRangeCurrentNumber();
                       if (document.URL.indexOf("1003PositionData.aspx") != -1)
                           positionCheckIsBudgetted();//add function check position checkbox check uncheck copy functionality  -> amruta -> 31-08-2016
                       //  $("table[class*=grdDataTable]").attr("disabled", "disabled");

                   }
               },
               {

                   className: 'btn-sm fa fa-search',
                   titleAttr: 'Search',

                   action: function (e, dt, node, config) {
                       $(".SearchHide").toggle("slow");
                       //$("tr.SearchHide").find("th:first").addClass("width170pximp");
                       //  $(".panel .table th:first").addClass("");
                   }
               },

             { extend: 'copy', className: 'btn-sm fa fa-clipboard disp-noneimp', exportOptions: { columns: arrCols }, titleAttr: 'Copy To Clipboard' },//amruta change 6 aug 16
             { extend: 'csv', title: pageName, className: 'btn-sm disp-noneimp' },
             { extend: 'excel', title: 'eClue - ' + pageName, className: 'btn-sm fa fa-file-excel-o', exportOptions: { columns: arrCols }, titleAttr: 'Save As Excel' },
             { extend: 'pdf', title: 'eClue - ' + pageName, className: 'btn-sm fa fa-file-pdf-o', exportOptions: { columns: arrCols }, titleAttr: 'Save As PDF' },
             { extend: 'print', title: 'eClue - ' + pageName,
                 //one line display coloumns and specially date format one line table -> print format amruta chnage 5 oct 2016 start
                 customize: function (win) {
                 $(win.document.body)
                     .css( 'font-size', '10pt' )                     
 
                 $(win.document.body).find( 'table' )
                     .addClass( 'compact' )
                     .css('font-size', 'inherit');
                     //one line display coloumns and specially date format one line table -> print format amruta chnage 5 oct 2016 end
             }, exportOptions: { columns: arrCols }, titleAttr: 'Print', className: 'btn-sm fa fa fa-print margin-right10imp' },

            ]
        });

        var row = '<tr class="SearchHide"><th></th>'
        var cols = '';

        for (var i = 0; i < noColumns; i++) {
            cols = cols + '<th><input name="search_engine" class="search_init" /></th>';
        }

        row = row + cols + "</tr>";


        $('.grdDataTable  > thead > tr:first').before(row);

        $(".SearchHide").toggle("fast");

        // Apply the filter
        $(".grdDataTable thead input").on('keyup change', function () {
            oTable
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        $("table[class*=grdDataTable]").wrap("<div class='overflow-x-auto overFlowLine margin-left-15 margin-right41'></div>")
    }


    // oTable.find('thead th').css('width', 'auto');
}

//*--------------------Initialize Datatable for infotypes/Transactions---------------------*//
function InitializeDataTableForInfotype(noColumns, pageName, arrCols) {
    if ($('.grdDataTable').find("tr").length > 0) {
        oTable = $(".grdDataTable").DataTable({
            dom: "<'row'<'col-sm-6'l><'col-sm-4'B><'col-sm-6'f>><'row'<'col-sm-12'tr>><'row'<'col-sm-5'i><'col-sm-7'p>>",
            "columnDefs": [{
                "targets": [0],
                "searchable": false,
                "orderable": false
            }],
            "order": [[1, "desc"]],
            "bFilter": true,
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            buttons: [
                {

                    className: 'btn-sm fa fa-minus-circle grdBtnPlus',
                    titleAttr: 'Add Records',
                    action: function (e, dt, node, config) {

                        //$(".panel-body").toggle("slow");
                        //highLightRequiredFields();
                        callHighlightFields();

                        console.log($(".panel-body").is(":visible"));
                        if ($("div[class*=panel-body]").hasClass("disp-none")) {
                            $("div[class*=panel-body]").toggle("slow");
                            $("div[class*=panel-body]").removeClass("disp-none");
                            console.log("panel-body visible");

                            $("a[class*=grdBtnPlus]").removeClass("fa-plus-circle");
                            $("a[class*=grdBtnPlus]").addClass("fa-minus-circle");
                            // ClearContents();
                        }
                        else {
                            console.log("panel-body not visible");
                            $("div[class*=panel-body]").toggle("slow");
                            $("div[class*=panel-body]").addClass("disp-none");
                            $("a[class*=grdBtnPlus]").removeClass("fa-minus-circle");
                            $("a[class*=grdBtnPlus]").addClass("fa-plus-circle");

                            ClearContents();
                            $(".cls-disabledOne").attr("disabled", "disabled");
                            $(".cls-disabledOne").val("31-12-9999");
                        }

                        return false;
                        // $("div[class*=panel-body").toggle("slow");

                        //  addDiv(();
                    }
                },
               {

                   className: 'btn-sm fa fa-refresh',
                   titleAttr: 'Refresh',
                   action: function (e, dt, node, config) {

                       location.reload(true);
                   }
               },
               {
                   extend: '', className: 'btn-sm fa fa-files-o', titleAttr: 'Copy Records',
                   action: function (e, dt, node, config) {
                       console.log('in click event of copy');
                       var ct = 0;
                       $(".grdDataTable").find("tr").find("td:eq(0)").each(function () {
                           if ($(this).hasClass("bgLightBlue")) {
                               ct = ct + 1;
                               arrTr.push($(this).parent("tr").index());
                               // $(this).attr('disabled', 'disabled');
                           }
                       });


                       console.log("array length - " + arrTr.length);

                       for (var i = 0; i < arrTr.length; i++) {
                           console.log("arr value - " + arrTr[i]);
                       }



                       if (ct == 0) {
                           var img = "<i class='fa fa-times' aria-hidden='true'></i>";
                           errorAlert(img + ' ' + 'Error', "Please select at least one record to copy");
                       }
                       else {
                           $("#hdnEdit").val("0");//amruta 22-08-2016

                           $('div[class*=table-responsive]').find('a, input, table,select').each(function () {
                               $(this).attr('disabled', 'disabled');
                               $('div[class*=grdDataTable]').find("a[class*=glyphicon]").attr('disabled', 'disabled');
                           });


                           $("#hdnTblArrayIndex").val("0");
                           DisplayRecords(arrTr[0]);



                           $("div[class*=panel-body]").each(function () {
                               $(this).removeClass("disp-none");
                           });


                           $("#divPanel").addClass("divPopup");
                           $("#divPanel").addClass("height60per");
                           $(".copyPanel").addClass("height95per overflow-y-auto margin-top20");
                           //$("#divPanel").append($('<div />', {
                           //    'class': 'cls-insideClass height95per overflow-y-auto margin-top20'
                           //}));

                           //var $DivInsideClass = $("div[class*=cls-insideClass]");
                           //$("#divPanel").find("div[class*=panel-body]").each(function () {
                           //    $($DivInsideClass).append($(this));
                           //});
                           //$(".cls-btnClose .panel-body .margin-top-30imp").wrap("<div class='height60per overflow-x-auto'></div>");
                           //$(".cls-btnSave-Validate").addClass("disp-noneimp");
                           $(".cls-btnSave").addClass("disp-noneimp");
                           $(".cls-btnCancel").addClass("disp-noneimp");
                           $(".cls-btnSaveNext").removeClass("disp-noneimp");
                           $(".cls-btnSkipNext").removeClass("disp-noneimp");
                           $(".cls-btnClose").removeClass("disp-noneimp");
                           $(".cls-btnDelete").addClass("disp-noneimp");
                           // $(".cls-btnAddNew").addClass("disp-noneimp");



                           $("div[class*=panel-body]").find("input").each(function () {
                               $(this).removeAttr("readonly", "true");
                           });

                           $("div[class*=panel-body]").find("input").each(function () {
                               $(this).removeAttr("disabled", "disabled");
                           });

                           $("div[class*=panel-body]").find("select").each(function () {
                               $(this).removeAttr("readonly", "true");
                           });


                           $("div[class*=panel-body]").find("select").each(function () {
                               $(this).removeAttr("disabled", "disabled");
                           });

                           $("div[class*=panel-body]").find("checkbox").each(function () {
                               $(this).removeAttr("disabled", "disabled");
                           });

                           $("div[class*=panel-body]").find("textarea").each(function () {
                               $(this).removeAttr("disabled", "disabled");
                           });

                           $(".cls-disabledOne").attr("disabled", "disabled");
                           $(".cls-disabledOne").val("31-12-9999");

                           //$(".cls-DisabledThree").val("31-12-9999");

                           $(".cls-disabledmessageid").attr("disabled", "disabled");

                           if (arrTr.length == 1)
                               $(".cls-btnSkipNext").attr("disabled", "disabled");

                           $("select[class*=cls-disableControl").attr("disabled", "disabled");

                       }
                       console.log("count - " + ct);

                       callHighlightFields();
                   }

               },
               {
                   className: 'btn-sm fa fa-search',
                   titleAttr: 'Search',
                   action: function (e, dt, node, config) {
                       $(".SearchHide").toggle("slow");
                   }
               },

             { extend: 'copy', className: 'btn-sm fa fa-clipboard disp-noneimp', exportOptions: { columns: arrCols }, titleAttr: 'Copy To Clipboard' },//amruta change 18-8 -16 
             { extend: 'csv', title: pageName, className: 'btn-sm disp-noneimp' },
             { extend: 'excel', titile: pageName, className: 'btn-sm fa fa-file-excel-o', exportOptions: { columns: arrCols }, titleAttr: 'Save As Excel' },
             { extend: 'pdf', title: 'eClue - ' + pageName, className: 'btn-sm fa fa-file-pdf-o', exportOptions: { columns: arrCols }, titleAttr: 'Save As PDF' },
             { extend: 'print', title: 'eClue - ' + pageName, exportOptions: { columns: arrCols }, titleAttr: 'Print', className: 'btn-sm fa fa fa-print margin-right10imp' },

            ]
        });

        var row = '<tr class="SearchHide"><th class="width200pximp"></th>'
        var cols = '';

        for (var i = 0; i < noColumns; i++) {
            cols = cols + '<th><input name="search_engine" class="search_init" /></th>';
        }

        row = row + cols + "</tr>";


        $('.grdDataTable  > thead > tr:first').before(row);

        $(".SearchHide").toggle("fast");

        // Apply the filter
        $(".grdDataTable thead input").on('keyup change', function () {
            oTable
                .column($(this).parent().index() + ':visible')
                .search(this.value)
                .draw();
        });

        $("table[class*=grdDataTable]").wrap("<div class='overflow-x-auto overFlowLine margin-left-15 margin-right41'></div>")
    }
    // oTable.find('thead th').css('width', 'auto');
}
//-----------------------------------------------------------------------------------------//
//*--------------------Chk Grid Length and show hide body --------------------------------*//
function ShowHideBodyControls() {
    var rowCount = $(".grdDataTable tr").length;
    if (rowCount == 0) {
        $("div[class*=panel-body]").each(function () {
            $(this).removeAttr("disp-none");

            // $(this).addClass("disp-block");
        });
        //enabledControls();
    }
    else {
        if (localStorage.getItem("act") == null) {
            $("div[class*=panel-body]").each(function () {
                //   $(this).removeClass("disp-block");
                $(this).addClass("disp-none");
            });
        }
    }
}

//-----------------------------------------------------------------------------------------//
function numberRangeCurrentNumber() {
    if ($("select[id*=drpNumberRangeType]").val() == "PAIN" || $("select[id*=drpNumberRangeType]").val() == "OMIN") {
        $("input[id*=txtCurrentNumber]").attr("disabled", "disabled");
        $("input[id*=btnCurrentNumber]").removeClass("disp-noneimp");
        $("input[id*=btnCurrentNumber]").addClass("disp-inlineblockimp");
    }
    else {
        $("input[id*=txtCurrentNumber]").attr("disabled", "disabled");

        $("input[id*=btnCurrentNumber]").removeClass("disp-inlineblockimp");
        $("input[id*=btnCurrentNumber]").addClass("disp-noneimp");

    }
    callHighlightFields();

}

function positionCheckIsBudgetted() {
    if ($("input[id*=hdnViewCheckID]").val() === "1") {

        if ($("input[id*=chkIsBudgetted]").is(':checked') == true) {
            $("input[id*=txtBudgetedCost]").removeAttr("disabled", "disabled");
            $("select[id*=drpCurrencyCode]").removeAttr("disabled", "disabled");
            callHighlightFields();
            $("input[id*=hdnViewCheckID]").val("0");
        }
        else {
            $("input[id*=txtBudgetedCost]").val("0");
            $("#txtBudgetedCost").attr("disabled", "disabled");
            $("select[id*=drpCurrencyCode]").val("");
            $("select[id*=drpCurrencyCode]").attr("disabled", "disabled");
            callHighlightFields();
            $("select[id*=drpCurrencyCode]").removeClass("brd-1px-redimp");
            $("select[id*=txtBudgetedCost]").removeClass("brd-1px-redimp");
            $("input[id*=hdnViewCheckID]").val("1");
        }
    }
    else {
        if ($("input[id*=chkIsBudgetted]").is(':checked') == true) {
            $("input[id*=txtBudgetedCost]").attr("disabled", "disabled");
            $("select[id*=drpCurrencyCode]").attr("disabled", "disabled");
            callHighlightFields();
        }
        else {
            $("input[id*=txtBudgetedCost]").val("0");
            $("input[id*=txtBudgetedCost]").attr("disabled", "disabled");
            $("select[id*=drpCurrencyCode]").val("");
            $("select[id*=drpCurrencyCode]").attr("disabled", "disabled");
            callHighlightFields();
            $("select[id*=drpCurrencyCode]").removeClass("brd-1px-redimp");
            $("select[id*=txtBudgetedCost]").removeClass("brd-1px-redimp");
        }
        $("input[id*=hdnViewCheckID]").val("1");
    }

}

function SkipNext(pageName, msgText) {
    $('#form1').validationEngine('hide');
    var index = $("#hdnTblArrayIndex").val();
    index = parseInt(index) + 1;
    $("#hdnTblArrayIndex").val(index);
    console.log("index - " + index);
    console.log("arrTr.length - " + arrTr.length);

    if (index >= arrTr.length) {
        displayMessageText(pageName, msgText, true);
        $("#hdnTblArrayIndex").val("");
        // ClearData();
        // location.reload(true);
    }
    else {
        if ((parseInt(index) + 1) == arrTr.length) {
            $(".cls-btnSkipNext").attr("disabled", true);
        }
        DisplayRecords(arrTr[index]);
        if (msgText != 'undefined' && msgText != "")
            displayMessageText(pageName, msgText, false);
    }

    return false;
}

function fillHeader(activePageHeader) {
    var loc = window.location;
    var pathName = loc.pathname.substring(loc.pathname.lastIndexOf('/') + 1);
    ////alert(pathName);

    //var recentlyVisitedLi = "";
    ////alert(arrRecentlyVisitedURL.length);
    //if (arrRecentlyVisitedURL.length == 2) {

    //    arrRecenltyVisitedURL[1] = arrRecenltyVisitedURL[0];
    //    arrRecentlyVisitedPageName[1] = arrRecentlyVisitedPageName[0];

    //    arrRecenltyVisitedURL[0] = pathName;
    //    arrRecentlyVisitedPageName[0] = activePageHeader;

    //    recentlyVisitedLi = "<li><span onclick='return redirectLink(\"" + arrRecenltyVisitedURL[0].toString() + "\">" + arrRecentlyVisitedPageName[0].toString() + "</span></li>";
    //    recentlyVisitedLi += "<li><span onclick='return redirectLink(\"" + arrRecenltyVisitedURL[1].toString() + "\">" + arrRecentlyVisitedPageName[1].toString() + "</span></li>";

    //    $("div[class*=recentviewd]").find("ul").html("");
    //    $("div[class*=recentviewd]").find("ul").html(recentlyVisitedLi);
    //}
    //else if (arrRecentlyVisitedURL.length == 1) {
    //    arrRecenltyVisitedURL[0] = pathName;
    //    arrRecentlyVisitedPageName[0] = activePageHeader;

    //    recentlyVisitedLi = "<li><span onclick='return redirectLink(\"" + arrRecenltyVisitedURL[0].toString() + "\">" + arrRecentlyVisitedPageName[0].toString() + "</span></li>";

    //    $("div[class*=recentviewd]").find("ul").html("");
    //    $("div[class*=recentviewd]").find("ul").html(recentlyVisitedLi);
    //}

    if ($("section[class*=main]").find("div[class*=MainContains]").hasClass("margin-top55"))
        $("section[class*=main]").find("div[class*=MainContains]").removeClass("margin-top55");

    $("#divHeader").removeClass("disp-noneimp");
    $("section[class*=main]").find("label[class*=cls-Header]").text(activePageHeader);

    ////
    //var pageURL = $(location).attr("href");

    //var pageTitle = $(this).attr("title");

    //console.log("pageURL - " + pageURL);
    //console.log("pageTitle - " + pageTitle);


    CheckPageLevelAuthorization(pathName, activePageHeader);
    checkRecentlyViewed(pathName, activePageHeader);




}

function checkPrevious() {
    var prevPage = "";
    prevPage = document.referrer;
    var currentPage = "";
    currentPage = window.location.pathname;
    if (prevPage.includes("/ASPX/AdminPanel/SPRO.aspx")) {
        return prevPage + '^' + 'SPRO';
    }
    return "";
}

function fillBreadCrumbs(activePageText, prevPageURL, prevPageText) {

    $("#divBreadCrumbs").removeClass("disp-noneimp");
    console.log("prevPageURL - " + prevPageURL);
    $("ul[class='breadcrumb']").html("");

    $("ul[class='breadcrumb']").append("<li><a href='/ASPX/AdminPanel/AdminHome.aspx'><i class='fa fa-home'></i>Home</a></li>");
    var chkPrev = checkPrevious();
    if (chkPrev != "" && activePageText != 'SPRO') {
        prevPageURL = chkPrev.split('^')[0];
        prevPageText = chkPrev.split('^')[1];
    }
    if (prevPageURL != "" && prevPageURL != undefined) {
        $("ul[class='breadcrumb']").append("<li><a href='" + prevPageURL + "'>" + prevPageText + "</a></li>");
    }
    $("ul[class='breadcrumb']").append("<li class='active'>" + activePageText + "</li>");
}
function redirectLink(URL) {

    var nextUrl = location.protocol + "//" + location.host + "/" + URL;
    console.log("nextUrl - " + nextUrl);

    $(location).attr("href", nextUrl);
}

function Year(minOffset, maxOffset, ControlClass) {
    //minOffset = 18, maxOffset = 100; // Change to whatever you want
    var thisYear = (new Date()).getFullYear() + 15;
    var select = $('<select class="select2">');
    $(ControlClass)
            .append($("<option></option>")
            .attr("value", '')
            .text('--Select--'));

    for (var i = minOffset; i <= maxOffset; i++) {
        var year = thisYear - i;
        //$('<option>', { value: year, text: year }).appendTo(select);

        $(ControlClass)
            .append($("<option></option>")
            .attr("value", year)
            .text(year));
    }

}

//use to date picker 
function datePicker(ControlClass) {

    $(".MainContains").find(ControlClass).each(function () {
        if ($(this).attr("disabled") == "disabled") {
            $(this).datetimepicker({

                lang: 'ch',
                timepicker: false,
                format: 'd-m-Y',
                scrollMonth: false,
                scrollInput: false//,
                //minDate: '1900/01/01',
                //maxDate: '9999/31/01',
                //yearRange: "c-116:+7983"
                //formatDate: 'Y/m/d',
                //minDate: '-1970/01/02', // yesterday is minimum date
                //maxDate: '+1970/01/02' // and tommorow is maximum date calendar
            }).on('changeDate', function (ev) {

                if ($('input[class*=reuired]').valid())
                    $('input[class*=reuired]').addClass('brd-1px-redimp');
                else
                    $('input[class*=reuired]').addClass('brd-1px-redimp');

                //if the letter is not digit then display error and don't type anything
                if (ev.which != 8 && ev.which != 0 && String.fromCharCode(ev.which) != '-' && (ev.which < 48 || ev.which > 57)) {
                    //display error message
                    $(this).addClass('brd-1px-redimp');
                    return false;
                }

            });
        }
        else {
            $(this).datetimepicker({
                lang: 'ch',
                timepicker: false,
                format: 'd-m-Y',
                scrollMonth: false//,
               // yearRange: "c-116:+7983"
                //formatDate: 'Y/m/d',
                //minDate: '-1970/01/02', // yesterday is minimum date
                //maxDate: '+1970/01/02' // and tommorow is maximum date calendar
            }).on('changeDate', function (ev) {

                if ($('input[class*=reuired]').valid())
                    $('input[class*=reuired]').addClass('brd-1px-redimp');
                else
                    $('input[class*=reuired]').addClass('brd-1px-redimp');


            });
        }
    }).on('keypress', function (ev) {
        //if the letter is not digit then display error and don't type anything
        if (ev.which != 8 && ev.which != 0 && String.fromCharCode(ev.which) != '-' && (ev.which < 48 || ev.which > 57)) {
            //display error message
            $(this).addClass('brd-1px-redimp');

            return false;
        }
    });

    //$(ControlClass).datetimepicker({
    //    lang: 'ch',
    //    timepicker: false,
    //    format: 'd-m-Y',
    //    scrollMonth: false,
    //    scrollInput: false
    //    //formatDate: 'Y/m/d',
    //    //minDate: '-1970/01/02', // yesterday is minimum date
    //    //maxDate: '+1970/01/02' // and tommorow is maximum date calendar
    //}).on('changeDate', function (ev) {

    //    if ($('input[class*=reuired]').valid())
    //        $('input[class*=reuired]').addClass('brd-1px-redimp');
    //    else
    //        $('input[class*=reuired]').addClass('brd-1px-redimp');
    //});


}

//use to time picker 
function timePicker(ControlClass) {

    $(ControlClass).datetimepicker({
        lang: 'ch',
        datepicker: false,
        timepicker: true,
        // format: 'h:i'
        format: 'H:i',
        formatTime: 'g:i A',
        use24hours: false
        //formatDate: 'Y/m/d',
        //minDate: '-1970/01/02', // yesterday is minimum date
        //maxDate: '+1970/01/02' // and tommorow is maximum date calendar
    }).on('keypress', function (ev) {
        //if the letter is not digit then display error and don't type anything
        if (ev.which != 8 && ev.which != 0 && String.fromCharCode(ev.which) != ':' && (ev.which < 48 || ev.which > 57)) {
            //display error message
            $(this).addClass('brd-1px-redimp');
            return false;
        }
    });
}



/************ Functions for fetching messages based on page and action ****/

function getMessageText(pageLink, pageAction, tempData) {

    var msgsList = JSON.parse($(".hdnMsgsList").val());
    var messageText = "";

    $.each(msgsList, function (index, data) {
        if (data.MenuLink == pageLink && data.PageAction == pageAction)
            messageText = data.MessageText;
    });

    if (messageText == "") {
        $.each(msgsList, function (index, data) {
            if (data.MenuLink == "" && data.PageAction == pageAction)
                messageText = data.MessageText;
        });
    }
    console.log("before setting the values of variables messageText - " + messageText);

    if (tempData.length > 2) {
        for (var i = 2; i < tempData.length; i++) {
            var start = messageText.indexOf("#");
            var end = messageText.indexOf("#", start + 1);
            var substr = messageText.substring(start, end + 1);
            console.log("start position of # - " + start);
            console.log("end position of # - " + end);
            console.log("substr - " + substr);
            if (start != -1 && end != -1)
                messageText = messageText.replace(substr, tempData[i].toString());
        }
    }

    console.log("messageText - " + messageText);

    return messageText;
}

function displayMessageText(pageLink, messageData, reload) {
    var temp = messageData;
    var tempData = temp.split('$');
    var msgType = tempData[0];
    var pageAction = tempData[1];

    console.log("msgType - " + msgType);
    console.log("pageAction - " + pageAction);
    console.log("messageData - " + messageData);

    //return false;
    var msgText = getMessageText(pageLink, pageAction, tempData);
    console.log("msgText - " + msgText);

    if (msgType == "S")
        successAlert("Success", msgText, reload);
    else if (msgType == "E")
        errorAlert("Error", msgText, reload);
    else {
        msgText = getMessageText(pageLink, "UnknownErr");
        errorAlert("Error", msgText, reload);
    }
}

/*--------------------------------------------------------------------****/

/************ Functions for fetching Standard Fields *****/

function fillStandardFields(tableName) {

    var action = "1";

    //Fill dropdown data
    $.ajax({
        url: "/ASPX/WebServices/CommonWebService.asmx/GetStandardCustomFields",
        data: "{'action':'" + action + "', 'fieldId' : '0', 'tableName':'" + tableName + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {
            console.log("data - " + data.d);
            var dataFields = data.d;
            var jsonData = JSON.parse(dataFields);
            //console.log("jsonData length - " + jsonData.length);
            console.clear();
            for (i = 0; i < jsonData.length; i++) {
                console.log("fieldId - " + jsonData[i].fieldID);
                console.log("fieldLabel - " + jsonData[i].fieldLabel);
                var $elem;

                //changes * replace by $='txt' / $='drp' --> 18 - 8 - 16 amruta 
                if (jsonData[i].fieldControl == "Textbox")
                    $elem = $("input[id$='txt" + jsonData[i].fieldDB + "']");
                else if (jsonData[i].fieldControl == "Dropdown")
                    $elem = $("select[id$='drp" + jsonData[i].fieldDB + "']");
                else if (jsonData[i].fieldControl == "TextArea")
                    $elem = $("textarea[id$='txt" + jsonData[i].fieldDB + "']");
                else if (jsonData[i].fieldControl == "Checkbox")//amruta add changes 16 aug 16
                    $elem = $("checkbox[id$='chk" + jsonData[i].fieldDB + "']");
                else if (jsonData[i].fieldControl == "Radiobutton")
                    $elem = $("radio[id$-'rdb" + jsonData[i].fieldDB + "']");

                //changes * replace by $='txt' / $='drp' --> 18 - 8 - 16 amruta 

                if (jsonData[i].fieldVisible == "False") {
                    $($elem).parent("div").parent("div").addClass("disp-none");
                }
                else {
                    console.log($($elem).attr("class"));
                    $($elem).parent("div").parent("div").removeClass("disp-none");
                    $($elem).parent("div").prev("label").text(jsonData[i].fieldLabel);

                    var classData = jsonData[i].fieldCSSClass;
                    if (jsonData[i].fieldReq == "False") {
                        classData = classData.replace("required, ", "");
                        classData = classData.replace("required,", "");
                        classData = classData.replace("required", "");

                        //if (classData != "")
                        //    classData = classData + ",";

                        //classData = classData + "required";
                    }

                    //classData = "validate[" + classData + "]";

                    $($elem).addClass(classData);

                    if (jsonData[i].fieldDisabled == "True") {
                        $($elem).attr("disabled", true);
                    }
                }
            }
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });

    return false;
}

/*-------------------------------------------------------------------*/

/************ Functions for fetching Standard Fields *****/

function fillCustomFields(tableName) {

    var action = "2";

    //Fill dropdown data
    $.ajax({
        url: "/ASPX/WebServices/CommonWebService.asmx/GetStandardCustomFields",
        data: "{'action':'" + action + "', 'fieldId' : '0', 'tableName':'" + tableName + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {

            console.log("data - " + data.d);
            var dataFields = data.d;
            var jsonData = JSON.parse(dataFields);
            //console.log("jsonData length - " + jsonData.length);
            var customFields = "";
            var cssClass = "form-control";
            var $divAdditional = $("#divAdditional");
            if (jsonData.length > 0) {
                $("#divCustomField").removeClass("disp-none");
            }
            else {
                $("#divCustomField").addClass("disp-none");
            }

            for (i = 0; i < jsonData.length; i++) {
                //console.log("fieldId - " + jsonData[i].fieldID);
                var $div = $('<div />', { 'class': 'form-group' });

                $div.append($('<label />', { 'class': 'col-lg-3 control-label', 'text': jsonData[i].fieldLabel }));

                var $internalDiv = $('<div />', { 'class': 'col-lg-8' });
                var $control;

                classData = jsonData[i].fieldCSSClass;
                if (jsonData[i].fieldReq == "False") {
                    classData = classData.replace("required, ", "");
                    classData = classData.replace("required,", "");
                    classData = classData.replace("required", "");
                }

                if (jsonData[i].fieldControl == "Textbox") {

                    //var applyClass = "";
                    //if (jsonData[i].fieldReq == "True")
                    //    applyClass = "required ";
                    //if (jsonData[i].fieldCSSClass == "custom[date]")
                    //    applyClass += jsonData[i].fieldCSSClass;

                    //if (applyClass != "") {
                    //    if (jsonData[i].fieldCSSClass == "custom[date]")
                    //        applyClass = "validate[" + applyClass + "]" + ' ' + "datePicker";
                    //    else
                    //        applyClass = "validate[" + applyClass + "]";
                    //}

                    ////$control = $('<input />', {
                    ////    'class': 'form-control' + (jsonData[i].fieldReq == 'True' ? ' validate[required]' : ''),
                    ////    'type': 'text', 'id': 'txt' + jsonData[i].fieldDB
                    ////});

                    $control = $('<input />', {
                        'class': classData,
                        'type': 'text', 'id': 'txt' + jsonData[i].fieldDB
                    });

                    $internalDiv.append($control);
                }

                else if (jsonData[i].fieldControl == "Textarea") {
                    $control = $('<textarea />', {
                        'class': classData,
                        'id': 'txt' + jsonData[i].fieldDB
                    });
                    $internalDiv.append($control);
                }
                else if (jsonData[i].fieldControl == "Dropdown") {
                    $control = $('<select />', {
                        'class': classData,
                        'id': 'drp' + jsonData[i].fieldDB
                    });

                    var fieldValues = getAdditionalFieldsValues(jsonData[i].fieldID);
                    console.log("fieldValues - " + fieldValues);
                    $control.append('<option value="" >--Select--</option>');
                    $.map(fieldValues, function (item) {
                        $control.append('<option value="' + item.split('/')[1] + '">' + item.split('/')[0] + '</option>');
                    });
                    $internalDiv.append($control);
                }
                else if (jsonData[i].fieldControl == "Radiobutton") {

                    var fieldValues = getAdditionalFieldsValues(jsonData[i].fieldID);
                    $.map(fieldValues, function (item) {

                        $control = $('<input />', {
                            'class': classData,
                            'type': 'radio', 'name': 'rdb' + jsonData[i].fieldDB, 'value': item.split('/')[1]
                        });

                        //$control = $('<input />', {
                        //    'class': 'margin-right10imp margin-left10imp' + (jsonData[i].fieldReq == 'True' ? ' validate[required]' : ''),
                        //    'type': 'radio', 'name': 'rdb' + jsonData[i].fieldDB, 'value': item.split('/')[1]
                        //});
                        $internalDiv.append($control);
                        //$control = $('<label />', {
                        //    'class': 'control-label', 'text': item.split('/')[0], 'for': 'rdb' + jsonData[i].fieldDB
                        //});
                        $internalDiv.append(item.split('/')[0]);
                    });
                }
                else if (jsonData[i].fieldControl == "Checkbox") {
                    $control = $('<input />', {
                        'class': classData,
                        'type': 'checkbox', 'id': 'chk' + jsonData[i].fieldDB, 'value': jsonData[i].fieldLabel
                    });

                    //$control = $('<input />', {
                    //    'class': 'margin-right10imp margin-left10imp' + (jsonData[i].fieldReq == 'True' ? ' validate[required]' : ''),
                    //    'type': 'checkbox', 'id': 'chk' + jsonData[i].fieldDB, 'value': jsonData[i].fieldLabel
                    //});

                    $internalDiv.append($control);
                }
                else if (jsonData[i].fieldControl == "Fileupload") {
                    $control = $('<input />', {
                        'class': classData,
                        'type': 'file', 'name': 'file', 'id': 'txt' + jsonData[i].fieldDB
                    });

                    //$control = $('<input />', {
                    //    'class': 'text-input' + (jsonData[i].fieldReq == 'True' ? ' validate[required]' : 'validate[optional]'),
                    //    'type': 'file', 'name': 'file', 'id': 'txt' + jsonData[i].fieldDB
                    //});
                    $internalDiv.append($control);
                }

                console.log("control - " + $control);

                //$internalDiv.append($control);
                $div.append($internalDiv);
                $divAdditional.append($div);


            }
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });

    return false;
}

function getAdditionalFieldsValues(fieldId) {
    var fieldValues = "";
    var action = "4";
    var fieldValues = "";
    //Fill dropdown data
    $.ajax({
        url: "/ASPX/WebServices/CommonWebService.asmx/GetCustomFieldsValues",
        data: "{'action':'" + action + "', 'fieldId' : '" + fieldId + "', 'tableName':''}",
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            fieldValues = data.d;

        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });

    console.log('fieldvalues in get func - ' + fieldValues);
    return fieldValues;
}

function fetchCustomFieldValues(columnId, tableName) {
    var action = "5";

    //Fill dropdown data
    $.ajax({
        url: "/ASPX/WebServices/CommonWebService.asmx/GetCustomFieldsData",
        data: "{'action':'" + action + "', 'fieldId' : " + columnId + ", 'tableName':'" + tableName + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {

            console.log("data - " + data.d);
            var dataFields = data.d;
            if (dataFields != "") {
                var jsonData = JSON.parse(dataFields);

                if (jsonData[0].fieldDB != "Notfound") {
                    //console.log("jsonData length - " + jsonData.length);
                    var customFields = "";
                    var cssClass = "form-control";
                    var $divAdditional = $("#divAdditional");
                    var $ctrl;
                    var ctrlType;
                    for (i = 0; i < jsonData.length; i++) {


                        if ($($divAdditional).find("input[id='txt" + jsonData[i].fieldDB + "']").length > 0) {
                            $ctrl = $($divAdditional).find("input[id='txt" + jsonData[i].fieldDB + "']");
                            console.log("control id - " + $($ctrl).attr("id"));
                            ctrlType = "textbox";
                        }
                        else if ($($divAdditional).find("input[id='chk" + jsonData[i].fieldDB + "']").length > 0) {
                            $ctrl = $($divAdditional).find("input[id='chk" + jsonData[i].fieldDB + "']");
                            ctrlType = "checkbox";
                        }
                        else if ($($divAdditional).find("select[id='drp" + jsonData[i].fieldDB + "']").length > 0) {
                            $ctrl = $($divAdditional).find("select[id='drp" + jsonData[i].fieldDB + "']");
                            ctrlType = "dropdown";
                        }
                        else if ($($divAdditional).find("textarea[id='txt" + jsonData[i].fieldDB + "']").length > 0) {
                            $ctrl = $($divAdditional).find("textarea[id='txt" + jsonData[i].fieldDB + "']");
                            ctrlType = "textarea";
                        }
                        else if ($($divAdditional).find("input[name='rdb" + jsonData[i].fieldDB + "']").length > 0)
                            ctrlType = "radio";


                        console.log("ctrlType - " + ctrlType);
                        console.log("$ctrl - " + $ctrl);

                        if (ctrlType == "radio") {
                            if (jsonData[i].fieldValue != "")
                                $("input[name='rdb" + jsonData[i].fieldDB + "'][value=" + jsonData[i].fieldValue + "]").attr('checked', true);

                        }
                        else if (ctrlType == "checkbox") {
                            console.log("chekbox length - " + $($divAdditional).find("input[id='chk" + jsonData[i].fieldDB + "']").length);
                            if (jsonData[i].fieldValue == "True")
                                $("input[id='chk" + jsonData[i].fieldDB + "']").attr('checked', true);
                            else if (jsonData[i].fieldValue == "1")
                                $("input[id='chk" + jsonData[i].fieldDB + "']").attr('checked', false);
                        }
                        else if (ctrlType != "file") {
                            console.log("field DB - " + jsonData[i].fieldDB);
                            console.log("field Value - " + jsonData[i].fieldValue);
                            console.log("select length - " + $($divAdditional).find("select[id='drp" + jsonData[i].fieldDB + "']").length);
                            console.log("textbox length - " + $($divAdditional).find("input[id='txt" + jsonData[i].fieldDB + "']").length);
                            console.log("textarea length - " + $($divAdditional).find("textarea[id='txt" + jsonData[i].fieldDB + "']").length);
                            if ($($ctrl).hasClass("datePicker")) {
                                if ((jsonData[i].fieldValue).length > 0) {
                                    console.log('date val :' + jsonData[i].fieldValue);
                                    //var dateValue = Date(jsonData[i].fieldValue);

                                    //var dateValue = day(dateValue) + '-' + jsonData[i].fieldValue.substr(0, 2) + '-' + jsonData[i].fieldValue.substr(6, 4);
                                    var dateValue = jsonData[i].fieldValue.substr(3, 2) + '-' + jsonData[i].fieldValue.substr(0, 2) + '-' + jsonData[i].fieldValue.substr(6, 4);
                                    //console.log('conv date val :' + $.format.Date(jsonData[i].fieldValue, "dd-mm-yyyy"));
                                    $($ctrl).val(dateValue);
                                }
                            }
                            else {
                                $($ctrl).val(jsonData[i].fieldValue);
                            }
                            console.log("control id - " + $($ctrl).attr("id"));
                            console.log("control value - " + $($ctrl).val());
                        }

                    }
                }
            }
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });
}

/*-------------------------------------------------------------------*/

/*--------------------Functions for Additional Search---------------------*/

function getSearchCriterias() {
    $.ajax({
        url: "/ASPX/WebServices/CommonWebService.asmx/GetSearchCriterias",
        data: "{'action':'" + 1 + "'}",
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log("Search criteria html - " + data.d);
            $("#divCriterias").html("");
            $("#divCriterias").html(data.d);
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });

}

function showHideFilters() {
    getSearchCriterias();
    $('.clsDivFilters').bPopup();

}

function triggerFunction(drpRef, drpID) {
    console.log("drp value - " + $(drpRef).val());
    var selValue = $(drpRef).val();
    var $div = $("#divCriterias");

    $($div).find("input[class='hdnDependant']").each(function () {

        if (($(this).val() == drpID)) {
            var Action = $(this).next("input[class='hdnAction']").val();
            var $drpdown = $(this).prev("select");
            //Fill dropdown data
            $.ajax({
                url: "/ASPX/WebServices/CommonWebService.asmx/GetDropDownData",
                data: "{'Action':'" + Action + "'}",
                dataType: "json",
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $drpdown.html("");
                    $drpdown.append('<option value="">' + "--Select--" + '</option>');
                    var value = "";
                    var text = "";
                    $.map(data.d, function (item) {
                        value = item.split('/')[2];
                        text = item.split('/')[0];
                        if ((value.split('$')[0].toString() == selValue) || (selValue == ""))
                            $drpdown.append($("<option></option>").val(value.split('$')[1].toString()).html(text));
                        //$drpdown.append('<option id="' +  + '">' + text + '</option>');

                    });
                },
                error: function (xhr, status, error) {
                    if (xhr.responseJSON.Message == "Session Expired") {
                        errorAlert("Error", "Session Expired", true);
                    }
                    else {
                        errorAlert("Error", xhr.responseJSON.Message, false);
                    }
                    console.log("Error Thrown  - " + xhr.responseText);
                }
            });
        }

    });

}

function searchEmployeeNumber() {
    var searchCriteria = "";

    $("#divCriterias").find("select").each(function () {
        console.log("drp value - " + $(this).find("option:selected").val());
        if ($(this).find("option:selected").val() != "") {
            searchCriteria += " and " + $(this).parent("div").find("input[class='clsDBValue']").val() + " = " + $(this).find(":selected").val() + "";
        }
    });

    console.log("searchCriteria - " + searchCriteria);

    var $drpdown = $("select[class*=drpEmpNo]");
    //Fill dropdown data
    $.ajax({
        url: "/ASPX/WebServices/CommonWebService.asmx/GetFilteredEmployees",
        data: "{'action':'" + 35 + "',searchCriteria : '" + searchCriteria + "'}",
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $drpdown.html("");
            $drpdown.append('<option id="">' + "--Select--" + '</option>');
            var value = "";
            var text = "";
            $.map(data.d, function (item) {
                value = item.split('/')[1];
                text = item.split('/')[0];
                $drpdown.append('<option id="' + value + '">' + text + '</option>');

            });
            $('.clsDivFilters').bPopup().close();
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });

    $('.collaps').addClass("disp-none");
    return false;

}

/*-----------------------------------------------------------------------*/

//Function used to fetch records to delete
function DeleteConfirmation(btnActionRef) {
    $delBtnRef = btnActionRef;
    ShowHideForm("View", btnActionRef);

    ////$("input[class*=cls-btnSave-Validate]").addClass("disp-noneimp");
    $(".SaveBtn").addClass("disp-noneimp");
    $("input[class*=cls-btnDelete]").removeAttr("disabled", "disabled");
    $("input[class*=cls-btnDelete]").removeClass("disp-noneimp");

    //add html control in page -> amruta 2016-22-08
    $("button[class*=cls-btnDelete]").removeAttr("disabled", "disabled");
    $("button[class*=cls-btnDelete]").removeClass("disp-noneimp");


}

//function used to drag panel 
var mydragg = function () {
    return {
        move: function (divClass, xpos, ypos) {
            $("." + divClass).css("left", xpos + 'px');
            $("." + divClass).css("top", ypos + 'px');
            //divid.style.left = xpos + 'px';
            //divid.style.top = ypos + 'px';
        },
        startMoving: function (divClass, container, evt) {
            console.log("event - " + evt);
            var targetContainer = evt.target.id;
            if (targetContainer != "" && targetContainer != "divPanel")
                return false;
            var target = evt.currentTarget;
            console.log("evt target - " + target);
            console.log("target container - " + targetContainer);

            console.log("container   " + container);
            evt = evt || window.event;
            var posX = evt.clientX,
                posY = evt.clientY,
            divTop = $("." + divClass).css("top"),
            divLeft = $("." + divClass).css("left"),
            eWi = parseInt($("." + divClass).css("width")),
            eHe = parseInt($("." + divClass).css("height")),
            cWi = parseInt(document.getElementById(container).style.width),
            cHe = parseInt(document.getElementById(container).style.height);
            document.getElementById(container).style.cursor = 'move';

            if (divTop != undefined && divTop != "") {
                if (divTop.indexOf('px') > 0) {
                    divTop = divTop.replace('px', '');
                }
            }
            if (divLeft != undefined && divLeft != "") {
                if (divLeft.indexOf('px') > 0) {
                    divLeft = divLeft.replace('px', '');
                }
            }
            var diffX = posX - divLeft,
                diffY = posY - divTop;
            document.onmousemove = function (evt) {
                evt = evt || window.event;
                var posX = evt.clientX,
                    posY = evt.clientY,
                    aX = posX - diffX,
                    aY = posY - diffY;
                if (aX < 0) aX = 0;
                if (aY < 0) aY = 0;
                if (aX + eWi > cWi) aX = cWi - eWi;
                if (aY + eHe > cHe) aY = cHe - eHe;
                mydragg.move(divClass, aX, aY);
            }
        },
        stopMoving: function (container) {
            var a = document.createElement('script');
            document.getElementById(container).style.cursor = 'default';
            document.onmousemove = function () { }
        },
    }
}();

function generateQueryToSaveCustomFields() {

    var strQry = "";

    var strFieldName = "";
    $("#divAdditional input").each(function (e) {
        strFieldName = this.id;
        if (strFieldName.substr(0, 3) == 'chk') {
            if ($(this).is(':checked')) {
                strQry = strQry + ', [' + strFieldName.substr(3, strFieldName.length) + '] = "1"';
            }
            else {
                strQry = strQry + ', [' + strFieldName.substr(3, strFieldName.length) + '] = "0"';
            }
        }
        else if (this.name.substr(0, 3) == 'rdb') {
            if ($(this).is(':checked')) {
                strQry = strQry + ', [' + this.name.substr(3, this.name.length) + '] = "' + this.value + '"';
            }
        }
        else {
            if ($(this).hasClass("datePicker")) {
                if ((this.value).length > 0) {
                    var dateValue = this.value.substr(3, 2) + '-' + this.value.substr(0, 2) + '-' + this.value.substr(6, 4);
                    strQry = strQry + ', [' + strFieldName.substr(3, strFieldName.length) + '] = "' + dateValue + '"';
                }
            }
            else {
                strQry = strQry + ', [' + strFieldName.substr(3, strFieldName.length) + '] = "' + this.value.replace("'", "`") + '"';
            }
        }
    });

    $("#divAdditional select").each(function (e) {
        strFieldName = this.id;
        strQry = strQry + ', [' + strFieldName.substr(3, strFieldName.length) + '] = "' + this.value + '"';
    });

    $("#divAdditional textarea").each(function (e) {
        strFieldName = this.id;
        strQry = strQry + ', [' + strFieldName.substr(3, strFieldName.length) + '] = "' + this.value.replace("'", "`") + '"';
    });

    strQry = strQry.substr(1, strQry.length);
    return strQry;
}

//--------------Function to read values from QueryString----------------------------//

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return (urlparam[1] == 'null' ? '' : urlparam[1]);
            //return urlparam[1];
        }
    }
}

//----------------------------------------------------------------------------------//

function highLightRequiredFields() {


    $("input[class*=cls-LetterOnly]").trigger("keydown");

    $("input[class*=required]").trigger("keyup");

    //$('input[class*=datePicker]').trigger("focus")

    $('input[class*=integer]').trigger("keydown");

    $("select[class*=required]").trigger("change");

    $("textarea[class*=required]").trigger("keyup");

    $("input[class*=cls-UpperCase").trigger("keyup");



    //$('textarea[MaxLength]').trigger("keyup");

    //var val = $("input").val();
    //if (val != "")
    //    $(this).css("border", "");
    //else {

    //}
}

//function callHighlightFields(clsField, clsFieldType) {

function callHighlightFields() {

    //$(".required").each(function () {
    //    if ($(this).val() == "")
    //        $(this).addClass("brd-1px-redimp");
    //    else
    //        $(this).removeClass("brd-1px-redimp");
    //});

    $("input[class*=required]").each(function () {
        if ($(this).val() == "")
            $(this).addClass("brd-1px-redimp");
        else
            $(this).removeClass("brd-1px-redimp");
    });

    $("select[class*=required]").each(function () {
        if ($(this).val() == "" || $(this).val() == null)
            $(this).addClass("brd-1px-redimp");
        else
            $(this).removeClass("brd-1px-redimp");
    });

    $("textarea[class*=required]").each(function () {
        if ($(this).val() == "")
            $(this).addClass("brd-1px-redimp");
        else
            $(this).removeClass("brd-1px-redimp");
    });




}

//*--------------------Functions for Employee header Search---------------------*//

function getEmployeeHeaders(EmployeeNo, TableName) {
    if (EmployeeNo != "undefined") {
        $.ajax({
            url: "/ASPX/WebServices/CommonWebService.asmx/GetEmployeeHeader",
            data: "{action:'" + 1 + "',EmployeeNo:'" + EmployeeNo + "',TableName:'" + TableName + "'}",
            dataType: "json",
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log("Search criteria html - " + data.d);
                $("#divEmployeeHeader").html("");
                $("#divEmployeeHeader").html(data.d);
            },
            error: function (xhr, status, error) {
                if (xhr.responseJSON.Message == "Session Expired") {
                    errorAlert("Error", "Session Expired", true);
                }
                else {
                    errorAlert("Error", xhr.responseJSON.Message, false);
                }
                console.log("Error Thrown  - " + xhr.responseText);
            }

        });
    }
}

//----------------------------------------------------------------------------------//

function renderFields(pageName) {
    fillStandardFields(pageName);
    fillCustomFields(pageName);
    highLightRequiredFields();
    datePicker(".datePicker");
    timePicker(".timePicker");

}

function getWizardData(Action, EventId, RedirectedFrom, PageName, InfoTypeSubId) {

    $.ajax({
        url: "/ASPX/WebServices/CommonWebService.asmx/GetWizardData",
        data: "{'Action':'" + Action + "', 'EventId' : '" + EventId + "', 'RedirectedFrom':'" + RedirectedFrom + "', 'PageName':'" + PageName + "', 'InfoTypeSubId':'" + InfoTypeSubId + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {
            console.log("data - " + data.d);
            var dataFields = data.d;
            var jsonData = JSON.parse(dataFields);

            $("input[id*=hdPrevPage]").val(jsonData.PrevPage);
            $("input[id*=hdNextPage]").val(jsonData.NextPage);
            $("input[id*=hdCanSkip]").val(jsonData.CanSkip);
            $("input[id*=hdNextTypeId]").val(jsonData.NextTypeId);
            $("input[id*=hdPrevTypeId]").val(jsonData.PrevTypeId);

            console.log("Prev Page : " + $("input[id*=hdPrevPage]").val());
            console.log("Next Page : " + $("input[id*=hdNextPage]").val());
            console.log("Can Skip : " + $("input[id*=hdCanSkip]").val());
            console.log("Next Sub Type : " + $("input[id*=hdNextTypeId]").val());
            console.log("Prev Sub Type : " + $("input[id*=hdPrevTypeId]").val());

        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });

    return false;
}

function displayMessageTextForWizard(pageLink, messageData, reload) {
    var temp = messageData;
    var tempData = temp.split('$');
    var msgType = tempData[0];
    var pageAction = tempData[1];

    console.log("msgType - " + msgType);
    console.log("pageAction - " + pageAction);
    console.log("messageData - " + messageData);

    //return false;
    var msgText = getMessageText(pageLink, pageAction, tempData);
    console.log("msgText - " + msgText);

    if (msgType == "S")
        showAlertForWizard("Success", msgText, 'green', reload);

}


function disabledControls() {
    $("div[class*=panel-body]").removeClass("disp-none");

    $("div[class*=panel-body]").find("input").each(function () {
        $(this).attr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("textarea").each(function () {
        $(this).attr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("select").each(function () {
        $(this).attr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("input[class*=cls-btnSave]").each(function () {
        $(this).attr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("input[class*=cls-btnCancel]").each(function () {
        $(this).removeAttr("disabled", "disabled");
    });
}

function enabledControls() {
    $("div[class*=panel-body]").removeClass("disp-none");

    $("div[class*=panel-body]").find("input").each(function () {
        $(this).removeAttr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("textarea").each(function () {
        $(this).removeAttr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("select").each(function () {
        $(this).removeAttr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("input[class*=cls-btnSave]").each(function () {
        $(this).removeAttr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("input[class*=cls-btnCancel]").each(function () {
        $(this).removeAttr("disabled", "disabled");
    });


}

function readOnlyControlsView() {
    $("div[class*=panel-body]").removeClass("disp-none");

    $("div[class*=panel-body]").find("input").each(function () {
        $(this).attr("readonly", "true");
    });

    $("div[class*=panel-body]").find("textarea").each(function () {
        $(this).attr("readonly", "true");
    });

    $("div[class*=panel-body]").find("select").each(function () {
        $(this).attr("readonly", "true");
    });

    $("div[class*=panel-body]").find("input[class*=cls-btnSave]").each(function () {
        $(this).attr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("input[class*=cls-btnCancel]").each(function () {
        $(this).removeAttr("disabled", "disabled");
    });
}

function readOnlyControlsEdit() {
    $("div[class*=panel-body]").removeClass("disp-none");

    $("div[class*=panel-body]").find("input").each(function () {
        $(this).attr("readonly", "true");
    });

    $("div[class*=panel-body]").find("textarea").each(function () {
        $(this).attr("readonly", "true");
    });

    $("div[class*=panel-body]").find("select").each(function () {
        $(this).attr("readonly", "true");
    });

    $("div[class*=panel-body]").find("input[class*=cls-btnSave]").each(function () {
        $(this).attr("disabled", "disabled");
    });

    $("div[class*=panel-body]").find("input[class*=cls-btnCancel]").each(function () {
        $(this).removeAttr("disabled", "disabled");
    });
}

function showPlusMinusButtons(flag) {

    if (flag == "plus") {

        $("div[class*=panel-body]").removeClass("disp-block");
        $("div[class*=panel-body]").addClass("disp-none");

        $("a[class*=grdBtnPlus]").removeClass("fa-minus-circle");
        $("a[class*=grdBtnPlus]").addClass("fa-plus-circle");
    }
    else {
        $("div[class*=panel-body]").removeClass("disp-none");
        $("div[class*=panel-body]").addClass("disp-block");

        $("a[class*=grdBtnPlus]").removeClass("fa-plus-circle");
        $("a[class*=grdBtnPlus]").addClass("fa-minus-circle");
    }

}

function showBtnsForViewEdit() {
    $(".SaveBtn").removeClass("disp-noneimp");
    $("input[class*=cls-btnDelete]").addClass("disp-noneimp");
    $("button[class*=cls-btnDelete]").addClass("disp-noneimp");//add html control in page -> amruta 2016-22-08
    $(".cls-btnCancel").removeClass("disp-noneimp");

}

function ShowHideBodyReload() {
    var rowCount = $(".grdDataTable tr").length;
    if (rowCount == 0) {
        $("div[class*=panel-body]").each(function () {
            $(this).removeClass("disp-none");
            $(this).addClass("disp-block");
        });
    }
    else {
        $("div[class*=panel-body]").each(function () {
            $(this).removeClass("disp-block");
            $(this).addClass("disp-none");
            $("a[class*=grdBtnPlus]").removeClass("fa-minus-circle");
            $("a[class*=grdBtnPlus]").addClass("fa-plus-circle");
            //clearSelect2Value();
        });

    }
}

//display current date textbox and another control // 20 aug 2016
function CurrentDateDisp() {
    var month = "";
    if ((new Date().getMonth() + 1) < 10)
        month = "0" + (new Date().getMonth() + 1);
    else
        month = (new Date().getMonth() + 1);

    var day = "";
    if ((new Date().getDate()) < 10)
        day = "0" + (new Date().getDate());
    else
        day = (new Date().getDate());

    var date1 = ((day + "-" + month + "-" + new Date().getFullYear()));

    return date1;
}

function showHideWizardBtns() {
    $("div[id*=btnNextPrevTop]").removeClass("disp-noneimp");
    $("div[id*=btnNextPrevBottom]").removeClass("disp-noneimp");

    $("button[class*=cls-btnPrev]").addClass("disp-noneimp");
    $("button[class*=cls-btnNext]").addClass("disp-noneimp");

    if ($("input[id*=hdPrevPage]").val() != "") {
        $("button[class*=cls-btnPrev]").removeClass("disp-noneimp");
    }

    if ($("input[id*=hdNextPage]").val() != "") {
        $("button[class*=cls-btnNext]").removeClass("disp-noneimp");
    }
}


function assignSelect2Value() {

    $("#divContainer").find("select").each(function () {
        var selectVal = $(this).val();

        $(this).select2('val', selectVal);
    });

}

//clear dropdowns values // 2 sep 2016
function clearSelect2Value() {

    $("#form1").find("select").each(function () {
        //var selectVal = $(this).val();

        $(this).select2('val', "");
    });

}

//clear and fill dropdowns values - amruta 06-09-2016 
function assignSelect2ValueInTrans() {
    $("#divContainer").find("select").each(function () {
        var selectVal = $(this).val();
        if (selectVal == "" || selectVal == null)
            $(this).select2('val', "");
        else
            $(this).select2('val', selectVal);
    });
}

/**************  Recentl Visited Pages Code Starts HERE *******/

//recently viewed code
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    // $.cookie = cname + '=' + cvalue + ';path="/";' + expires;
    $.cookie(cname, cvalue, { expires: 365, path: "/" });
}

function getCookie(name) {

    var varCookie = $.cookie(name);

    console.log("Cookie - " + varCookie);
    if (varCookie != undefined && varCookie != '') {
        var temp = varCookie.split(',');
        if (temp.length > 0) {

            for (var i = 0; i < temp.length; i++)
                console.log("temp val - " + temp[i]);

        }
    }
    return temp;
}

function checkRecentlyViewed(pageURL, pageTitle) {

    var history = getCookie("history");

    var queryStringPath = window.location.href;
    console.log("queryStringPath - " + queryStringPath);
    var indx = queryStringPath.indexOf("/");
    pageURL = queryStringPath.substring(indx + 2);
    indx = pageURL.indexOf("/");
    pageURL = pageURL.substring(indx + 1);

    console.log("PageURL - " + pageURL);

    
    var length = pageURL.length;
    var indxOfHash = pageURL.indexOf('#');

    if(indxOfHash != -1)
    {
        if (length == (indxOfHash + 1))
            pageURL = pageURL.substring(0, length - 1);
    }

    var sp = '';
    if (history != "" && history != undefined) {

        sp = history.toString().indexOf(pageURL);

        if (sp == -1) {
            if (queryStringPath.indexOf("?") >= 0) {
                sp = history.toString();
            }
            else {
                if (history.length >= 5) {
                    history.shift();
                }

                history.push(pageURL + "$" + pageTitle);
                sp = history.toString();
            }
        }
        else {
            sp = history.toString();
        }
        //displayRecentlyViewed(history)

        setCookie("history", sp.toString(), 30);
    }
    else {
        var stack = new Array();
        stack.push(pageURL + "$" + pageTitle);
        console.log("URL stack - " + stack);
        setCookie("history", stack.toString(), 30);
        sp = stack.toString();
    }

    displayRecentlyViewed(sp)
}

function displayRecentlyViewed(history) {

    var pages = history.toString().split(',');
    var htmlContent = '<ul>';
    var content = "";
    for (var i = 0; i < pages.length ; i++) {
        var pageURL = pages[i].toString().split('$')[0];
        var pageTitle = pages[i].toString().split('$')[1];
        var index = pages.indexOf(pages[i]);

        //var htmlContent = $("[input*=recentviewtext]").html();
        content = '<li><span id="redirectlink" title="' + pageTitle + '" onclick="return redirectLink(\'' + pageURL + '\')">' + pageTitle + '</span><a href="javascript:void(0);" onclick="return closetab(this,' + index + ',\'' + pages + '\');"></a></li>';
        htmlContent += content;
        //htmlContent += '<a style="color: white;"  href="' + pageURL + '">' + pageTitle + '</a><br>';
    }
    htmlContent += '</ul>';
    var list = $(htmlContent).appendTo('#divRecentlyViewed');

}

function closetab(element, indexOfPage, history) {


    var arr = history.toString().split(',');
    arr.splice(indexOfPage, 1);
    $(element).parent('li').remove();
    var temp = arr;
    if (temp.length === 0) {
        setCookie("history", "", -1);
    }
    else {
        setCookie("history", temp.toString(), 30);
    }

    if (typeof ($('.recentviewd').find('ul li').html()) == 'undefined') {
        $('#recentviewtext').html('');
    }
}


///**************** shortcuts  ****************///

//Pin and unpin page from shortcut menu // 6 sep 2016
function pinShortCut() {
    if ($("#hdnShortcutCount").val() >= 16) {
        errorAlert("Error", "You have already added 16 shortcut icons,Please unpin some page", false);
        return false;
    }
    var urlName = window.location.pathname;
    $.ajax({
        url: "/ASPX/WebServices/M105_PinUnpinMenus_WS.asmx/InsertUpdateData",
        data: "{Action : '2', UrlName : '" + urlName + "'}",
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("a[id*=pin]").removeClass("disp-block");
            $("a[id*=pin]").addClass("disp-none");
            if ($("a[id*=unpin]").hasClass("disp-none")) {
                $("a[id*=unpin]").removeClass("disp-none")
                $("a[id*=unpin]").addClass("disp-block");
            }
            updateShortCut();
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }

    });

}

function unpinShortCut() {
    var urlName = window.location.pathname;
    $.ajax({
        url: "/ASPX/WebServices/M105_PinUnpinMenus_WS.asmx/DeleteData",
        data: "{Action : '4', UrlName : '" + urlName + "'}",
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if ($("a[id*=pin]").hasClass("disp-none")) {
                $("a[id*=pin]").removeClass("disp-none")
                $("a[id*=pin]").addClass("disp-block");
            }
            $("a[id*=unpin]").removeClass("disp-block");
            $("a[id*=unpin]").addClass("disp-none");
            updateShortCut();
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });
}

function updateShortCut() {
    var urlName = window.location.pathname;
    $.ajax({
        url: "/ASPX/WebServices/M105_PinUnpinMenus_WS.asmx/GetData",
        data: "{Action : '3'}",
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d != "") {
                var msg = data.d;
                var dataTemp = msg.split('$');
                $("#hdnShortcutCount").val(dataTemp[1].toString());
                $("div[id*=divShortcut]").html(dataTemp[0].toString());
            }
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });
}

///**************** end shortcuts ****************///

///**************** Prevent Caret Key  ****************///

function PreventCaretKey() {
    $("input").keypress(function (e) {
        if (e.which === 94) {
            return false;
        }
    });
    $("textarea").keypress(function (e) {
        if (e.which === 94) {
            return false;
        }
    });
}

// ******************Authorization ***********************************************************************************
function CheckPageLevelAuthorization(pageURL, pageTitle) {
    $.ajax({
        type: "POST",
        url: "/ASPX/WebServices/CommonWebService.asmx/GetPageLevelAuthorization",
        data: "{Action : '3', MenuLink : '" + pageURL + "'}",
        async: false,
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            if (response.d != "") {
                var data = response.d;
                var dataTemp = data.split('$');
                if (parseInt(dataTemp[0].toString()) == 0) {
                    showAlertToRedirectPage("Error", "You are not authorized to access this page", 'red', "/ASPX/AdminPanel/AdminHome.aspx");
                    return false;
                }
                else {

                    if (window.location.pathname.toUpperCase().indexOf("INFOTYPES") != -1 && dataTemp.length > 1) {
                        if (parseInt(dataTemp[1].toString()) == 0) {
                            $("div[class*=panel-body]").find("input[class*=cls-btnSave]").each(function () {
                                $(this).attr("disabled", "disabled");

                            });
                            $("div[class*=dataTables_wrapper]").find("a[class*=fa-files-o]").each(function () {
                                $(this).css("display", "none");
                            });
                        }
                        if (parseInt(dataTemp[2].toString()) == 0) {
                            $("table[id*=gvData]").find("tbody").find("tr").each(function () {
                                $(this).find("td").find("a[class*=glyphicon-edit]").css("display", "none");
                            });
                        }
                        if (parseInt(dataTemp[3].toString()) == 0) {
                            $("table[id*=gvData]").find("tbody").find("tr").each(function () {
                                $(this).find("td").find("a[class*=glyphicon-trash]").css("display", "none");
                            });
                        }
                    }
                }

            }
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });
}

function SetEmployeeLockUnlock(action, employeeNo) {
    $.ajax({
        type: "POST",
        url: "/ASPX/WebServices/CommonWebService.asmx/SetEmployeeLockUnlock",
        data: "{Action : '" + action + "', EmployeeNo : '" + employeeNo + "'}",
        async: false,
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            if (response.d != "") {
              
            }
        },
        error: function (xhr, status, error) {
            if (xhr.responseJSON.Message == "Session Expired") {
                errorAlert("Error", "Session Expired", true);
            }
            else {
                errorAlert("Error", xhr.responseJSON.Message, false);
            }
            console.log("Error Thrown  - " + xhr.responseText);
        }
    });
}

// ******************Authorization ***********************************************************************************


// ******************dashboard Birthday ***********************************************************************************
/////Show birthday message box
//On form show hide panel on click of left buttons.
$("#ContentPlaceHolder1_bdaylist li a").click(function () {
    var divId = "#ContentPlaceHolder1_" + $(this).attr("class");
    if (currActDvId1 != "") {
        $(currActDvId1).hide("fast");
    }
    $(divId).show("slow");
    $(divId).focus();
    $(divId + " textarea").focus();

    $.scrollTo(divId);
    currActDvId1 = divId;
});
// ******************dashboard Birthday ***********************************************************************************