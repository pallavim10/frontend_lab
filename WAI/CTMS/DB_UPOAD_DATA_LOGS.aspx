<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_UPOAD_DATA_LOGS.aspx.cs" Inherits="CTMS.DB_UPOAD_DATA_LOGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" runat="server" id="content">
        <div class="box-header">
            <h3 class="box-title">Upload Data Logs
            </h3>
        </div>
        <div>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <label>
                        Select Activity:</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList Style="width: 250px;" ID="ddlActivity" runat="server" class="form-control drpControl required1" TabIndex="1">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="Upload Modules/Fields" Text="Upload Module and Fields"></asp:ListItem>
                        <asp:ListItem Value="Upload MedDRA Libraries" Text="Upload MedDRA Libraries"></asp:ListItem>
                        <asp:ListItem Value="Upload WHODD Libraries" Text="Upload WHODD Libraries"></asp:ListItem>
                        <asp:ListItem Value="Upload Master Library" Text="Upload Master Library"></asp:ListItem>
                        <asp:ListItem Value="Upload Rule" Text="Upload Rules"></asp:ListItem>
                        <asp:ListItem Value="Upload Rule Variable" Text="Upload Rule Variable"></asp:ListItem>
                        <asp:ListItem Value="Upload Lab Reference Range" Text="Upload Lab Reference Range"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnShow_Click" TabIndex="2" />
                    <asp:LinkButton ID="btnExportExcel" runat="server" Text="Export Reviews Logs" OnClick="btnExportExcel_Click" CssClass="btn btn-info btn-sm" ForeColor="White" Font-Bold="true" TabIndex="3"> Export To Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box box-primary" runat="server" id="divRecord">
        <div class="box-header with-border">
            <h4 class="box-title" align="left">Records
            </h4>
        </div>
        <div class="box-body">
            <div class="rows">
                <div class="fixTableHead">
                    <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped Datatable"
                        OnPreRender="grdField_PreRender" EmptyDataText="No Record Found." OnRowCommand="grdRecords_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDownloadSupportDoc" runat="server" CommandArgument='<%# Bind("ID") %>' CssClass="btn"
                                        CommandName="DownloadSupportDoc" ToolTip="Download"><i class="fa fa-download"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FILENAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Navigation Menu Name">
                                <ItemTemplate>
                                    <asp:Label ID="NEV_MENU_NAME" runat="server" Text='<%# Bind("NEV_MENU_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Uploaded By Details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Uploaded By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <div>
                                            <asp:Label ID="lblUploaded" runat="server" Text='<%# Bind("UPLOADBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblUPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblUPLOAD_CAL_TZDAT" runat="server" Text='<%# Bind("UPLOAD_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
