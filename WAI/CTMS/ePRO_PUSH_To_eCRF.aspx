<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ePRO_PUSH_To_eCRF.aspx.cs" Inherits="CTMS.ePRO_PUSH_To_eCRF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <style>
        .btn-DarkGreen
        {
            background-repeat: repeat-x;
            border-color: #7FB27F;
            color: White;
            background-image: linear-gradient(to bottom, #006600 0%, #006600 100%);
        }
        .btn-DarkGreen:hover, .btn-DarkGreen:focus
        {
            background-color: #006600;
            color: White;
            background-position: 0 -15px;
        }
        .btn-DarkGreen:active, .btn-DarkGreen.active
        {
            background-color: #006600;
            border-color: #245580;
        }
        .btn-DarkGreen.disabled, .btn-DarkGreen[disabled], fieldset[disabled] .btn-DarkGreen, .btn-DarkGreen.disabled:hover, .btn-DarkGreen[disabled]:hover, fieldset[disabled] .btn-DarkGreen:hover, .btn-DarkGreen.disabled:focus, .btn-DarkGreen[disabled]:focus, fieldset[disabled] .btn-DarkGreen:focus, .btn-DarkGreen.disabled.focus, .btn-DarkGreen[disabled].focus, fieldset[disabled] .btn-DarkGreen.focus, .btn-DarkGreen.disabled:active, .btn-DarkGreen[disabled]:active, fieldset[disabled] .btn-DarkGreen:active, .btn-DarkGreen.disabled.active, .btn-DarkGreen[disabled].active, fieldset[disabled] .btn-DarkGreen.active
        {
            background-color: #006600;
            background-image: none;
        }
        
        .btn-DARKORANGE
        {
            background-repeat: repeat-x;
            border-color: #FFB266;
            color: White;
            background-image: linear-gradient(to bottom, #FF8000 0%, #FF8000 100%);
        }
        .btn-DARKORANGE:hover, .btn-DARKORANGE:focus
        {
            background-color: #FF8000;
            color: White;
            background-position: 0 -15px;
        }
        .btn-DARKORANGE:active, .btn-DARKORANGE.active
        {
            background-color: #FF8000;
            border-color: #245580;
        }
        .btn-DARKORANGE.disabled, .btn-DARKORANGE[disabled], fieldset[disabled] .btn-DARKORANGE, .btn-DARKORANGE.disabled:hover, .btn-DARKORANGE[disabled]:hover, fieldset[disabled] .btn-DARKORANGE:hover, .btn-DARKORANGE.disabled:focus, .btn-DARKORANGE[disabled]:focus, fieldset[disabled] .btn-DARKORANGE:focus, .btn-DARKORANGE.disabled.focus, .btn-DARKORANGE[disabled].focus, fieldset[disabled] .btn-DARKORANGE.focus, .btn-DARKORANGE.disabled:active, .btn-DARKORANGE[disabled]:active, fieldset[disabled] .btn-DARKORANGE:active, .btn-DARKORANGE.disabled.active, .btn-DARKORANGE[disabled].active, fieldset[disabled] .btn-DARKORANGE.active
        {
            background-color: #FF8000;
            background-image: none;
        }
    </style>
    <script language="javascript" type="text/javascript">

        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.Mandatory').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == undefined && $(this).hasClass('radio')) {
                    ctrl = 'radio';
                }
                else if (ctrl == undefined && $(this).hasClass('checkbox')) {
                    ctrl = 'checkbox';
                }

                if (ctrl == "select-one") {
                    if ($(this).closest('tr').hasClass('disp-none') == false) {
                        if (value == "0" || value == "--Select--" || value == null) {
                            $(this).addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $(this).removeClass("brd-1px-redimp");
                        }
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    var prefix = $(this).closest('tr').find('td').eq(14).text().trim();
                    if ($(this).closest('tr').hasClass('disp-none') == false) {
                        if (value.trim() == "" || value.trim() == prefix) {
                            $(this).addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $(this).removeClass("brd-1px-redimp");
                        }
                    }
                }
                else if (ctrl == 'radio') {
                    var ctrlArr = $(this).closest('td').find("input[id*='RAD_FIELD']:checked").toArray();
                    if ($(this).closest('tr').hasClass('disp-none') == false) {
                        if (ctrlArr.length < 1) {
                            $(this).closest('td').addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $(this).closest('td').removeClass("brd-1px-redimp");
                        }
                    }
                }
                else if (ctrl == 'checkbox') {
                    var ctrlArr = $(this).closest('td').find("input[id*='CHK_FIELD']:checked").toArray();
                    if ($(this).closest('tr').hasClass('disp-none') == false) {
                        if (ctrlArr.length < 1) {
                            $(this).closest('td').addClass("brd-1px-redimp");
                            test = "1";
                        }
                        else {
                            $(this).closest('td').removeClass("brd-1px-redimp");
                        }
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

    </script>
    <script language="javascript" type="text/javascript">

        $(function () {

            $('.txtDateMask').inputmask(
                    "dd/mm/yyyy", {
                        placeholder: "dd/mm/yyyy"
                    }
                                      );


        });

        $(function () {

            $('.txtTime').inputmask(
        "hh:mm", {
            placeholder: "HH:MM",
            insertMode: false,
            showMaskOnHover: false,
            hourFormat: "24"
        }
      );

        });
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
            margin-right: 142px;
        }
        .style2
        {
            width: 540px;
            background-color: #E98454;
        }
        .table-bordered > tbody > tr > td
        {
            border: none;
        }
        .table-bordered
        {
            border-color: transparent;
        }
        .fontbold
        {
            font-weight: bold;
        }
        
        .strikeThrough
        {
            text-decoration: line-through;
        }
        .inpputchkCriticalDP
        {
            outline: 2px #c00 solid;
            outline-offset: -2px;
        }
        .radio, .checkbox
        {
            margin-bottom: 0px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function tab(e) {
            if (e.which == 13) {
                e.preventDefault();
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function isValidDate(s) {
            var bits = s.split('-');
            var d = new Date(bits[2] + '-' + bits[1] + '-' + bits[0]);
            return !!(d && (d.getMonth() + 1) == bits[1] && d.getDate() == Number(bits[0]));
        }

        function getFirstDate() {

            var dates = [];

            var i = 0;

            var dateField = $("span:contains('Start '):contains('Date')").toArray();
            for (i = 0; i < dateField.length; ++i) {

                if ($(dateField[i]).closest('tr').find('td').eq(7).find('input').val() != '') {
                    if ($(dateField[i]).closest('tr').find('td').eq(7).find('input').val() != undefined) {

                        dates.push(new Date($(dateField[i]).closest('tr').find('td').eq(7).find('input').val()));

                    }

                }
            }

            if (dates.length > 0) {

                var firstDate = new Date(Math.min.apply(null, dates)).toShortFormat();

                $("span:contains('first Symptom')").closest('tr').find('td').eq(7).find('input').val(firstDate);
            }

        }

        Date.prototype.toShortFormat = function () {

            var month_names = ["Jan", "Feb", "Mar",
                      "Apr", "May", "Jun",
                      "Jul", "Aug", "Sep",
                      "Oct", "Nov", "Dec"];

            var day = this.getDate();
            if (day.toString().length == 1) {
                day = '0' + day;
            }
            var month_index = this.getMonth();
            var year = this.getFullYear();

            return "" + day + "-" + month_names[month_index] + "-" + year;
        }

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        function stringContains(arrString, valString) {

            var arrMain = arrString.split('^');

            return (arrMain.indexOf(valString) > -1);
        }

        function showChild(element) {

            var MODULENAME = $("#lblModuleName").text();
            var MODULEID = getUrlVars()["MODULEID"];

            var Child_Val;
            if ($(element).hasClass("radio") || $(element).hasClass("checkbox")) {
                Child_Val = $(element).context.lastChild.textContent.trim();
            }
            else {
                if ($(element).val() != null) {
                    Child_Val = $(element).val().trim();
                }
            }

            if (Child_Val == 'RAD_FIELD' || Child_Val == 'CHK_FIELD') {
                Child_Val = '';
            }


            var SelfTableName = $(element).closest('table').attr('id');
            var TableName = $($(element).closest('tr').next('tr').find('table').toArray()[0]).attr('id');


            var Upper = $("#" + TableName + "").find('tr').eq(0).find('td').eq(7).find('input').val();
            var Lower = $("#" + TableName + "").find('tr').eq(2).find('td').eq(7).find('input').val();

            if ($(element).hasClass("checkbox")) {
                var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
                    if (TableName != 'MainContent_grd_Data' && TableName != SelfTableName) {

                        if ($.trim($(this).find('td').eq(15).text().trim()) == "Is Not Blank") {
                            if ($($(element).context.getElementsByTagName("input")).prop('checked') == true) {
                                if ($(this).hasClass('disp-none')) {
                                    $(this).removeClass('disp-none');
                                }

                                if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                    $(element).closest('tr').next('tr').removeClass('disp-none');
                                }
                            }
                            else {
                                if ($(this).hasClass('disp-none')) {
                                    $(this).removeClass('disp-none');
                                    $(this).addClass('disp-none');
                                }
                                else {
                                    $(this).addClass('disp-none');
                                }
                            }

                        }
                        else if ($.trim($(this).find('td').eq(15).text().trim()) == "Compare") {

                            if (parseInt(Child_Val) > parseInt(Upper) || parseInt(Child_Val) < parseInt(Lower)) {
                                if ($(this).hasClass('disp-none')) {
                                    $(this).removeClass('disp-none');
                                }

                                if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                    $(element).closest('tr').next('tr').removeClass('disp-none');
                                }
                            }
                            else {
                                if ($(this).hasClass('disp-none')) {
                                    $(this).removeClass('disp-none');
                                    $(this).addClass('disp-none');
                                }
                                else {
                                    $(this).addClass('disp-none');
                                }
                            }

                        }
                        else if ($(this).find('td').toArray().length > 5) {
                            if (stringContains($.trim($(this).find('td').eq(15).text().trim()), Child_Val)) {
                                if ($($(element).context.getElementsByTagName("input")).prop('checked') == true) {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                    }

                                    if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                        $(element).closest('tr').next('tr').removeClass('disp-none');
                                    }
                                }
                                else {
                                    if ($(this).hasClass('disp-none')) {
                                    }
                                    else {
                                        $(this).addClass('disp-none');
                                    }

                                }
                            }
                        }
                    }
                })
            }
            else {
                var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
                    if (TableName != 'MainContent_grd_Data' && TableName != SelfTableName) {
                        if ($(this).find('td').toArray().length > 5) {

                            if ($.trim($(this).find('td').eq(15).text().trim()) == "Is Not Blank") {
                                if (Child_Val != "") {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                    }

                                    if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                        $(element).closest('tr').next('tr').removeClass('disp-none');
                                    }
                                }
                                else {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                        $(this).addClass('disp-none');
                                    }
                                    else {
                                        $(this).addClass('disp-none');
                                    }
                                }

                            }
                            else if ($.trim($(this).find('td').eq(15).text().trim()) == "Compare") {

                                if (parseInt(Child_Val) > parseInt(Upper) || parseInt(Child_Val) < parseInt(Lower)) {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                    }

                                    if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                        $(element).closest('tr').next('tr').removeClass('disp-none');
                                    }
                                }
                                else {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                        $(this).addClass('disp-none');
                                    }
                                    else {
                                        $(this).addClass('disp-none');
                                    }
                                }

                            }
                            else if ($(this).find('td').toArray().length > 5) {
                                if (stringContains($.trim($(this).find('td').eq(15).text().trim()), Child_Val)) {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                    }

                                    if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                        $(element).closest('tr').next('tr').removeClass('disp-none');
                                    }
                                }
                                else {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                        $(this).addClass('disp-none');
                                    }
                                    else {
                                        $(this).addClass('disp-none');
                                    }
                                }
                            }
                            else {
                                if ($(this).hasClass('disp-none')) {
                                    $(this).removeClass('disp-none');
                                    $(this).addClass('disp-none');
                                }
                                else {
                                    $(this).addClass('disp-none');
                                }
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }
                        }
                    }
                })
            }
        }
        
    </script>
    <script language="javascript" type="text/javascript">
        function OpenModule(element) {
            var MODULE_ID = $(element).closest('tr').find('td:eq(16)').find('span').text();
            var MODULE_NAME = $(element).closest('tr').find('td:eq(17)').find('span').text();
            var MULTIPLE = $(element).closest('tr').find('td:eq(18)').find('span').text();
            var INVID = /[^:]*$/.exec($("#MainContent_lblSiteId").text())[0].trim();
            var SUBJID = /[^:]*$/.exec($("#MainContent_lblSubjectId").text())[0].trim();
            var VISIT = /[^:]*$/.exec($("#MainContent_lblVisit").text())[0].trim();
            var VISITID = /[^:]*$/.exec($("#MainContent_hfDM_VISITNUM").val())[0].trim();
            var Indication = /[^:]*$/.exec($("#MainContent_lblIndication").text())[0].trim();

            var REF = $('.REFERENCE').val()

            if (REF != '' && REF != undefined) {

                if (MODULE_ID != "0") {

                    if (MULTIPLE == "True") {
                        var test = "DM_DataEntry_MultipleData.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&Indication=" + Indication + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + REF;
                        var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                        window.open(test, '_blank');
                        return false;
                    }
                    else {
                        var test = "DM_DataEntry.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&Indication=" + Indication + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1&REFERENCE=" + REF;
                        var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                        window.open(test, '_blank');
                        return false;
                    }
                }

            }
            else {

                if (MODULE_ID != "0") {

                    if (MULTIPLE == "True") {
                        var test = "DM_DataEntry_MultipleData.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&Indication=" + Indication + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                        var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                        window.open(test, '_blank');
                        return false;
                    }
                    else {
                        var test = "DM_DataEntry.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME + "&VISITID=" + VISITID + "&Indication=" + Indication + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&OPENLINK=1";
                        var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                        window.open(test, '_blank');
                        return false;
                    }
                }

            }

        }
    </script>
    <script language="javascript" type="text/javascript">
        $(function callChange() {

            var a;
            var b;
            var c;
            var d;
            var e = 0;

            var DRP_FIELD = '';
            var TXT_FIELD = '';
            var CHK_FIELD1 = '';
            var CHK_FIELD = '';
            var RAD_FIELD1 = '';
            var RAD_FIELD = '';
            var CONTROLTYPE = '';


            DRP_FIELD = $('.ShowChild2').find("select[id*='DRP_FIELD']").toArray();
            for (a = 0; a < DRP_FIELD.length; ++a) {
                showChild(DRP_FIELD[a]);
            }

            TXT_FIELD = $('.ShowChild2').find("input[id*='TXT_FIELD']").toArray();
            for (a = 0; a < TXT_FIELD.length; ++a) {
                showChild(TXT_FIELD[a]);
            }

            CHK_FIELD1 = $('.ShowChild2').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
            for (c = 0; c < CHK_FIELD1.length; ++c) {
                showChild(CHK_FIELD1[c]);
            }

            CHK_FIELD = $('.ShowChild2').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
            for (c = 0; c < CHK_FIELD.length; ++c) {
                showChild(CHK_FIELD[c]);
            }

            RAD_FIELD1 = $('.ShowChild2').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
            for (d = 0; d < RAD_FIELD1.length; ++d) {
                showChild(RAD_FIELD1[d]);
            }

            RAD_FIELD = $('.ShowChild2').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
            for (d = 0; d < RAD_FIELD.length; ++d) {
                showChild(RAD_FIELD[d]);
            }

            CONTROLTYPE = $('.ShowChild2').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
            for (e = 0; e < CONTROLTYPE.length; ++e) {
                SetHeader(CONTROLTYPE[e]);
            }



            DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
            for (a = 0; a < DRP_FIELD.length; ++a) {
                showChild(DRP_FIELD[a]);
            }

            TXT_FIELD = $('.ShowChild3').find("input[id*='TXT_FIELD']").toArray();
            for (a = 0; a < TXT_FIELD.length; ++a) {
                showChild(TXT_FIELD[a]);
            }

            CHK_FIELD1 = $('.ShowChild3').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
            for (c = 0; c < CHK_FIELD1.length; ++c) {
                showChild(CHK_FIELD1[c]);
            }

            CHK_FIELD = $('.ShowChild3').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
            for (c = 0; c < CHK_FIELD.length; ++c) {
                showChild(CHK_FIELD[c]);
            }

            RAD_FIELD1 = $('.ShowChild3').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
            for (d = 0; d < RAD_FIELD1.length; ++d) {
                showChild(RAD_FIELD1[d]);
            }

            RAD_FIELD = $('.ShowChild3').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
            for (d = 0; d < RAD_FIELD.length; ++d) {
                showChild(RAD_FIELD[d]);
            }

            CONTROLTYPE = $('.ShowChild3').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
            for (e = 0; e < CONTROLTYPE.length; ++e) {
                SetHeader(CONTROLTYPE[e]);
            }



            DRP_FIELD = $('.ShowChild3').find("select[id*='DRP_FIELD']").toArray();
            for (a = 0; a < DRP_FIELD.length; ++a) {
                showChild(DRP_FIELD[a]);
            }

            TXT_FIELD = $('.ShowChild4').find("input[id*='TXT_FIELD']").toArray();
            for (a = 0; a < TXT_FIELD.length; ++a) {
                showChild(TXT_FIELD[a]);
            }

            CHK_FIELD1 = $('.ShowChild4').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
            for (c = 0; c < CHK_FIELD1.length; ++c) {
                showChild(CHK_FIELD1[c]);
            }

            CHK_FIELD = $('.ShowChild4').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
            for (c = 0; c < CHK_FIELD.length; ++c) {
                showChild(CHK_FIELD[c]);
            }

            RAD_FIELD1 = $('.ShowChild4').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
            for (d = 0; d < RAD_FIELD1.length; ++d) {
                showChild(RAD_FIELD1[d]);
            }

            RAD_FIELD = $('.ShowChild4').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
            for (d = 0; d < RAD_FIELD.length; ++d) {
                showChild(RAD_FIELD[d]);
            }

            CONTROLTYPE = $('.ShowChild4').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
            for (e = 0; e < CONTROLTYPE.length; ++e) {
                SetHeader(CONTROLTYPE[e]);
            }




            DRP_FIELD = $('.ShowChild1').find("select[id*='DRP_FIELD']").toArray();
            for (a = 0; a < DRP_FIELD.length; ++a) {
                showChild(DRP_FIELD[a]);
            }

            TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
            for (a = 0; a < TXT_FIELD.length; ++a) {
                showChild(TXT_FIELD[a]);
            }

            CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
            for (c = 0; c < CHK_FIELD1.length; ++c) {
                showChild(CHK_FIELD1[c]);
            }

            CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
            for (c = 0; c < CHK_FIELD.length; ++c) {
                showChild(CHK_FIELD[c]);
            }

            RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
            for (d = 0; d < RAD_FIELD1.length; ++d) {
                showChild(RAD_FIELD1[d]);
            }

            RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
            for (d = 0; d < RAD_FIELD.length; ++d) {
                showChild(RAD_FIELD[d]);
            }

            CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
            for (e = 0; e < CONTROLTYPE.length; ++e) {
                SetHeader(CONTROLTYPE[e]);
            }


            DRP_FIELD = $('.ShowChild1').find("select[id*='DRP_FIELD']").toArray();
            for (a = 0; a < DRP_FIELD.length; ++a) {
                showChild(DRP_FIELD[a]);
            }

            TXT_FIELD = $('.ShowChild1').find("input[id*='TXT_FIELD']").toArray();
            for (a = 0; a < TXT_FIELD.length; ++a) {
                showChild(TXT_FIELD[a]);
            }

            CHK_FIELD1 = $('.ShowChild1').find("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
            for (c = 0; c < CHK_FIELD1.length; ++c) {
                showChild(CHK_FIELD1[c]);
            }

            CHK_FIELD = $('.ShowChild1').find("input[id*='CHK_FIELD']:checked").closest('span').toArray();
            for (c = 0; c < CHK_FIELD.length; ++c) {
                showChild(CHK_FIELD[c]);
            }

            RAD_FIELD1 = $('.ShowChild1').find("input[id*='RAD_FIELD']:not(:checked)").toArray();
            for (d = 0; d < RAD_FIELD1.length; ++d) {
                showChild(RAD_FIELD1[d]);
            }

            RAD_FIELD = $('.ShowChild1').find("input[id*='RAD_FIELD']:checked").closest('span').toArray();
            for (d = 0; d < RAD_FIELD.length; ++d) {
                showChild(RAD_FIELD[d]);
            }

            CONTROLTYPE = $('.ShowChild1').find("span[id*='lblCONTROLTYPE']").closest('span').toArray();
            for (e = 0; e < CONTROLTYPE.length; ++e) {
                SetHeader(CONTROLTYPE[e]);
            }

            var f = 0;

            var HEADERS = $('span:contains(HEADER)').toArray();
            for (f = 0; f < HEADERS.length; ++f) {
                HideHeaderIcons(HEADERS[f]);
            }

            f = 0;
            var TXTDATEMASK = $(".txtDateMask").toArray();
            for (f = 0; f < TXTDATEMASK.length; ++f) {
                SetTextInputMask(TXTDATEMASK[f]);
            }

        });

        function HideHeaderIcons(element) {

            $(element).closest('tr').find('td').eq(11).addClass('disp-none');

        }

        function SetHeader(element) {

            if ($(element).text() == 'HEADER') {
                $(element).closest('td').next('td').attr('colspan', '2');
            }

        }

        function SetTextInputMask(element) {

            var hdnVal = $(element).next().val();
            if (hdnVal != '') {
                $(element).val(hdnVal);
            }

        }

        function callChange() {

            var a;
            var b;
            var c;
            var d;


            var DRP_FIELD = $("select[id*='DRP_FIELD']").toArray();
            for (a = 0; a < DRP_FIELD.length; ++a) {
                showChild(DRP_FIELD[a]);
            }

            var TXT_FIELD = $("input[id*='TXT_FIELD']").toArray();
            for (a = 0; a < TXT_FIELD.length; ++a) {
                showChild(TXT_FIELD[a]);
            }

            var CHK_FIELD1 = $("input[id*='CHK_FIELD']:not(:checked)").closest('span').toArray();
            for (c = 0; c < CHK_FIELD1.length; ++c) {
                showChild(CHK_FIELD1[c]);
            }

            var CHK_FIELD = $("input[id*='CHK_FIELD']:checked").closest('span').toArray();
            for (c = 0; c < CHK_FIELD.length; ++c) {
                showChild(CHK_FIELD[c]);
            }

            var RAD_FIELD1 = $("input[id*='RAD_FIELD']:not(:checked)").toArray();
            for (d = 0; d < RAD_FIELD1.length; ++d) {
                showChild(RAD_FIELD1[d]);
            }

            var RAD_FIELD = $("input[id*='RAD_FIELD']:checked").closest('span').toArray();
            for (d = 0; d < RAD_FIELD.length; ++d) {
                showChild(RAD_FIELD[d]);
            }

            var e = 0;
            var CONTROLTYPE = $("span[id*='lblCONTROLTYPE']").closest('span').toArray();
            for (e = 0; e < CONTROLTYPE.length; ++e) {
                SetHeader(CONTROLTYPE[e]);
            }

            var f = 0;
            var TXTDATEMASK = $(".txtDateMask").toArray();
            for (f = 0; f < TXTDATEMASK.length; ++f) {
                SetTextInputMask(TXTDATEMASK[f]);
            }

        }

        $(function checkCritcalDp() {

            var chk;

            var CHKs = $(".chkCriticalDP").toArray();

            for (chk = 0; chk < CHKs.length; ++chk) {

                $('#' + $(CHKs[chk]).find('input').attr('id')).addClass('inpputchkCriticalDP');

            }

        });

    </script>
    <script type="text/javascript" language="javascript">
        function RadioCheck(rb) {
            //            var gv = document.getElementById($(rb).closest('table').attr('id'));
            var rbs = rb.parentNode.parentNode.parentNode.getElementsByTagName("input");
            var row = rb.parentNode.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript">

        function GetDefaultData(element) {
            var dataFreezeLockStatus = '<%= Session["DATAFREEZELOCKSTATUS"] %>';
            if (dataFreezeLockStatus != '1') {
                var LabID = $(element).val(); //bindin  

                $.ajax({
                    type: "POST",
                    url: "DM_DataEntry_MultipleData.aspx/GetDefaultData",
                    data: '{LabID: "' + LabID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                    },
                    failure: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }

                    }
                });

                location.reload(true);
            }
        }

    
    </script>
    <script>
        var CurrentObj = "";
        var CurrentObjType = "";
        var counter = 0;
        var query = "0";
        var controlType;
        var overrideid;

//        var DM_TABLENAME=$("#MainContent_hfTablename").val();
//        var DM_PVID=$("#MainContent_hfDM_PVID").val();
//        var DM_PAGESTATUS=$("#MainContent_hfDM_PAGESTATUS").val();
//        var DM_RECID=$("#MainContent_hfDM_RECID").val();
//        var DM_DATAFREEZELOCKSTATUS=$("#MainContent_hfDM_DATAFREEZELOCKSTATUS").val();

        $(document).ready(function () {

        if ($(location).attr('href').indexOf('NIWRS_FORM') != -1) {

                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050],
                        maxDate: new Date(),  
                    });
                });

            }
            else {

                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050]
                    });
                });

            }

            
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

            if ($("#MainContent_hfDM_DATAFREEZELOCKSTATUS").val() == '0') {
                $('.PageStatus').each(function (index, element) {
                    $(element).addClass("disp-none")
                });
            }
            else {
                $('.PageStatus').each(function (index, element) {
                    $(element).removeClass("disp-none")

                });
            }


            // only CRA Group can source data verify
            if ($("#MainContent_hfDM_DATAFREEZELOCKSTATUS").val() == '0') {
                HighlightControl();
                FillAuditDetails();
            }
            else {

                $('.Comments').each(function (index, element) {
                    $(element).removeClass("disp-none")
                });
                //only CRA and CDM  Group can raise query
                //if ('<%= Session["UserGroup_ID"] %>' == 'CRA' || '<%= Session["UserGroup_ID"] %>' == 'CDM' || '<%= Session["UserGroup_ID"] %>' == 'Administrator') {
                    $('.raiseManualQuery').each(function (index, element) {
                        $(element).removeClass("disp-none")
                    });
                //}
            }
            

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
            //only numeric and decimal value Accept
            $('.numericdecimal').keypress(function (event) {
                // Allow only backspace and delete
                if (event.charCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                    // let it happen, don't do anything
                    return true;
                }
                if (event.which == 46 && $(this).val().indexOf('.') != -1) {
                    this.value = '';
                }
                event.preventDefault();

            });

            $('.txtDate').keypress(function (event) {
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
                    //let it happen, don't do anything
                    return true;
                }
                event.preventDefault();
                return false;
            });

            HighlightControl();
            FillSDVDetails();
            FillAuditDetails();
            FillCommentsDetails();


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

         //show audit details icon of field 
        function FillAuditDetails() {
            var count = 0;
            $("#MainContent_grdAUDITTRAILDETAILS tr").each(function () {
                if (count > 0) {
                    var contId = $(this).find('td:eq(2)').find('input').val().trim();
                    var tableName = 'grd_Data';
                    var variableName = $(this).find('td:eq(1)').find('input').val().trim();

                    var element = $("img[id*='_AD_" + variableName + "_']").attr('id');
                    $("#" + element).removeClass("disp-none");
                }
                count++;
            });

        }

        //fill comments details icon change
        function FillCommentsDetails() {
            var count = 0;
            $("#MainContent_grdComments tr").each(function () {
                if (count > 0) {
                    var contId = $(this).find('td:eq(0)').find('input').val().trim();
                    var tableName = 'grd_Data';
                    var variableName = $(this).find('td:eq(2)').find('input').val().trim();

                    var element = $("img[id*='_CM_" + variableName + "_" + contId + "']").attr('id');
                    $("#" + element).attr("src", "Images/index_3.png");
                }
                count++;
            });

        }



        function isValidTime(text) {
            // var regexp = new RegExp(/^([0-2][0-9]):([0-5][0-9])$/)
            var regexp = new RegExp(/^([01]?[0-9]|2[0-4]):[0-5][0-9]$/)
            //  ([01]?[0-9]|2[0-3]):[0-5][0-9]
            return regexp.test(text);
        }

        //highlight control if auto query or manual query on particular field
        function HighlightControl() {
            var count = 0;
            $("#MainContent_grdQUERYDETAILS tr").each(function () {
                if (count > 0) {
                    var contId = $(this).find('td:eq(2)').find('input').val().trim();
                    var tableName = 'grd_Data';
                    var variableName = $(this).find('td:eq(8)').find('input').val().trim();
                    var pvid = $(this).find('td:eq(1)').find('input').val().trim();
                    var status = $(this).find('td:eq(9)').find('input').val().trim();
                    var queryType = $(this).find('td:eq(11)').find('input').val().trim();


                    var CurrentPVID = $("#MainContent_hfDM_PVID").val(); //get current page pvid

                    var element = $("img[id*='_GMQ_" + variableName + "_']").attr('id');


                    if (CurrentPVID === pvid) {
                        if (queryType == 'Manual') {
                            var element = $("img[id*='_MQ_" + variableName + "_']").attr('id');
                            $("#" + element).removeClass("disp-none");
                            var element = $("img[id*='_GMQ_" + variableName + "_']").attr('id');
                            $("#" + element).addClass("disp-none");
                        }
                        else {
                            var element = $("img[id*='_AQ_" + variableName + "_']").attr('id');
                            $("#" + element).removeClass("disp-none");
                        }
                    }

                    var element = $("input[id*='_" + variableName + "_']").attr('id');
                    controlType = $('#' + element).closest('tr').find('td:eq(2)').find('span').html();

                    if (controlType == 'DROPDOWN') {
                        element = $('#' + element).closest('tr').find('td:eq(7)').find('select').attr('id');
                    }
                    else {
                        element = $('#' + element).closest('tr').find('td:eq(7)').find('input').attr('id');
                    }
                    if (CurrentPVID === pvid) {
                        if (status == "0") {
                            if ($("#" + element).hasClass("bkcolor-red")) {
                                $("#" + element).removeClass("bkcolor-red")
                            }
                            if ($("#" + element).hasClass("bkcolor-orange")) {
                                $("#" + element).removeClass("bkcolor-orange")
                            }
                            $("#" + element).addClass("bkcolor-red");
                            if ($("#" + element).parent().hasClass("checkbox")) {
                                $("#" + element).parent().addClass("bkcolor-red")
                            }
                        }
                        else {
                            if ($("#" + element).hasClass("bkcolor-red")) {
                                $("#" + element).removeClass("bkcolor-red")
                            }
                            if ($("#" + element).hasClass("bkcolor-orange")) {
                                $("#" + element).removeClass("bkcolor-orange")
                            }
                            $("#" + element).addClass("bkcolor-orange");
                            if ($("#" + element).prev().hasClass("checkbox")) {
                                $("#" + element).prev().addClass("bkcolor-orange")
                            }
                        }
                    }


                    
                }
                count++;
            });
            return false;
        }

        function ResolveHighlightControl(lnk) {
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var pvid = row.cells[1].getElementsByTagName("input")[0].value.trim();
            var contId = row.cells[2].getElementsByTagName("input")[0].value.trim();
            var tableName = 'MainContent_grd_Data';
            var variableName = row.cells[8].getElementsByTagName("input")[0].value.trim();


            var CurrentPVID = $("#MainContent_hfDM_PVID").val(); //get current page pvid

            if (CurrentPVID === pvid) {
                var element = $("." + variableName + "").attr('id');
                controlType = $('#' + element).closest('tr').find('td:eq(2)').find('span').html();
                if (controlType == 'DROPDOWN') {
                    element = $('#' + element).closest('tr').find('td:eq(7)').find('select').attr('id');
                }
                else {
                    element = $('#' + element).closest('tr').find('td:eq(7)').find('input').attr('id');
                }
                $("#" + element).closest('td').addClass("border3pxsolidblack");
            }

            return false;
        }



        //check uncheck sdv verify field
        function FillSDVDetails() {
            var count = 0;
            $("#grdSDVDETAILS tr").each(function () {
                if (count > 0) {
                    var contId = $(this).find('td:eq(2)').find('input').val().trim();
                    var tableName = 'grd_Data'
                    var variableName = $(this).find('td:eq(4)').find('input').val().trim();
                    var status = $(this).find('td:eq(5)').find('input').val().trim();

                    var element = $("input[id*='C_" + variableName + "']").attr('id');
                    var hfelement = $('#' + element).closest('td').find('input:hidden').attr('id');

                    if (status == "False") {

                        //if ('<%= Session["UserGroup_ID"] %>' != 'CRA' || 'Administrator') {
                            if (!$("#" + element).hasClass("disp-none"));
                            {
                                $("#" + element).addClass("disp-none");
                            }
                        //}
                        $("#" + element).prop("checked", false);
                        $("#" + hfelement).val("false");
                    }
                    if (status == "True") {
                        if ($("#" + element).hasClass("disp-none"));
                        {
                            $("#" + element).removeClass("disp-none");
                        }
                        $("#" + element).prop("checked", true);
                        $("#" + hfelement).val("true");
                    }
                }
                count++;
            });
        }



        function GenerateManualQuery(element) {
            //check login user type if invetigator and data entry is completed and open status  is update mode then only update popup will open
            var userGroupId = '<%= Session["UserGroup_ID"] %>';
            var pageStatus = $("#MainContent_hfDM_PAGESTATUS").val();
            var dataFreezeLockStatus = $("#MainContent_hfDM_DATAFREEZELOCKSTATUS").val();

            if (dataFreezeLockStatus == '1') {
                // if (dataFreezeLockStatus == '1') {

                //  var tableName = $(element).parent('td').parent('tr').parent('tbody').parent('table').attr('id');
                var HTMLtableName = $(element).parent('td').parent('tr').parent('tbody').parent('table').attr('id');

                var moduleName = $('#MainContent_lblModuleName').html(); //$(element).closest('table').attr('title'); //module name of form
                var tableName = $(element).closest('tr').find('td:eq(5)').find('span').html(); //$(element).closest('table').attr('Name'); //table name of database
                var contId = $(element).closest('tr').find('td:first').find('input').val(); //unique id for each row
                var fieldName = $(element).attr('fieldname'); //column name of particular control                                            
                var variablename = $(element).attr('variablename'); //variable name
                var fieldvalue = $("#MainContent_" + HTMLtableName + "_" + variablename + "_" + contId).attr('value');
                var DM_PVID=$("#MainContent_hfDM_PVID").val();
                var DM_RECID=$("#MainContent_hfDM_RECID").val();

                // getting old manual query on fields
                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/GetMQHistory",
                    data: '{VariableName: "' + variablename + '",ContId: "' + contId + '",TableName: "' + tableName + '",DM_PVID: "' + DM_PVID + '",DM_RECID: "' + DM_RECID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            $('#grdoldManualQuery').html(data.d)
                        }
                    },
                    failure: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }

                    }
                });



                var Qid = "";
                var fieldtype = $(element).closest('td').prev('td').children(); //unique id for each row
                if ($(fieldtype).is("input")) {
                    if (fieldvalue != '') {
                        Qid = variablename + "=" + fieldvalue;
                    }
                    else {
                        Qid = variablename + "=Null";
                    }
                }
                if ($(fieldtype).is("select")) {
                    if (fieldvalue != 0) {
                        Qid = variablename + "=" + fieldvalue;
                    }
                    else {
                        Qid = variablename + "=Null";
                    }
                }
                if ($(fieldtype).is("span")) {
                    var fieldvalue = $("#MainContent_" + HTMLtableName + "_" + variablename + "_" + contId).attr('checked');
                    if (fieldvalue == 'true') {
                        Qid = variablename + "=" + fieldvalue;
                    }
                    else {
                        Qid = variablename + "=Null";
                    }
                }



                $('#txt_MQModule').val(moduleName);
                $('#txt_MQTable').val(tableName);
                $('#txt_MQFieldName').val(fieldName);
                $('#txt_MQContId').val(contId);
                $('#txt_MQVariable').val(variablename);
                $('#txt_MQQueryText').val("");
                $('#txt_MQQID').val(Qid);

                $("#popup_ManualQuery").dialog({
                    title: "Raise Manual Query",
                    width: "auto",
                    height: "auto",
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
                // }
            }
            return false;
        }
        

        //onfocus of any control this function will call    
        function myFocus(element) {
            var test = '';
            var td = $(element).closest('td');

            if ($(element).hasClass('radio')) {
                CurrentObj = $(element).children().attr('id');

                var radiobtn = $(td).find("input[id*='RAD_FIELD']:checked").attr('id');
                test = $($('#' + radiobtn).siblings().first()).text();
            }
            else if ($(element).hasClass('checkbox')) {
                CurrentObj = $(element).children().attr('id');

                var radiobtn = $(td).find("input[id*='CHK_FIELD']:checked").toArray();

                var a = 0;

                for (a = 0; a < radiobtn.length; ++a) {

                if(test=='')
                {
                test = $($('#' + $(radiobtn[a]).attr('id')).siblings().first()).text();
                }
                else{
                test = test + ',' + $($('#' + $(radiobtn[a]).attr('id')).siblings().first()).text();
                }

                }
            }
            else if ($(element).is("select")) {

                    if($(element).val().trim()=='0')
                    {
                        test = '';
                    }
                    else
                    {
                        test = $(element).val().trim();
                    }
            }
            else {
                test = $(element).val().trim();
            }


            $('#MainContent_hdn_Value').val(test);
            counter++;
            // alert(test);
        }

        $(document).ready(function () {

            $('.rightClick').mousedown(function (event) {
                switch (event.which) {
                    case 3:
                        myRightFunction(this);
                        break;
                }
            });

        });

        function setBlankData(element)
        {
        
            if($(element).hasClass('radio') || $(element).hasClass('radio'))
            {
                var rb = element;
                var rbs = rb.parentNode.parentNode.parentNode.getElementsByTagName("input");
                var row = rb.parentNode.parentNode.parentNode;
                for (var i = 0; i < rbs.length; i++) {
                    if (rbs[i].type == "radio" || rbs[i].type == "checkbox") {
                            $('#'+rbs[i].id).prop('checked', false);
                    }
                }
            }
            else if($(element).attr('type') == 'text')
            {
                $(element).val('');
            }
            else if($(element).context.tagName == 'SELECT')
            {
                $(element).prop('selectedIndex', 0);
            }
            
        }

        //onRightclick event of any control in gridview this function will call
        function myRightFunction(element) {

            setBlankData(element);
                
            callChange();

            //check login user type if invetigator and data entry is completed and open status  is update mode then only update popup will open
            var userGroupId = '<%= Session["UserGroup_ID"] %>';
            var dataFreezeLockStatus = 1;

            if (dataFreezeLockStatus == '1') {

                //when multiple rows are added then in last added rows insert value in control vaoid popup
                var updateFlag = $(element).closest('tr').find('td:eq(14)').find('input').attr('Name'); //table name of database
                if (updateFlag != undefined) {
                    var test1 = updateFlag.split('$');
                    var test2 = test1.pop(); //get last elements of array
                    if (test2 == 'UPDATE_FLAG') {
                        var updateflag = $(element).closest('tr').find('td:eq(14)').find('input').val(); //unique id for each row
                        if (updateflag == '0') {
                            return false;
                        }
                    }
                }
                
                var hdnValue = $('#MainContent_hdn_Value').val().trim();

                //used for onfirstime binding grid  date control change event is called so prevent that
                if (($(element).hasClass("txtDate") || $(element).hasClass("txtDateNoFuture")) && hdnValue == "" && counter == 0) {
                    return false;
                }
                //for add new row function on add click hdnvalue 0 the poup up open so avoid that this function is used
                if (($(element).hasClass("txtDate") || $(element).hasClass("txtDateNoFuture")) && hdnValue == "0") {
                    return false;
                }
                
                var a = 0;
                var ansVal = ''

                elementValue = ansVal;

                if (hdnValue == elementValue) {
                    return false;
                }
                else {
                    //check if normal update or query resolving update
                    if ($(element).hasClass('bkcolor-red') || $(element).hasClass('bkcolor-orange')) {
                        query = "1"
                    }
                    //  var tableName = $(element).parent('td').parent('tr').parent('tbody').parent('table').attr('id');
                    var moduleName = $('#MainContent_lblModuleName').html(); //$(element).closest('table').attr('title'); //module name of form
                    var tableName = $(element).closest('tr').find('td:eq(5)').find('span').html(); //$(element).closest('table').attr('Name'); //table name of database
                    var variableName = $(element).closest('tr').find('td:eq(1)').find('span').html();
                    var contId = $(element).closest('tr').find('td:first').find('input').val(); //unique id for each row
                    var fieldName = $(element).closest('tr').find('td:eq(3)').find('span').html(); //column name of particular control

                    if ($(element).is("span")) {
                        CurrentObjType = $(element).children().attr('type');
                    }
                    else {
                        CurrentObjType = $(element).attr('type');
                        CurrentObj = $(element).attr('id');
                    }

                    if($(element).hasClass('checkbox'))
                    {
                        controlType = "checkbox";
                    }
                    if($(element).hasClass('radio'))
                    {
                        controlType = "radio";
                    }
                    if ($(element).is("input")) {
                        controlType = "textbox"
                    }
                    if ($(element).is("select")) {
                        controlType = "drp"
                    }

                    var test4 = variableName;  //get varaiable name

                    $('#txt_OldValue').val(hdnValue);
                    $('#txt_NewValue').val(elementValue);
                    $('#txt_ModuleName').val(moduleName);
                    $('#txt_TableName').val(tableName);
                    $('#txt_FieldName').val(fieldName);
                    $('#txt_ContId').val(contId);
                    $('#txt_VariableName').val(test4);
                    $('#txt_Comments').val("");
                    $('#drp_Reason').val('0');

                    $("#popup_UpdateData_ePRO").dialog({
                        title: "Reason For Change",
                        width: 460,
                        height: 300,
                        modal: true
                    });
                }
                return false;
            }
            else {
                return false;
            }
        }

            var DM_PVID = '';
            var DM_RECID = '';
            var DM_SUBJID = '';

        //onchange event of any control in gridview this function will call
        function myFunction(element) {

            DM_PVID = $("#MainContent_hfDM_PVID").val();
            DM_RECID = $("#MainContent_hfDM_RECID").val();
            DM_SUBJID = /[^:]*$/.exec($("#MainContent_lblSubjectId").text())[0].trim();

            //check login user type if invetigator and data entry is completed and open status  is update mode then only update popup will open
            var userGroupId = '<%= Session["UserGroup_ID"] %>';
            var dataFreezeLockStatus = 1;

            if (dataFreezeLockStatus == '123' ) {

                //when multiple rows are added then in last added rows insert value in control vaoid popup
                var updateFlag = $(element).closest('tr').find('td:eq(14)').find('input').attr('Name'); //table name of database
                if (updateFlag != undefined) {
                    var test1 = updateFlag.split('$');
                    var test2 = test1.pop(); //get last elements of array
                    if (test2 == 'UPDATE_FLAG') {
                        var updateflag = $(element).closest('tr').find('td:eq(14)').find('input').val(); //unique id for each row
                        if (updateflag == '0') {
                            return false;
                        }
                    }
                }
                
                var hdnValue = $('#MainContent_hdn_Value').val().trim();

                //used for onfirstime binding grid  date control change event is called so prevent that
                if (($(element).hasClass("txtDate") || $(element).hasClass("txtDateNoFuture")) && hdnValue == "" && counter == 0) {
                    return false;
                }
                //for add new row function on add click hdnvalue 0 the poup up open so avoid that this function is used
                if (($(element).hasClass("txtDate") || $(element).hasClass("txtDateNoFuture")) && hdnValue == "0") {
                    return false;
                }
                var elementValue = $(element).val().trim();

                if($(element).hasClass('checkbox'))
                {
                    var radiobtn = $(element).closest('td').find("input[id*='CHK_FIELD']:checked").toArray();

                    var a = 0;
                    var ansVal = ''

                    for (a = 0; a < radiobtn.length; ++a) {

                    if(ansVal=='')
                    {
                    ansVal = $($('#' + $(radiobtn[a]).attr('id')).siblings().first()).text();
                    }
                    else{
                    ansVal = ansVal + ',' + $($('#' + $(radiobtn[a]).attr('id')).siblings().first()).text();
                    }
                    }

                    elementValue = ansVal;

                }
                else if ($(element).is("span")) {
                    elementValue = $(element).context.textContent;
                }
                else if ($(element).is("select")) {
                    if($(element).val().trim()=='0')
                    {
                        elementValue = '';
                    }
                }

                if (hdnValue == elementValue) {
                    return false;
                }
                else {
                    //check if normal update or query resolving update
                    if ($(element).hasClass('bkcolor-red') || $(element).hasClass('bkcolor-orange')) {
                        query = "1"
                    }
                    //  var tableName = $(element).parent('td').parent('tr').parent('tbody').parent('table').attr('id');
                    var moduleName = $('#MainContent_lblModuleName').html(); //$(element).closest('table').attr('title'); //module name of form
                    var tableName = $(element).closest('tr').find('td:eq(5)').find('span').html(); //$(element).closest('table').attr('Name'); //table name of database
                    var variableName = $(element).closest('tr').find('td:eq(1)').find('span').html();
                    var contId = $(element).closest('tr').find('td:first').find('input').val(); //unique id for each row
                    var fieldName = $(element).closest('tr').find('td:eq(3)').find('span').html(); //column name of particular control

                    if ($(element).is("span")) {
                        CurrentObjType = $(element).children().attr('type');
                    }
                    else {
                        CurrentObjType = $(element).attr('type');
                        CurrentObj = $(element).attr('id');
                    }

                    if($(element).hasClass('checkbox'))
                    {
                        controlType = "checkbox";
                    }
                    if($(element).hasClass('radio'))
                    {
                        controlType = "radio";
                    }
                    if ($(element).is("input")) {
                        controlType = "textbox"
                    }
                    if ($(element).is("select")) {
                        controlType = "drp"
                    }

                    var test4 = variableName;  //get varaiable name

                    $('#MainContent_txt_OldValue').val(hdnValue);
                    $('#MainContent_txt_NewValue').val(elementValue);
                    $('#MainContent_txt_ModuleName').val(moduleName);
                    $('#MainContent_txt_TableName').val(tableName);
                    $('#MainContent_txt_FieldName').val(fieldName);
                    $('#MainContent_txt_ContId').val(contId);
                    $('#MainContent_txt_VariableName').val(test4);
                    $('#MainContent_txt_Comments').val("");
                    $('#MainContent_drp_Reason').val('0');

                    $("#popup_UpdateData_ePRO").dialog({
                        title: "Reason For Change",
                        width: 460,
                        height: 300,
                        modal: true
                    });
                }
                return false;
            }
            else {
                return false;
            }
        }

        // when query override then popup will open and with comments it update status 
        function OpenOverideData(lnk) {
            var dataFreezeLockStatus = $("#MainContent_hfDM_DATAFREEZELOCKSTATUS").val();

            if (dataFreezeLockStatus == '1') {
                var userGroupId = '<%= Session["UserGroup_ID"] %>';
//                if (userGroupId == 'CDM' ) {
//                    $('#OverrideComments').addClass("disp-none");
//                }
//                if (userGroupId == 'Investigator' || userGroupId == 'Co_Investigator') {
//                    $('#overrideReason').addClass("disp-none");
//                }

                $('#overrideReason').addClass("disp-none");

                var row = lnk.parentNode.parentNode;
                var rowIndex = row.rowIndex - 1;
                overrideid = row.cells[0].getElementsByTagName("input")[0].value.trim();
                $('#txt_Overrideid').val(overrideid);
                $('#txt_OverrideComm').val("");
                GetOverrideCommnets();
                $("#popup_Override").dialog({
                    title: "Reason for Override",
                    width: 550,
                    height: 300,
                    modal: true
                });
            }
            return false;
        }


        // when query override then popup will open and with comments it update status 
        function OpenOverideFieldQuery(lnk) {
            var userGroupId = '<%= Session["UserGroup_ID"] %>';
//            if (userGroupId == 'CDM' || 'Administrator') {
//                $('#OverrideComments').addClass("disp-none");
//            }
//            if (userGroupId == 'INV') {
//                $('#overrideReason').addClass("disp-none");
//            }

            $('#overrideReason').addClass("disp-none");

            overrideid = $(lnk).closest('tr').find('td:first').text();
            $('#txt_Overrideid').val(overrideid);
            $('#txt_OverrideComm').val("");
            GetOverrideCommnets();
            $("#popup_Override").dialog({
                title: "Reason for Override",
                width: 550,
                height: 300,
                modal: true
            });
            return false;
        }

        //For ajax function call on update data button click
        function UpdateOverrideData() {
            var userGroupId = 'Investigator';
            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/OverrideQueryData",
                data: '{Id: "' + $("#txt_Overrideid")[0].value.trim() + '",InvComments: "' + $("#txt_OverrideComm")[0].value.trim().replace(/"/g, '') + '",Reason: "' + $("#drpoverrideReason option:selected").text() + '",CdmComments: "' + $("#txt_OverrideComm")[0].value.trim().replace(/"/g, '') + '",ActionTakenBy: "' + userGroupId + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert(data.d);
                        location.reload(true)
                    }
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }

                }
            });
        }

        function ShowManualQuery(element) {
            var dataFreezeLockStatus = $("#MainContent_hfDM_DATAFREEZELOCKSTATUS").val();

            if (dataFreezeLockStatus == '1') {

                var variableName = $(element).attr('variablename');
                var contId = $(element).closest('tr').find('td:first').find('input').val(); //unique id for each row
                var tableName = $(element).closest('tr').find('td:eq(5)').find('span').html(); //$(element).closest('table').attr('Name'); //table name of database
                var DM_PVID=$("#MainContent_hfDM_PVID").val();
                var DM_RECID=$("#MainContent_hfDM_RECID").val();

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/GetManualQueryData",
                    data: '{VariableName: "' + variableName + '",ContId: "' + contId + '",TableName: "' + tableName + '",DM_PVID: "' + DM_PVID + '",DM_RECID: "' + DM_RECID + '",TYPE: "Data Entry"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            $('#grdShowManualQuery').html(data.d)
                        }
                    },
                    failure: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }

                    }
                });
                $("#popup_ShowManualQuery").dialog({
                    title: "Manual Query Details",
                    width: 700,
                    height: "auto",
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
                
                if(!$('#divMQAction').hasClass('disp-none'))
                {
                    $('#divMQAction').addClass('disp-none');
                }

            }
        }
        //show auto query onclick of auto query icon
        function ShowAutoQuery(element) {

            var dataFreezeLockStatus = $("#MainContent_hfDM_DATAFREEZELOCKSTATUS").val();
            var DM_PVID=$("#MainContent_hfDM_PVID").val();
            var DM_RECID=$("#MainContent_hfDM_RECID").val();

            if (dataFreezeLockStatus == '1') {

                var variableName = $(element).attr('variablename');
                var contId = $(element).closest('tr').find('td:first').find('input').val(); //unique id for each row
                var tableName = $(element).closest('table').attr('Name'); //table name of database

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/GetFieldQuery",
                    data: '{VariableName: "' + variableName + '",ContId: "' + contId + '",TableName: "' + tableName + '",DM_PVID: "' + DM_PVID + '",DM_RECID: "' + DM_RECID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            $('#grdAutoQuery').html(data.d)
                        }
                    },
                    failure: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }

                    }
                });
                $("#popup_AutoQuery").dialog({
                    title: "Query Details",
                    width: 600,
                    height: "auto",
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
            }
        }
        //For ajax function call on update data button click
        function ShowComments(element) {
            var dataFreezeLockStatus = $("#MainContent_hfDM_DATAFREEZELOCKSTATUS").val();
            if (dataFreezeLockStatus == '1') {
                var variableName = $(element).attr('variablename');
                var contId = $(element).closest('tr').find('td:first').find('input').val(); //unique id for each row
                var tableName = $(element).closest('table').attr('Name'); //table name of database
                var fieldname = $(element).attr('fieldname'); //fieldname 
                var DM_PVID = $("#MainContent_hfDM_PVID").val();
                var DM_RECID=$("#MainContent_hfDM_RECID").val();

                $('#txtFieldContID').val(contId);
                $('#txtFieldTableName').val(tableName);
                $('#txtFieldName').val(variableName);
                $('#txtcolumnName').val(fieldname);

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/GetFielsComments",
                    data: '{VariableName: "' + variableName + '",ContId: "' + contId + '",DM_PVID:"'+DM_PVID+'",DM_RECID: "'+DM_RECID+'"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            $('#grdfieldComments').html(data.d)
                        }
                    },
                    failure: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }

                    }
                });
                $("#popup_FieldComments").dialog({
                    title: "Comments",
                    width: 600,
                    height: "auto",
                    modal: true,
                    buttons: {
                        "Close": function () { $(this).dialog("close"); }
                    }
                });
            }
        }



        function GetAEFilterName(element) {
            var href = $(element).attr('href');

            if (href == 'MM_AEFilter_List.aspx')     {
                var filtername = $(element).find('span').attr('commandargument');
                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/GetFilterData",
                    data: '{FilterName: "' + filtername + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //alert(data.d);
                        // $("#<=GridEnrollemnt.ClientID%>").html(data.d);
                    },
                    failure: function (response) {
                        alert("Contact administrator.")
                    }
                });

            }
            return true;
        }

        //For ajax function call on update data button click
        function showAuditTrail(element) {
            var dataFreezeLockStatus = 1;
            if (dataFreezeLockStatus == '1') {
                var variableName = $(element).attr('variablename'); //bindin  
                var contId = $(element).closest('tr').find('td:first').find('input').val(); //unique id for each row
                var DM_PVID=$("#MainContent_hfDM_PVID").val();
                var DM_RECID=$("#MainContent_hfDM_RECID").val();

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/ePRO_GetAuditTrail",
                    data: '{VariableName: "' + variableName + '",ContId: "' + contId + '",DM_PVID: "' + DM_PVID + '",DM_RECID: "' + DM_RECID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            $('#grdAuditTrail_ePRO').html(data.d)
                            $("#popup_AuditTrail_ePRO").dialog({
                                title: "Audit Trail",
                                width: 560,
                                height: 300,
                                modal: true,
                                buttons: {
                                    "Close": function () { $(this).dialog("close"); }
                                }
                            });
                        }
                    },
                    failure: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }

                    }
                });
            }
        }

        function UpdateManualQueryData() {
            if ($("#txtMQComments")[0].value == '') {
                alert("Please enter comments");
                return false;
            }
            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/ManualQueryUpdate",
                data: '{Id: "' + $("#MQQID_Id").val().trim() + '",Comments: "' + $("#txtMQComments")[0].value.trim().replace(/"/g, '') + '",Reason: "' + $("#drpMQAction option:selected").text() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert(data.d);
                        location.reload(true)
                    }
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
        }

        //onchange event of any control in gridview this function will call
        function AUDIT_myFunction(element) {

            $(element).next().val($(element).val().trim());

            DM_PVID = $("#MainContent_hfDM_PVID").val();
            DM_RECID = $("#MainContent_hfDM_RECID").val();
            DM_SUBJID = /[^:]*$/.exec($("#MainContent_lblSubjectId").text())[0].trim();

            //check login user type if invetigator and data entry is completed and open status  is update mode then only update popup will open
            var userGroupId = '<%= Session["UserGroup_ID"] %>';
            var dataFreezeLockStatus = 1;

            if (dataFreezeLockStatus == '1') {

                //when multiple rows are added then in last added rows insert value in control vaoid popup
                var updateFlag = $(element).closest('tr').find('td:eq(14)').find('input').attr('Name'); //table name of database
                if (updateFlag != undefined) {
                    var test1 = updateFlag.split('$');
                    var test2 = test1.pop(); //get last elements of array
                    if (test2 == 'UPDATE_FLAG') {
                        var updateflag = $(element).closest('tr').find('td:eq(14)').find('input').val(); //unique id for each row
                        if (updateflag == '0') {
                            return false;
                        }
                    }
                }
                var hdnValue = $('#hdn_Value').val().trim();
                //used for onfirstime binding grid  date control change event is called so prevent that
                if (($(element).hasClass("txtDate") || $(element).hasClass("txtDateNoFuture")) && hdnValue == "" && counter == 0) {
                    return false;
                }
                //for add new row function on add click hdnvalue 0 the poup up open so avoid that this function is used
                if (($(element).hasClass("txtDate") || $(element).hasClass("txtDateNoFuture")) && hdnValue == "0") {
                    return false;
                }
                var elementValue = $(element).val().trim();


                if ($(element).hasClass('checkbox')) {
                    var radiobtn = $(element).closest('td').find("input[id*='CHK_FIELD']:checked").toArray();

                    var a = 0;
                    var ansVal = ''

                    for (a = 0; a < radiobtn.length; ++a) {

                        if (ansVal == '') {
                            ansVal = $($('#' + $(radiobtn[a]).attr('id')).siblings().first()).text();
                        }
                        else {
                            ansVal = ansVal + '¸' + $($('#' + $(radiobtn[a]).attr('id')).siblings().first()).text();
                        }
                    }

                    elementValue = ansVal;

                }
                else if ($(element).is("span")) {
                    elementValue = $(element).text();
                }
                else if ($(element).is("select")) {

                    if ($(element).val().trim() == '0') {
                        elementValue = '';
                    }
                }
                else if ($(element).hasClass('checkbox')) {

                    var CurrentObj = $(element).children().attr('id');

                    var radiobtn = $(td).find("input[id*='CHK_FIELD']:checked").toArray();

                    var a = 0;
                    elementValue = '';

                    for (a = 0; a < radiobtn.length; ++a) {

                        if (elementValue == '') {
                            elementValue = $($('#' + $(radiobtn[a]).attr('id')).siblings().first()).text();
                        }
                        else {
                            elementValue = elementValue + ',' + $($('#' + $(radiobtn[a]).attr('id')).siblings().first()).text();
                        }

                    }
                }

                if (hdnValue == elementValue) {
                    return false;
                }
                else {
                    //check if normal update or query resolving update
                    if ($(element).hasClass('bkcolor-red') || $(element).hasClass('bkcolor-orange')) {
                        query = "1"
                    }
                    //  var tableName = $(element).parent('td').parent('tr').parent('tbody').parent('table').attr('id');
                    var moduleName = $('#MainContent_lblModuleName').html(); //$(element).closest('table').attr('title'); //module name of form
                    var tableName = $(element).closest('tr').find('td:eq(5)').find('span').html(); //$(element).closest('table').attr('Name'); //table name of database
                    var variableName = $(element).closest('tr').find('td:eq(1)').find('span').html();
                    var contId = $(element).closest('tr').find('td:first').find('input').val(); //unique id for each row
                    var fieldName = $(element).closest('tr').find('td:eq(3)').find('span').html(); //column name of particular control

                    if ($(element).is("span")) {
                        CurrentObjType = $(element).children().attr('type');
                    }
                    else {
                        CurrentObjType = $(element).attr('type');
                        CurrentObj = $(element).attr('id');
                    }

                    if ($(element).is("input")) {
                        controlType = "textbox"
                    }
                    if ($(element).is("select")) {
                        controlType = "drp"
                    }

                    $.ajax({
                        type: "POST",
                        url: "AjaxFunction.aspx/DM_UpdateData",
                        data: '{ContId: "0", ModuleName: "' + moduleName + '" , FieldName: "' + fieldName + '",TableName: "' + tableName + '",VariableName: "' + variableName + '",OldValue: "' + hdnValue + '",NewValue: "' + elementValue + '",Reason: "Data Entry Error",Comments: "",Query: "' + query + '",ControlType: "' + controlType + '",DM_PVID: "' + DM_PVID + '",DM_RECID: "' + DM_RECID + '",DM_SUBJID: "' + DM_SUBJID + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            //                        alert("Record Updated Successfully");
                        },
                        failure: function (response) {
                            if (response.d == 'Object reference not set to an instance of an object.') {
                                alert(response.d);
                            }
                            else {
                                alert("Contact administrator not suceesfully updated")
                            }
                        }
                    });
                }
                return false;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <div runat="server" id="divHideShow" class="disp-none">
            <br />
            <input id="hideshow" type="button" class="btn btn-primary btn-sm" value="SHOW QUERY"
                style="font-size: 9px; margin-top: 1px;" />
        </div>
    </div>
    <div class="box box-warning" onkeydown="tab(event);">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%;">
                DATA ENTRY &nbsp;||&nbsp;
                <asp:Label runat="server" ID="lblSiteId" />&nbsp;||&nbsp;<asp:Label runat="server"
                    ID="lblSubjectId" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblVisit" />&nbsp;||&nbsp;<asp:Label
                        runat="server" ID="lblIndication" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblPVID" />
                &nbsp;<asp:Label runat="server" Visible="false" ID="lblStatus" />&nbsp;
            </h3>
        </div>
        <br />
        <div class="box-body">
            <asp:GridView ID="grdAUDITTRAILDETAILS" runat="server" AutoGenerateColumns="False"
                CellPadding="4" font-family="Arial" Font-Size="X-Small" ForeColor="#333333" GridLines="None"
                Style="text-align: center" Width="228px" CssClass="table table-bordered table-striped disp-none">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="TABLE NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="TABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("TABLENAME") %>' Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VARIABLE NAME">
                        <ItemTemplate>
                            <asp:TextBox ID="VARIABLENAME" runat="server" font-family="Arial" Font-Size="X-Small"
                                Text='<%# Bind("VARIABLENAME") %>' Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CONTID">
                        <ItemTemplate>
                            <asp:TextBox ID="CONTID" runat="server" font-family="Arial" Font-Size="X-Small" Text='<%# Bind("CONTID") %>'
                                Width="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div id="popup_AuditTrail_ePRO" title="Audit Trail" class="disp-none">
                <div id="grdAuditTrail_ePRO">
                </div>
            </div>
            <asp:HiddenField ID="hdn_Value" runat="server" Value="" />
            <div id="popup_UpdateData_ePRO" title="Reason For Change" class="disp-none">
                <div class="disp-none">
                    <asp:Label ID="Label7" runat="server" Text="Table Name"></asp:Label>
                    <asp:TextBox ID="txt_TableName" runat="server"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="Cont Id"></asp:Label>
                    <asp:TextBox ID="txt_ContId" runat="server"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text="Variable Name"></asp:Label>
                    <asp:TextBox ID="txt_VariableName" runat="server"></asp:TextBox>
                </div>
                <div class="formControl">
                    <asp:Label ID="Label4" runat="server" CssClass="wrapperLable" Text="Module Name"></asp:Label>
                    <asp:TextBox ID="txt_ModuleName" CssClass="width245px" Enabled="false" runat="server"></asp:TextBox>
                </div>
                <div class="formControl">
                    <asp:Label ID="Label8" runat="server" CssClass="wrapperLable" Text="Field Name"></asp:Label>
                    <asp:TextBox ID="txt_FieldName" CssClass="width245px" Enabled="false" runat="server"></asp:TextBox>
                </div>
                <div class="formControl">
                    <asp:Label ID="Label1" runat="server" CssClass="wrapperLable" Text="Old value"></asp:Label>
                    <asp:TextBox ID="txt_OldValue" CssClass="width245px" Enabled="false" runat="server"></asp:TextBox>
                </div>
                <div class="formControl">
                    <asp:Label ID="Label2" runat="server" CssClass="wrapperLable" Text="New value"></asp:Label>
                    <asp:TextBox ID="txt_NewValue" CssClass="width245px" Enabled="false" runat="server"></asp:TextBox>
                </div>
                <div class="formControl">
                    <asp:Label ID="Label3" runat="server" CssClass="wrapperLable" Text="Reason"></asp:Label>
                    <asp:DropDownList ID="drp_Reason" CssClass="width245px" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="Data entry error">Data entry error</asp:ListItem>
                        <asp:ListItem Value="Updated data available">Updated data available</asp:ListItem>
                        <asp:ListItem Value="Other">Other</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="formControl">
                    <asp:Label ID="Label9" runat="server" CssClass="wrapperLable" Text="Comments"></asp:Label>
                    <asp:TextBox ID="txt_Comments" CssClass="width245px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </div>
                <div style="margin-top: 10px">
                    <asp:Button ID="btn_Update" runat="server" Style="margin-left: 107px;" OnClientClick="ePRO_UpdateData()"
                        Text="Update Data" />
                    <asp:Button ID="btn_Cancel" runat="server" Style="margin-left: 62px" Text="Cancel"
                        OnClientClick="ePRO_CancelUpdate()" />
                </div>
            </div>
            <div class="form-group">
                <asp:HiddenField runat="server" ID="hfTablename" />
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;
                    font-size: 15px; margin-left: 7px"></asp:Label>
                <asp:HiddenField runat="server" ID="hfModuleLimit" />
                <asp:HiddenField runat="server" ID="hfModuleData" />
                <asp:HiddenField runat="server" ID="hfDM_PVID" />
                <asp:HiddenField runat="server" ID="hfDM_PAGESTATUS" />
                <asp:HiddenField runat="server" ID="hfDM_DATAFREEZELOCKSTATUS" />
                <asp:HiddenField runat="server" ID="hfDM_RECID" />
                <asp:HiddenField runat="server" ID="hfDM_VISITNUM" />
                <asp:HiddenField runat="server" ID="hfDM_PAGENUM" />
                <asp:HiddenField runat="server" ID="hfIsClicked" />
                <table class="style1">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblRemaining" runat="server" Style="color: #CC3300; font-weight: 700;
                                font-size: 15px;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblModuleName" runat="server" Text="" Font-Size="12px" Font-Bold="true"
                                Font-Names="Arial" CssClass="list-group-item"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:GridView ID="grd_Data" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped ShowChild1" ShowHeader="false" CellSpacing="2"
                                OnRowDataBound="DSMH_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" onmousedown="myFocus(this)"
                                                onchange="myFunction(this)" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="disp-none"></HeaderStyle>
                                        <ItemStyle CssClass="disp-none"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVARIABLENAME" CssClass='<%# Bind("VARIABLENAME") %>' Text='<%# Bind("VARIABLENAME") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTABLENAME" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DATATYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDATATYPE" Text='<%# Bind("DATATYPE") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                        <ItemTemplate>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" runat="server" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);"
                                                    onmousedown="myFocus(this)" Visible="false" Width="200px">
                                                </asp:DropDownList>
                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                <asp:TextBox ID="TXT_FIELD" runat="server" CssClass="rightClick" autocomplete="off"
                                                    Width="200px" Visible="false" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this); getFirstDate();"
                                                    onmousedown="myFocus(this)"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                    <ItemTemplate>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);"
                                                                onmousedown="myFocus(this)" CssClass="checkbox rightClick" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                Text='<%# Bind("TEXT") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                    <ItemTemplate>
                                                        <div class="col-md-4">
                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);" onmousedown="myFocus(this)"
                                                                onclick="return RadioCheck(this);" CssClass="radio rightClick" Text='<%# Bind("TEXT") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:HiddenField ID="hdnGrid" runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" PageStatus" ItemStyle-Width="2%"
                                        ItemStyle-CssClass="txt_center PageStatus">
                                        <ItemTemplate>
                                            <img src="Images/manualquery1.jpg" id="GMQ" onclick="GenerateManualQuery(this)" title="Raise Manual Query"
                                                height="14" width="16" visible="false" class="disp-none  raiseManualQuery" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <img src="Images/manualquery7.jpg" id="AQ" onclick="ShowAutoQuery(this)" title="Auto Query"
                                                height="14" width="16" class="disp-none  ExistAutoQuery" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <img src="Images/manualquery2.png" id="MQ" onclick="ShowManualQuery(this)" title="Manual Query"
                                                height="16" width="19" class="disp-none  ExistManualQuery" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center "
                                        Visible="false">
                                        <ItemTemplate>
                                            <img src="Images/index.png" id="CM" height="14" width="16" runat="server" class="disp-none Comments"
                                                title="Comments" onclick="ShowComments(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                        <ItemTemplate>
                                            <img src="Images/Audit_Trail.png" id="AD" height="15" width="18" runat="server" title="Audit trail"
                                                visible="false" onclick="showAuditTrail(this)" class="disp-none" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div id="divIcon" runat="server" class="icon">
                                                <asp:Label ID="lblicon" runat="server"><i id="ICONCLASS" runat="server" visible="false"
                                                    class="fa fa-book"></i></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                    <div style="float: right; font-size: 13px;">
                                                    </div>
                                                    <div>
                                                        <div id="_CHILD" style="display: block; position: relative;">
                                                            <asp:GridView ID="grd_Data1" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                CssClass="table table-bordered table-striped table-striped1 ShowChild2" ShowHeader="false"
                                                                CellSpacing="2" OnRowDataBound="DSMH1_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" onmousedown="myFocus(this)"
                                                                                onchange="myFunction(this)" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="disp-none"></HeaderStyle>
                                                                        <ItemStyle CssClass="disp-none"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVARIABLENAME" CssClass='<%# Bind("VARIABLENAME") %>' Text='<%# Bind("VARIABLENAME") %>'
                                                                                runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTABLENAME" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="DATATYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDATATYPE" Text='<%# Bind("DATATYPE") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                        <ItemTemplate>
                                                                            <div class="col-md-12">
                                                                                <asp:DropDownList ID="DRP_FIELD" runat="server" CssClass="rightClick" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);"
                                                                                    onmousedown="myFocus(this)" Visible="false" Width="200px">
                                                                                </asp:DropDownList>
                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                <asp:TextBox ID="TXT_FIELD" runat="server" CssClass="rightClick" autocomplete="off"
                                                                                    Width="200px" Visible="false" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this); getFirstDate();"
                                                                                    onmousedown="myFocus(this)"></asp:TextBox>
                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-4">
                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);"
                                                                                                onmousedown="myFocus(this)" CssClass="checkbox rightClick" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                    <ItemTemplate>
                                                                                        <div class="col-md-4">
                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);" onmousedown="myFocus(this)"
                                                                                                onclick="return RadioCheck(this);" CssClass="radio rightClick" Text='<%# Bind("TEXT") %>' />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:HiddenField ID="hdnGrid1" runat="server" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" PageStatus" ItemStyle-Width="2%"
                                                                        ItemStyle-CssClass="txt_center PageStatus">
                                                                        <ItemTemplate>
                                                                            <img src="Images/manualquery1.jpg" id="GMQ" onclick="GenerateManualQuery(this)" title="Raise Manual Query"
                                                                                height="14" width="16" visible="false" class="disp-none  raiseManualQuery" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <img src="Images/manualquery7.jpg" id="AQ" onclick="ShowAutoQuery(this)" title="Auto Query"
                                                                                height="14" width="16" class="disp-none  ExistAutoQuery" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <img src="Images/manualquery2.png" id="MQ" onclick="ShowManualQuery(this)" title="Manual Query"
                                                                                height="16" width="19" class="disp-none  ExistManualQuery" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <img src="Images/index.png" id="CM" height="14" width="16" visible="false" runat="server"
                                                                                class="disp-none Comments" title="Comments" onclick="ShowComments(this)" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                        <ItemTemplate>
                                                                            <img src="Images/Audit_Trail.png" id="AD" height="15" width="18" runat="server" visible="false"
                                                                                title="Audit trail" onclick="showAuditTrail(this)" class="disp-none" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass=" txt_center ">
                                                                        <ItemTemplate>
                                                                            <%--<input type="checkbox" id="C" class="sdvCheckbox disp-none" runat="server">--%>
                                                                            <asp:HiddenField runat="server" ID="hfSDV" Value="false" />
                                                                            <asp:CheckBox ID="C" CssClass="sdvCheckbox disp-none" runat="server" OnClick="chkVerifyHF(this);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <div id="divIcon" runat="server" class="icon" visible="false">
                                                                                <asp:Label ID="lblicon" runat="server"><i id="ICONCLASS" runat="server" visible="false"
                                                                                    class="fa fa-book"></i></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                    <div style="float: right; font-size: 13px;">
                                                                                    </div>
                                                                                    <div>
                                                                                        <div id="_CHILD" style="display: block; position: relative;">
                                                                                            <asp:GridView ID="grd_Data2" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                                CssClass="table table-bordered table-striped table-striped2 ShowChild3" ShowHeader="false"
                                                                                                CellSpacing="2" OnRowDataBound="DSMH2_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" onmousedown="myFocus(this)"
                                                                                                                onchange="myFunction(this)" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
                                                                                                        </ItemTemplate>
                                                                                                        <HeaderStyle CssClass="disp-none"></HeaderStyle>
                                                                                                        <ItemStyle CssClass="disp-none"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblVARIABLENAME" CssClass='<%# Bind("VARIABLENAME") %>' Text='<%# Bind("VARIABLENAME") %>'
                                                                                                                runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblTABLENAME" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="DATATYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDATATYPE" Text='<%# Bind("DATATYPE") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                                                        ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                                                        <ItemTemplate>
                                                                                                            <div class="col-md-12">
                                                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" runat="server" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);"
                                                                                                                    onmousedown="myFocus(this)" Visible="false" Width="200px">
                                                                                                                </asp:DropDownList>
                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                <asp:TextBox ID="TXT_FIELD" CssClass="rightClick" runat="server" autocomplete="off"
                                                                                                                    Width="200px" Visible="false" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this); getFirstDate();"
                                                                                                                    onmousedown="myFocus(this)"></asp:TextBox>
                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);"
                                                                                                                                onmousedown="myFocus(this)" CssClass="checkbox rightClick" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);" onmousedown="myFocus(this)"
                                                                                                                                onclick="return RadioCheck(this);" CssClass="radio rightClick" Text='<%# Bind("TEXT") %>' />
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:Repeater>
                                                                                                                <asp:HiddenField ID="hdnGrid2" runat="server" />
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" PageStatus" ItemStyle-Width="2%"
                                                                                                        ItemStyle-CssClass="txt_center PageStatus">
                                                                                                        <ItemTemplate>
                                                                                                            <img src="Images/manualquery1.jpg" id="GMQ" onclick="GenerateManualQuery(this)" title="Raise Manual Query"
                                                                                                                height="14" width="16" visible="false" class="disp-none  raiseManualQuery" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <img src="Images/manualquery7.jpg" id="AQ" onclick="ShowAutoQuery(this)" title="Auto Query"
                                                                                                                height="14" width="16" class="disp-none  ExistAutoQuery" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <img src="Images/manualquery2.png" id="MQ" onclick="ShowManualQuery(this)" title="Manual Query"
                                                                                                                height="16" width="19" class="disp-none  ExistManualQuery" runat="server" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <img src="Images/index.png" id="CM" height="14" width="16" visible="false" runat="server"
                                                                                                                class="disp-none Comments" title="Comments" onclick="ShowComments(this)" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <img src="Images/Audit_Trail.png" id="AD" height="15" width="18" runat="server" visible="false"
                                                                                                                title="Audit trail" onclick="showAuditTrail(this)" class="disp-none" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass=" txt_center ">
                                                                                                        <ItemTemplate>
                                                                                                            <%--<input type="checkbox" id="C" class="sdvCheckbox disp-none" runat="server">--%>
                                                                                                            <asp:HiddenField runat="server" ID="hfSDV" Value="false" />
                                                                                                            <asp:CheckBox ID="C" CssClass="sdvCheckbox disp-none" runat="server" OnClick="chkVerifyHF(this);" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <div id="divIcon" runat="server" class="icon" visible="false">
                                                                                                                <asp:Label ID="lblicon" runat="server"><i id="ICONCLASS" runat="server" visible="false"
                                                                                                                    class="fa fa-book"></i></asp:Label>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                                                    <div style="float: right; font-size: 13px;">
                                                                                                                    </div>
                                                                                                                    <div>
                                                                                                                        <div id="_CHILD" style="display: block; position: relative;">
                                                                                                                            <asp:GridView ID="grd_Data3" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                                                                CssClass="table table-bordered table-striped table-striped3 ShowChild4" ShowHeader="false"
                                                                                                                                CellSpacing="2" OnRowDataBound="DSMH3_RowDataBound">
                                                                                                                                <Columns>
                                                                                                                                    <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" onmousedown="myFocus(this)"
                                                                                                                                                onchange="myFunction(this)" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
                                                                                                                                        </ItemTemplate>
                                                                                                                                        <HeaderStyle CssClass="disp-none"></HeaderStyle>
                                                                                                                                        <ItemStyle CssClass="disp-none"></ItemStyle>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblVARIABLENAME" CssClass='<%# Bind("VARIABLENAME") %>' Text='<%# Bind("VARIABLENAME") %>'
                                                                                                                                                runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblTABLENAME" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="DATATYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblDATATYPE" Text='<%# Bind("DATATYPE") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                                                                                        ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <asp:DropDownList ID="DRP_FIELD" CssClass="rightClick" runat="server" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);"
                                                                                                                                                    onmousedown="myFocus(this)" Visible="false" Width="200px">
                                                                                                                                                </asp:DropDownList>
                                                                                                                                                <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                                                    OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                                                <asp:TextBox ID="TXT_FIELD" CssClass="rightClick" runat="server" autocomplete="off"
                                                                                                                                                    Width="200px" Visible="false" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);"
                                                                                                                                                    onmousedown="myFocus(this)"></asp:TextBox>
                                                                                                                                                <asp:HiddenField runat="server" ID="HDN_FIELD" />
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div class="col-md-4">
                                                                                                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);"
                                                                                                                                                                onmousedown="myFocus(this)" CssClass="checkbox rightClick" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                Text='<%# Bind("TEXT") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div class="col-md-4">
                                                                                                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                onchange="myFunction(this); AUDIT_myFunction(this); showChild(this);" onmousedown="myFocus(this)"
                                                                                                                                                                onclick="return RadioCheck(this);" CssClass="radio rightClick" Text='<%# Bind("TEXT") %>' />
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                                <asp:HiddenField ID="hdnGrid3" runat="server" />
                                                                                                                                            </div>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" PageStatus" ItemStyle-Width="2%"
                                                                                                                                        ItemStyle-CssClass="txt_center PageStatus">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <img src="Images/manualquery1.jpg" id="GMQ" onclick="GenerateManualQuery(this)" title="Raise Manual Query"
                                                                                                                                                height="14" width="16" visible="false" class="disp-none  raiseManualQuery" runat="server" />
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <img src="Images/manualquery7.jpg" id="AQ" onclick="ShowAutoQuery(this)" title="Auto Query"
                                                                                                                                                height="14" width="16" class="disp-none  ExistAutoQuery" runat="server" />
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <img src="Images/manualquery2.png" id="MQ" onclick="ShowManualQuery(this)" title="Manual Query"
                                                                                                                                                height="16" width="19" class="disp-none  ExistManualQuery" runat="server" />
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass=" " ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <img src="Images/index.png" id="CM" height="14" width="16" runat="server" class="disp-none Comments"
                                                                                                                                                title="Comments" onclick="ShowComments(this)" />
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass="txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <img src="Images/Audit_Trail.png" id="AD" height="15" width="18" runat="server" visible="false"
                                                                                                                                                title="Audit trail" onclick="showAuditTrail(this)" class="disp-none" />
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="" ItemStyle-Width="2%" ItemStyle-CssClass=" txt_center ">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <%--<input type="checkbox" id="C" class="sdvCheckbox disp-none" runat="server">--%>
                                                                                                                                            <asp:HiddenField runat="server" ID="hfSDV" Value="false" />
                                                                                                                                            <asp:CheckBox ID="C" CssClass="sdvCheckbox disp-none" runat="server" OnClick="chkVerifyHF(this);" />
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblMultiple" Text='<%# Bind("Multiple") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <div id="divIcon" runat="server" class="icon" visible="false">
                                                                                                                                                <asp:Label ID="lblicon" runat="server"><i id="ICONCLASS" runat="server" visible="false"
                                                                                                                                                    class="fa fa-book"></i></asp:Label>
                                                                                                                                            </div>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>
                                                                                                                                </Columns>
                                                                                                                            </asp:GridView>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hdnSourceValue" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: RIGHT">
                            <asp:Button ID="bntSaveComplete" runat="server" Text="Push to eCRF" OnClick="bntSaveComplete_Click"
                                CssClass="btn btn-DarkGreen btn-sm cls-btnSave" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
