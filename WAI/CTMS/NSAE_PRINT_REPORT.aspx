<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NSAE_PRINT_REPORT.aspx.cs" Inherits="CTMS.NSAE_PRINT_REPORT" %>

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

        .brd-1px-redimp {
            border: 1px solid !important;
            border-color: Red !important;
        }

        .disp-none {
            display: none;
        }

        .border3pxsolidblack {
            border: 3px solid black !important;
        }

        .strikeThrough {
            text-decoration: line-through;
        }

        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 850px;
            height: 500px;
        }

        .btn-DarkGreen {
            background-repeat: repeat-x;
            border-color: #7FB27F;
            color: White;
            background-image: linear-gradient(to bottom, #006600 0%, #006600 100%);
        }

            .btn-DarkGreen:hover, .btn-DarkGreen:focus {
                background-color: #006600;
                color: White;
                background-position: 0 -15px;
            }

            .btn-DarkGreen:active, .btn-DarkGreen.active {
                background-color: #006600;
                border-color: #245580;
            }

            .btn-DarkGreen.disabled, .btn-DarkGreen[disabled], fieldset[disabled] .btn-DarkGreen, .btn-DarkGreen.disabled:hover, .btn-DarkGreen[disabled]:hover, fieldset[disabled] .btn-DarkGreen:hover, .btn-DarkGreen.disabled:focus, .btn-DarkGreen[disabled]:focus, fieldset[disabled] .btn-DarkGreen:focus, .btn-DarkGreen.disabled.focus, .btn-DarkGreen[disabled].focus, fieldset[disabled] .btn-DarkGreen.focus, .btn-DarkGreen.disabled:active, .btn-DarkGreen[disabled]:active, fieldset[disabled] .btn-DarkGreen:active, .btn-DarkGreen.disabled.active, .btn-DarkGreen[disabled].active, fieldset[disabled] .btn-DarkGreen.active {
                background-color: #006600;
                background-image: none;
            }

        .btn-DARKORANGE {
            background-repeat: repeat-x;
            border-color: #FFB266;
            color: White;
            background-image: linear-gradient(to bottom, #FF8000 0%, #FF8000 100%);
        }

            .btn-DARKORANGE:hover, .btn-DARKORANGE:focus {
                background-color: #FF8000;
                color: White;
                background-position: 0 -15px;
            }

            .btn-DARKORANGE:active, .btn-DARKORANGE.active {
                background-color: #FF8000;
                border-color: #245580;
            }

            .btn-DARKORANGE.disabled, .btn-DARKORANGE[disabled], fieldset[disabled] .btn-DARKORANGE, .btn-DARKORANGE.disabled:hover, .btn-DARKORANGE[disabled]:hover, fieldset[disabled] .btn-DARKORANGE:hover, .btn-DARKORANGE.disabled:focus, .btn-DARKORANGE[disabled]:focus, fieldset[disabled] .btn-DARKORANGE:focus, .btn-DARKORANGE.disabled.focus, .btn-DARKORANGE[disabled].focus, fieldset[disabled] .btn-DARKORANGE.focus, .btn-DARKORANGE.disabled:active, .btn-DARKORANGE[disabled]:active, fieldset[disabled] .btn-DARKORANGE:active, .btn-DARKORANGE.disabled.active, .btn-DARKORANGE[disabled].active, fieldset[disabled] .btn-DARKORANGE.active {
                background-color: #FF8000;
                background-image: none;
            }

        .parentcss {
            font-weight: bold;
        }

        #parentTable {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
            height: auto;
        }

            #parentTable td {
                border: 0px solid #ddd;
                padding: 8px;
                width: auto;
            }

            #parentTable tr:nth-child(even) {
                background-color: white;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">SAE Print Report
            </h3>
        </div>
        <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        <asp:HiddenField ID="hdnid" runat="server" />
        <asp:HiddenField ID="hdnid1" runat="server" />
        <asp:HiddenField ID="hdnid2" runat="server" />
        <asp:HiddenField ID="hdnfieldname1" runat="server" />
        <asp:HiddenField ID="hdnfieldname2" runat="server" />
        <asp:HiddenField ID="hdnfieldname3" runat="server" />
        <div class="box-body">
            <div class="form-group">
                <div runat="server" id="divSIte" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label width70px">
                            Site ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div runat="server" id="divSubject" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label width70px">
                            Subject ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="ddlSUBJID" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSUBJID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div runat="server" id="divSAEID" style="display: inline-flex">
                    <label class="label">
                        SAE ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpSAEID" runat="server" CssClass="form-control required" AutoPostBack="True" OnSelectedIndexChanged="drpSAEID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div runat="server" id="div1" style="display: inline-flex">
                    <label class="label">
                        Status:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control required">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12" style="margin-bottom: 10px;">
                    <div class="col-md-2" style="width: 64px;">
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btngetdata" Text="Get Data" runat="server" OnClick="btngetdata_Click" CssClass="btn btn-primary btn-sm cls-btnSave" />
                        &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnDownload_Click" ToolTip="Export to PDF" CssClass="btn btn-info btn-sm"
                        Text="Export to Excel" ForeColor="White">Export to PDF&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="box-body" style="margin-left: 1%; margin-right: 1%;">
                <div class="form-group">
                    <asp:Repeater runat="server" ID="repeat_AllModule" OnItemDataBound="repeat_AllModule_ItemDataBound">
                        <ItemTemplate>
                            <div style="border-style: double; margin-bottom: 1px;">
                                <table>
                                    <tr>
                                        <td style="font-weight: bold; width: 15%; color: #a70808;">Module Name :&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("MODULENAME") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; width: 15%; color: #a70808;">Module Status :&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFormStatus" runat="server" Text='<%# Bind("STATUS") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; width: 15%; color: #a70808;">Site Id :&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSITEID" runat="server" Text='<%# Bind("INVID") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #a70808;">Subject Id :&nbsp;
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblSUBJID" runat="server" Text='<%# Bind("SUBJID") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #a70808;">Record Number. :&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="RECID" runat="server" Text='<%# Bind("RECORDNO") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="margin-bottom: 20px;">
                                <asp:GridView ID="grd_Data" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered1"
                                    ShowHeader="false" CaptionAlign="Left" OnRowDataBound="grd_Data_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT"
                                                    ForeColor="Blue" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="col-md-12" style="padding-left: 0px;">
                                                    <asp:Label ID="TXT_FIELD" Text='<%# Bind("DATAS") %>' runat="server"></asp:Label>
                                                    <asp:GridView ID="grd_Data1" BorderStyle="None" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-striped table-bordered" ShowHeader="false" CaptionAlign="Left"
                                                        OnRowDataBound="grd_Data1_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT"
                                                                        ForeColor="Maroon" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <div class="col-md-12" style="padding-left: 0px;">
                                                                        <asp:Label ID="TXT_FIELD1" Text='<%# Bind("DATAS") %>' runat="server"></asp:Label>&nbsp
                                                                <asp:GridView ID="grd_Data2" BorderStyle="None" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-striped table-bordered" ShowHeader="false" CaptionAlign="Left"
                                                                    OnRowDataBound="grd_Data2_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT"
                                                                                    ForeColor="Green" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <div class="col-md-12" style="padding-left: 0px;">
                                                                                    <asp:Label ID="TXT_FIELD2" Text='<%# Bind("DATAS") %>' runat="server"></asp:Label>
                                                                                    <asp:GridView ID="grd_Data3" BorderStyle="None" runat="server" AutoGenerateColumns="False"
                                                                                        CssClass="table table-striped table-bordered" ShowHeader="false" CaptionAlign="Left"
                                                                                        OnRowDataBound="grd_Data3_RowDataBound">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblFieldName" Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT"
                                                                                                        ForeColor="Red" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    &nbsp
                                                                                                    <asp:Label ID="TXT_FIELD3" Text='<%# Bind("DATAS") %>' runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <table width="100%">
                                <tr id="TRAUDIT" runat="server">
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Audit Trail List" Font-Size="16px" Font-Bold="true"
                                            Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                                        <asp:GridView ID="grdAudit" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                            CaptionAlign="Left">
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="TRQUERY" runat="server">
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Query List" Font-Size="16px" Font-Bold="true"
                                            Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                                        <asp:GridView ID="grdQuery" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                            CaptionAlign="Left">
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="TRQUERY_COMMENT" runat="server">
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Query Comments" Font-Size="16px" Font-Bold="true"
                                            Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                                        <asp:GridView ID="grdQryComment" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                            CaptionAlign="Left">
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="TRFIELDCOMMENT" runat="server">
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Field Comments" Font-Size="16px"
                                            Font-Bold="true" Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                                        <asp:GridView ID="grdFieldComment" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                            CaptionAlign="Left">
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="TRPAGECOMMENT" runat="server">
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Module Comments" Font-Size="16px"
                                            Font-Bold="true" Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                                        <asp:GridView ID="grdModuleComment" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                            CaptionAlign="Left">
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="TRDOCS" runat="server">
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Supporting Document" Font-Size="16px"
                                            Font-Bold="true" Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                                        <asp:GridView ID="grdDocs" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                            CaptionAlign="Left">
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="TREvent_Logs" runat="server">
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Event Logs" Font-Size="16px" Font-Bold="true"
                                            Font-Names="Arial" CssClass="list-group-item" Style="border-color: Black; color: maroon;"></asp:Label>
                                        <asp:GridView ID="grdEventLogs" HeaderStyle-ForeColor="Blue" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered"
                                            CaptionAlign="Left">
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <div style='page-break-after: always;'>&nbsp;</div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

