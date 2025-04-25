<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NSAE_REGULATORY_SUBMITED_REPORT.aspx.cs" Inherits="CTMS.NSAE_REGULATORY_SUBMITED_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/SAE/SAE_ConfirmMsg.js"></script>
    <script>
        function ShowDocs(element) {

            var DOCID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var DocName = $(element).closest('tr').find('td:eq(5)').text().trim();
            var FileType = $(element).closest('tr').find('td:eq(7)').text().trim();
            var SysFileName = $(element).closest('tr').find('td:eq(4)').text().trim();
            var FILEPATH = "~/SAE_Docs/";

            var test = "CTMS_DownloadDoc.aspx?DOCID=" + DOCID + "&DocName=" + DocName + "&FileType=" + FileType + "&SysFileName=" + SysFileName + "&FILEPATH=" + FILEPATH;

            var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test, '_blank');
            return false;
        }

        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
        );
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Publish Regulatory Report</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Select Site Id : &nbsp;
                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control width200px"
                        OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="label col-md-2">
                    Select Subject Id : &nbsp;
                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control width200px required"
                        AutoPostBack="true" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Select SAEID : &nbsp;
                    <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="drpSAEID" runat="server" CssClass="form-control width200px required"
                        AutoPostBack="true" OnSelectedIndexChanged="drpSAEID_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="label col-md-2">
                    Select Status : &nbsp;
                    <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control required width200px">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Select File : &nbsp;
                    <asp:Label ID="Label16" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="X-Small" />
                    <label style="color: red; font-weight: normal;">[Note : Please Choose PDF or Word File Only]</label>
                </div>
                <div class="label col-md-2">
                    Enter Comment : &nbsp;
                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtComment" runat="server" CssClass="form-control required" Width="100%" Height="50px" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div style="margin-left: 20%;">
                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary btn-sm cls-btnSave"
                    OnClick="btnUpload_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                    OnClick="btnCancel_Click" />
            </div>
        </div>
        <br />
    </div>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Published Regulatory Reports</h3>
        </div>
        <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False" Width="98%"
            CssClass="table table-bordered table-striped layerFiles Datatable" OnPreRender="grd_data_PreRender"
            OnRowDataBound="gvFiles_RowDataBound" OnRowCommand="gvFiles_RowCommand">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                    ItemStyle-CssClass="disp-none" HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lblDownloadSupportDoc" runat="server" CommandArgument='<%# Bind("ID") %>' CssClass="btn"
                            CommandName="DownloadSupportDoc" ToolTip="Download"><i class="fa fa-download"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lblDeleteSupportDoc" runat="server" CommandArgument='<%# Bind("ID") %>' OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this file : ", Eval("FILENAME")) %>'
                            CommandName="DeleteSupportDoc"  ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Site Id" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>' Font-Bold="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SUBJECT ID" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>' Font-Bold="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SAEID" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lblSAEID" runat="server" Text='<%# Bind("SAEID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="STATUS" runat="server" Text='<%# Bind("STATUS") %>' Font-Bold="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File Name">
                    <ItemTemplate>
                        <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FILENAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File Type" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="filetype" ToolTip='<%# Bind("CONTENTTYPE") %>' Visible="false" runat="server"
                            Text='<%# Bind("CONTENTTYPE") %>'></asp:Label>
                        <asp:Label ID="lbtnFileType" runat="server" Font-Size="Larger"><i id="ICONCLASS"
                            runat="server" class="fas fa-file-text"></i></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notes">
                    <ItemTemplate>
                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("NOTES") %>'></asp:Label>
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
                <asp:TemplateField HeaderStyle-CssClass="align-left">
                    <HeaderTemplate>
                        <label>Deleted By Details</label><br />
                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Deleted By]</label><br />
                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <div>
                                <asp:Label ID="lblDELETEDBYNAME" runat="server" Text='<%# Bind("DELETEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblDELETED_CAL_DAT" runat="server" Text='<%# Bind("DELETED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblDELETED_CAL_TZDAT" runat="server" Text='<%# Bind("DELETED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
