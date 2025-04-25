<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NCTMS_SHOW_VISIT_REPORT.aspx.cs"
    Inherits="CTMS.NCTMS_SHOW_VISIT_REPORT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .fontred
        {
            color: Red;
        }
        .fontBlue
        {
            color: Blue;
            cursor: pointer;
        }
        .circleQueryCountRed
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Red;
        }
        .circleQueryCountGreen
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Green;
        }
        .YellowIcon
        {
            color: Yellow;
        }
        .GreenIcon
        {
            color: Green;
        }
        .strikeThrough
        {
            text-decoration: line-through;
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
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
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

                var SVID = $(element).closest('tr').find('td:eq(0)').find('span').html();
                var RECID = $(element).closest('tr').find('td:eq(2)').find('span').html();
                var TABLENAME = $(element).closest('tr').find('td:eq(3)').find('span').html();

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/CTMS_GetAuditTrail_ALL",
                    data: '{SVID: "' + SVID + '",RECID: "' + RECID + '",TABLENAME:"' + TABLENAME + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            $('#grdAuditTrail').html(data.d)
                            $("#popup_AuditTrail").dialog({
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

        //For ajax function call on update data button click
        function VIEW_ALL_COMMENT(element) {

            var INVID = $("#<%=ddlSiteId.ClientID%>").find("option:selected").val();
            var PROJECTID = '<%= Session["PROJECTID"] %>';
            var SVID = $("#<%=drpVisitID.ClientID%>").find("option:selected").val();
            var VISITID = $("#<%=drpVisitType.ClientID%>").find("option:selected").val();
            var VISIT = $("#<%=drpVisitType.ClientID%>").find("option:selected").text();
            var Section = $(element).parent().prev().find('h3').find('span').text()
            var SectionID = $(element).next().text();

            var test = "NCTMS_VIEW_REPORT_COMMENT.aspx?Section=" + VISIT + "&ProjectId=" + PROJECTID + "&INVID=" + INVID + "&SVID=" + SVID + "&SectionID=" + VISITID + "&SubSectionID=" + SectionID + "&MODULENAME=" + Section;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1400";
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
            <h3 class="box-title">
                View Report
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div id="Div1" runat="server" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label" style="color: Maroon;">
                            Site Id:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="ddlSiteId" runat="server" ForeColor="Blue" CssClass="form-control required"
                                OnSelectedIndexChanged="ddlSiteId_SelectedIndexChanged" AutoPostBack="True" Style="width: 100%">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Select Visit Type:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpVisitType" runat="server" ForeColor="Blue" AutoPostBack="True"
                            CssClass="form-control required" Style="width: 100%" OnSelectedIndexChanged="drpVisitType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label" style="color: Maroon;">
                        Select Visit Id:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpVisitID" ForeColor="Blue" runat="server" CssClass="form-control required">
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                    OnClick="btngetdata_Click" />
                &nbsp&nbsp&nbsp <a href="JavaScript:ManipulateAll('_Pat_');" id="_Folder" style="color: #333333;
                    font-size: x-large; margin-top: 5px;"><i id="img_Pat_" class="icon-plus-sign-alt">
                    </i></a>
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
                </div>
                <div class="pull-right">
                    <asp:LinkButton ID="lblViewAllComment" CssClass="btn btn-DARKORANGE btn-sm" runat="server"
                        OnClientClick="return VIEW_ALL_COMMENT(this);" ToolTip="View All Comments">
                        <i class="fa fa-comments fa-2x" style="color: White;"></i>
                        <asp:Label class="badge badge-info right" ID="lblAllCommentCount" runat="server"
                            Text="0" Style="margin-left: 5px; background-color: White; color: Black;" Font-Bold="true"></asp:Label>
                    </asp:LinkButton>&nbsp&nbsp&nbsp
                    <asp:Label ID="lblMODULEID" CssClass="disp-none" runat="server" Text='<%# Bind("MODULEID") %>'></asp:Label>&nbsp&nbsp&nbsp&nbsp
                </div>
                <div id="_Pat_<%# Eval("MODULEID") %>" style="display: none; position: relative;
                    overflow: auto;">
                    <div class="box-body">
                        <div class="rows">
                            <div style="width: 100%; overflow: auto;">
                                <div>
                                    <asp:GridView ID="grd_Records" runat="server" CellPadding="3" AutoGenerateColumns="True"
                                        CssClass="table table-bordered table-striped notranslate" ShowHeader="True" EmptyDataText="No Records Available"
                                        CellSpacing="2" OnRowDataBound="grd_Records_RowDataBound" OnPreRender="grd_data_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Visit Id" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-center"
                                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSVID" Font-Size="Small" Text='<%# Bind("SVID") %>' runat="server"></asp:Label>
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
