<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_Query_Reports.aspx.cs" Inherits="CTMS.DM_Query_Reports" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="CommonStyles/Searchable_DropDown.css" rel="stylesheet" />
    <script type="text/jscript">
        function pageLoad() {

            $('.select').select2();
            $(".Datatable").dataTable({

                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true,
            });           
            
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        }


        function showQueryComment(element) {

            var ID = $(element).closest('tr').find('td:eq(8)').find('span').html();

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/ShowQueryComment",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#divShowQryComment').html(data.d)
                        $("#Popup_ShowQryComment").dialog({
                            title: "Comment",
                            width: 900,
                            height: 500,
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
            return false;
        }

        function Show_MM_QueryHistory(element) {

            $.ajax({
                type: "POST",
                url: "AjaxFunction_DM.aspx/Show_MM_QueryHistory",
                data: '{ID: "' + element + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#divShow_MM_QryHistory').html(data.d)

                        $("#Popup_Show_MM_QryHistory").dialog({
                            title: "MM Query History",
                            width: 850,
                            height: 450,
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
            return false;
        }

        function Validation() {
            var drpQueryStatus = document.getElementById("<%=drpQueryStatus.ClientID%>");
            var drpSite = document.getElementById("<%=drpSite.ClientID%>");
            if (drpQueryStatus.value == "0") {
                if (drpSite.value == "0") {
                    alert("Please Select Site ID.");
                    document.getElementById("<%=drpSite.ClientID%>").focus();
                    return false;
                }
            }
            else {
                return true;
            }           
        }

       
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="Updatepanel1">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Query Reports
                    </h3>
                </div>
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: small;"></asp:Label>
                </div>
                <div class="">
                    <br />
                    <div runat="server" id="Div1" class="" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblSiteId" runat="server" CssClass="wrapperLable" Text="Select Site Id:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpSite" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div2" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblPatientId" runat="server" CssClass="wrapperLable" Text="Select Subject Id:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpPatient" runat="server" CssClass="form-control select" AutoPostBack="True" SelectionMode="Single"
                                OnSelectedIndexChanged="drpPatient_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div3" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblVisitId" runat="server" CssClass="wrapperLable" Text="Select Visit:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpVisit" runat="server" CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div5" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblModule" runat="server" CssClass="wrapperLable" Text="Select Module:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpModule" runat="server" CssClass="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="drpModule_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div6" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblField" runat="server" CssClass="wrapperLable" Text="Select Field:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpField" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div7" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="lblQueryStatus" runat="server" CssClass="wrapperLable" Text="Query Status:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpQueryStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpQueryStatus_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div runat="server" id="Div8" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                <asp:Label ID="Label1" runat="server" CssClass="wrapperLable" Text="Query Type:"></asp:Label>
                            </label>
                        </div>
                        <div class="Control">
                            <asp:DropDownList ID="drpQueryType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                <asp:ListItem Text="Automatic" Value="Automatic"></asp:ListItem>
                                <asp:ListItem Text="Manual" Value="Manual"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search"
                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm"
                        OnClick="btnCancel_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" CssClass="btn btn-info btn-sm"
                        Text="Export to Excel" ForeColor="White">Export to Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                     &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnclosed" runat="server" CssClass="btn btn-primary btn-sm" Visible="false" OnClientClick="return Validation();"
                    Text="Close Bulk Query"  OnClick="btnclosed_Click" />
                     &nbsp;&nbsp;&nbsp;
                </div>
            </div>
            <div class="box-group">
                <div class="form-group">
                    <div id="tabscontainer" runat="server">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab-1" data-toggle="tab">Details Reports</a></li>
                            <li><a href="#tab-2" data-toggle="tab">Cumulative Reports</a></li>
                        </ul>
                        <div class="tab">
                            <div id="tab-1" class="tab-content current">
                                <div class="box">
                                    <div align="left">
                                        <div class="rows">
                                            <div class="fixTableHead">
                                                <asp:GridView ID="grdQueryDetailReports" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered Datatable" OnPreRender="grd_data_PreRender" 
                                                    OnRowCommand="grdQueryDetailReports_RowCommand" 
                                                    OnRowDataBound="grdQueryDetailReports_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSelect" runat="server" Text="Answer" CommandName="Resolve"
                                                                    Visible="false" CommandArgument='<%# Bind("ID") %>' OnClientClick="aspnetForm.target ='_blank';" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Query Comment" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lblComment" ToolTip="Comments" runat="server" Visible="false" OnClientClick="return showQueryComment(this);">
                                                <i class="fa fa-comment" style="font-size:17px;color:darkmagenta;" ></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Site ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Subject ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Record No." ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' CssClass="disp-none" runat="server"></asp:Label>
                                                                <asp:Label ID="Label5" Font-Size="Small" Text='<%# Bind("RECORDNO") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Visit" HeaderStyle-CssClass="align-left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Module">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Field Name" HeaderStyle-CssClass="align-left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="FIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Query ID" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rule description" HeaderStyle-CssClass="align-left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Query Text" HeaderStyle-CssClass="align-left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="QUERYTEXT" runat="server" Text='<%# Bind("QUERYTEXT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Query Status" HeaderStyle-CssClass="align-left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="STATUSTEXT" runat="server" Text='<%# Bind("STATUSTEXT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Query Type" HeaderStyle-CssClass="align-left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="QUERYTYPE" runat="server" Text='<%# Bind("QUERYTYPE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                            <HeaderTemplate>
                                                                <label>Generated Details</label><br />
                                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Generated By]</label><br />
                                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div runat="server" id="divGenerated">
                                                                    <div>
                                                                        <asp:Label ID="QRYGENBYNAME" runat="server" Text='<%# Bind("QRYGENBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Label ID="QRYGEN_CAL_DAT" runat="server" Text='<%# Bind("QRYGEN_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Label ID="QRYGEN_CAL_TZDAT" runat="server" Text='<%# Bind("QRYGEN_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="align-left">
                                                            <HeaderTemplate>
                                                                <label>Closed Details</label><br />
                                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Closed By]</label><br />
                                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div runat="server" id="divResolved">
                                                                    <div>
                                                                        <asp:Label ID="QRYRESBYNAME" runat="server" Text='<%# Bind("QRYRESBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Label ID="QRYRES_CAL_DAT" runat="server" Text='<%# Bind("QRYRES_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Label ID="QRYRES_CAL_TZDAT" runat="server" Text='<%# Bind("QRYRES_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MULTIPLEYN" runat="server" Text='<%# Bind("MULTIPLEYN") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="tab-2" class="tab-content">
                                <div class="rows">
                                    <div class="fixTableHead">
                                        <asp:GridView ID="grdCommulativeReports" runat="server" AutoGenerateColumns="true"
                                            CssClass="table table-bordered Datatable" OnPreRender="grd_data_PreRender">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
