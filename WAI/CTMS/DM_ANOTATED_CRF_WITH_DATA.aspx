<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_ANOTATED_CRF_WITH_DATA.aspx.cs"
    Inherits="CTMS.DM_ANOTATED_CRF_WITH_DATA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />

    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/DataEntry_Table.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/DM/CallChange.js"></script>
    <script src="CommonFunctionsJs/DM/Grid_AuditTrail.js"></script>
    <script src="CommonFunctionsJs/DM/Grid_Comments.js"></script>
    <script src="CommonFunctionsJs/DM/Grid_Queries.js"></script>
    <script src="CommonFunctionsJs/DM/OpenModule.js"></script>
    <script src="CommonFunctionsJs/DM/ShowChild.js"></script>

    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <script src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="CommonFunctionsJs/Datatable1.js" type="javascript"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>

    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();
            BindLinkedData();
            SetLinkedData();
            GET_LAB_DATA();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Subject CRF Data
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300;"></asp:Label>
                <div id="">
                    <asp:HiddenField ID="HDNMODULEID" runat="server" />
                    <asp:HiddenField ID="HDMODULENAME" runat="server" />
                    <asp:HiddenField ID="hdnid" runat="server" />
                    <asp:HiddenField ID="hdnid1" runat="server" />
                    <asp:HiddenField ID="hdnid2" runat="server" />
                    <asp:HiddenField ID="hdnfieldname1" runat="server" />
                    <asp:HiddenField ID="hdnfieldname2" runat="server" />
                    <asp:HiddenField ID="hdnfieldname3" runat="server" />
                    <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                        <div class="form-group" style="display: inline-flex">
                            <label class="label" style="color: Maroon;">
                                Site ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpInvID" ForeColor="Blue" runat="server" OnSelectedIndexChanged="drpInvID_SelectedIndexChanged"
                                    AutoPostBack="True" CssClass="form-control ">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" style="display: inline-flex">
                        <label class="label" style="color: Maroon;">
                            Subject ID:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpSubID" ForeColor="Blue" runat="server" CssClass="form-control required select"
                                AutoPostBack="True" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" style="display: inline-flex">
                        <label class="label" style="color: Maroon;">
                            Visit:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpVisit" ForeColor="Blue" runat="server" AutoPostBack="True"
                                CssClass="form-control select" OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" style="display: inline-flex; margin-bottom: -27px;">
                        <label class="label">
                        </label>
                        <div class="Control">
                            <asp:Button ID="Btn_Get_Data" runat="server" OnClick="Btn_Get_Data_Click" CssClass="btn btn-primary btn-sm cls-btnSave" Style="margin-bottom: -27px;"
                                Text="Get Data" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDownload" runat="server" Text="Download CRF" CssClass="btn btn-sm btn-DARKORANGE" Style="margin-bottom: -27px;"
                                OnClick="btnDownload_Click"></asp:Button>
                        </div>
                    </div>
                    <asp:Repeater runat="server" ID="repeat_AllModule" OnItemDataBound="repeat_AllModule_ItemDataBound">
                        <ItemTemplate>
                            <div style="border-style: double; margin-bottom: 1px;">
                                <table>
                                    <tr>
                                        <td style="font-weight: bold; width: 15%; color: #a70808;">Visit Name :&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblVISIT" runat="server" Text='<%# Bind("VISIT") %>' Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
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
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
