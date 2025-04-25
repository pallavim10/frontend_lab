<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SPONSOR_SAE_DETAILS.aspx.cs" Inherits="CTMS.SPONSOR_SAE_DETAILS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .fontred {
            color: Red;
        }

        .fontBlue {
            color: Blue;
            cursor: pointer;
        }

        .circleQueryCountRed {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Red;
        }

        .circleQueryCountGreen {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Green;
        }

        .YellowIcon {
            color: Yellow;
        }

        .GreenIcon {
            color: Green;
        }

        .strikeThrough {
            text-decoration: line-through;
        }

        .txtleft {
            text-align: left;
        }
    </style>
    <script type="text/javascript">

        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null || value == "Select") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        function pageLoad() {

            $(".Datatable").dataTable({
                "ordering": true,
                "bDestroy": false,
                stateSave: false
            });

        }

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

        //For ajax function call on update data button click
        function showAuditTrail_ALL(element) {
            var dataFreezeLockStatus = 1;
            if (dataFreezeLockStatus == '1') {

                var SAEID = $(element).closest('tr').find('td:eq(5)').find('span').html();
                var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();
                var MODULEID = $(element).closest('tr').find('td:eq(8)').find('span').html();
                var SUBJID = $("#MainContent_drpSubID option:selected").val();

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/SAE_GetAuditTrail_ALL",
                    data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID: "' + MODULEID + '",SUBJID: "' + SUBJID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            $('#SAE_grdAuditTrail').html(data.d)
                            $("#SAE_popup_AuditTrail").dialog({
                                title: "Audit Trail",
                                width: 650,
                                height: 400,
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

        //show auto query onclick of auto query icon
        function ShowManualQuery_ALL(element) {

            var dataFreezeLockStatus = 1;
            var SAEID = $(element).closest('tr').find('td:eq(5)').find('span').html();
            var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();
            var MODULEID = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();

            if (dataFreezeLockStatus == '1') {

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/SAE_GetFieldQuery_ALL",
                    data: '{SAEID: "' + SAEID + '",RECID: "' + RECID + '",MODULEID: "' + MODULEID + '",SUBJID: "' + SUBJID + '"}',
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


        function MarkAsSDV(element) {

            var TABLENAME = $(element).closest('tr').find('td:eq(7)').find('span').html();
            var SAEID = $(element).closest('tr').find('td:eq(5)').find('span').html();
            var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();
            var CONTID = 0;
            var MODULEID = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SAE_PATIENT_SDV",
                data: '{TABLENAME:"' + TABLENAME + '",  SAEID: "' + SAEID + '", RECID: "' + RECID + '" ,MODULEID: "' + MODULEID + '",SUBJID: "' + SUBJID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {

                    var SDVSTATUS = res.d;

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDV']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDVDONE']").attr('id')).addClass("disp-none");

                    if (SDVSTATUS == 'True') {

                        $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDVDONE']").attr('id')).removeClass("disp-none");

                    }
                    else if (SDVSTATUS == 'False') {

                        $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDV']").attr('id')).removeClass("disp-none");

                    }
                    else {

                        alert("This data can not be SDV.");

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

            return false;
        }

        function UnMarkAsSDV(element) {

            var TABLENAME = $(element).closest('tr').find('td:eq(7)').find('span').html();
            var SAEID = $(element).closest('tr').find('td:eq(5)').find('span').html();
            var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();
            var CONTID = 0;
            var MODULEID = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SAE_PATIENT_SDV_UNDOE",
                data: '{TABLENAME:"' + TABLENAME + '",  SAEID: "' + SAEID + '", RECID: "' + RECID + '" ,MODULEID: "' + MODULEID + '",SUBJID: "' + SUBJID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {

                    var SDVSTATUS = res.d;

                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDV']").attr('id')).addClass("disp-none");
                    $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDVDONE']").attr('id')).addClass("disp-none");

                    if (SDVSTATUS == 'True') {

                        $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDVDONE']").attr('id')).removeClass("disp-none");

                    }
                    else if (SDVSTATUS == 'False') {

                        $('#' + $(element).closest('tr').find('td:eq(0)').find("a[id*='lbtnSDV']").attr('id')).removeClass("disp-none");

                    }
                    else {

                        alert("This data can not be SDV.");

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

            return false;
        }

        function ViewData(element) {

            var TABLENAME = $(element).closest('tr').find('td:eq(7)').find('span').html();
            var PVID = $(element).closest('tr').find('td:eq(5)').find('span').html();
            var RECID = $(element).closest('tr').find('td:eq(6)').find('span').html();
            var CONTID = 0;
            var VISITNUM = $("#MainContent_drpVisit option:selected").val();
            var VISIT = $("#MainContent_drpVisit option:selected").text();
            var INDICATION = $("#MainContent_drpIndication option:selected").val();
            var MODULEID = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var MODULENAME = $(element).closest('tr').find('td:eq(9)').find('span').html();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();
            var INVID = $("#MainContent_drpInvID option:selected").val();
            var MULTIPLEYN = $(element).closest('tr').find('td:eq(10)').find('span').html();

            var test;
            if (MULTIPLEYN == "True") {
                test = "DM_DataSDV.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITNUM + "&Indication=" + INDICATION + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID + "&MULTIPLEPAGE=2";
            }
            else {
                test = "DM_DataSDV.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VISITID=" + VISITNUM + "&Indication=" + INDICATION + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID + "&RECID=" + RECID + "&MULTIPLEPAGE=0";
            }
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=450,resizable=no";
            window.open(test);
            return false;
        }

        function ViewSourceData(element) {

            var MODULEID = $(element).next().text();
            var VISITNUM = $("#MainContent_drpVisit option:selected").val();
            var VISIT = $("#MainContent_drpVisit option:selected").text();
            var INDICATION = $("#MainContent_drpIndication option:selected").val();
            var SUBJID = $("#MainContent_drpSubID option:selected").val();
            var INVID = $("#MainContent_drpInvID option:selected").val();

            test = "DM_Source_Data.aspx?MODULEID=" + MODULEID + "&VISITID=" + VISITNUM + "&Indication=" + INDICATION + "&VISIT=" + VISIT + "&INVID=" + INVID + "&SUBJID=" + SUBJID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">SAE Details
            </h3>
        </div>
        <div id="SAE_popup_AuditTrail" title="Audit Trail" class="disp-none">
            <div id="SAE_grdAuditTrail">
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label">
                            Site ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control "
                                OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label">
                        Subject ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required" AutoPostBack="true"
                            OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label">
                        SAE ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSAEID" runat="server" CssClass="form-control required" AutoPostBack="true"
                            OnSelectedIndexChanged="drpSAEID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex" runat="server" id="divStatus">
                    <label class="label">
                        Status:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                    OnClick="btngetdata_Click" />
                &nbsp&nbsp&nbsp <a href="JavaScript:ManipulateAll('_Pat_');" id="_Folder" style="color: #333333; font-size: x-large; margin-top: 5px;"><i id="img_Pat_" class="icon-plus-sign-alt"></i></a>
            </div>
        </div>
    </div>
    <asp:Repeater runat="server" OnItemDataBound="repeatData_ItemDataBound" ID="repeatData">
        <ItemTemplate>
            <div class="box box-primary">
                <div class="box-header" style="display: inline-flex;">
                    <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                        id="anchor">
                        <a href="JavaScript:divexpandcollapse('_Pat_<%# Eval("MODULEID") %>');" style="color: #333333">
                            <i id="img_Pat_<%# Eval("MODULEID") %>" class="icon-plus-sign-alt"></i></a>
                    </div>
                    <h3 class="box-title" style="padding-top: 0px;">
                        <asp:Label ID="lblHeader" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                    </h3>
                    <div class="pull-left" id="divSignoff" runat="server" style="color: Blue;">
                        &nbsp&nbsp||
                        <label class="label">
                            Investigator Sign Off :
                        </label>
                        <asp:Label ID="lblINVSIGNOFF" runat="server"></asp:Label>
                    </div>
                    <div class="pull-left" id="divMR" runat="server" style="color: Blue;">
                        &nbsp&nbsp||
                        <label class="label">
                            Medical Review :
                        </label>
                        <asp:Label ID="lblMR" runat="server"></asp:Label>
                    </div>
                </div>
                <div id="_Pat_<%# Eval("MODULEID") %>" style="display: none; position: relative; overflow: auto;">
                    <div class="box-body">
                        <div class="rows">
                            <div style="width: 100%; overflow: auto;">
                                <div>
                                    <asp:GridView ID="grd_Records" runat="server" CellPadding="3" AutoGenerateColumns="True"
                                        CssClass="table table-bordered table-striped Datatable" Width="100%" ShowHeader="True"
                                        EmptyDataText="No Records Available" CellSpacing="2" OnRowDataBound="grd_Records_RowDataBound"
                                        OnPreRender="grd_data_PreRender">
                                        <RowStyle HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SAEID" HeaderStyle-CssClass="width100px align-center"
                                                ItemStyle-CssClass="width100px align-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="SAEIDS" Font-Size="Small" Text='<%# Bind("SAEID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-center disp-none" ItemStyle-CssClass="align-center  disp-none"
                                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPAGENUM" runat="server" ToolTip="View" OnClientClick="return ViewData(this)"><i class="fa fa-search"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QUERY" HeaderStyle-CssClass="width100px align-center"
                                                ItemStyle-CssClass="width100px align-center">
                                                <ItemTemplate>
                                                    <img src="Images/manualquery2.png" alt="" id="lnkMANUALQUERY" title="Manual Query"
                                                        height="16" width="19" runat="server" onclick="ShowManualQuery_ALL(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AUDIT TRAIL" HeaderStyle-CssClass="width100px align-center"
                                                ItemStyle-CssClass="width100px align-center">
                                                <ItemTemplate>
                                                    <img src="Images/Audit_Trail.png" id="lnkAUDITTRAIL" title="Audit trail" height="15"
                                                        onclick="showAuditTrail_ALL(this)" width="18" runat="server" />
                                                    <img src="Images/Audit_Trail1.png" id="ADINITIAL" height="15" width="18" runat="server"
                                                        title="Audit trail" onclick="showAuditTrail_ALL(this)" class="disp-none" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Notes" HeaderStyle-CssClass=" disp-none" ItemStyle-Width="2%"
                                                ItemStyle-CssClass="txt_center disp-none">
                                                <ItemTemplate>
                                                    <img src="~/img/green file.png" id="NOTES" onclick="ShowContextualNotes(this)" title="Contextual Notes"
                                                        height="16" width="19" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PVID" HeaderStyle-CssClass="width100px align-center disp-none"
                                                ItemStyle-CssClass="width100px align-center disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="SAEID" Font-Size="Small" Text='<%# Bind("SAEID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RECID" HeaderStyle-CssClass="width100px align-center disp-none"
                                                ItemStyle-CssClass="width100px align-center disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="width100px align-center disp-none"
                                                ItemStyle-CssClass="width100px align-center disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="TABLENAME" Font-Size="Small" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MODULEID" HeaderStyle-CssClass="width100px align-center disp-none"
                                                ItemStyle-CssClass="width100px align-center disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="MODULEID" Font-Size="Small" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MODULENAME" HeaderStyle-CssClass="width100px align-center disp-none"
                                                ItemStyle-CssClass="width100px align-center disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="MODULENAME" Font-Size="Small" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MULTIPLEYN" HeaderStyle-CssClass="width100px align-center disp-none"
                                                ItemStyle-CssClass="width100px align-center disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="MULTIPLEYN" Font-Size="Small" Text='<%# Bind("MULTIPLEYN") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
