<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IWRS_MNG_STUDY_DOCS.aspx.cs" Inherits="CTMS.IWRS_MNG_STUDY_DOCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_showAuditTrail.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 300px;
        }

        .fixTableHead {
            overflow-y: auto;
            max-height: 350px;
            min-height: 300px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function pageLoad() {

            $(function () {
                $(".Datatable").parent().parent().addClass('fixTableHead');
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <contenttemplate>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Manage Study Documents</h3>
            </div>
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="box-body">
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-primary" id="div3" runat="server">
                        <div class="box-header">
                            <h3 class="box-title">Define Document</h3>
                        </div>
                        <div class="rows">
                            <div style="height: 264px;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Document Name :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control width200px required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>Upload Documemt :</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:FileUpload ID="DocsFile" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                ControlToValidate="DocsFile"
                                                ValidationExpression="^.*\.(pdf|PDF)$"
                                                ErrorMessage="Only PDF(.pdf) File is Allow"
                                                Display="Dynamic"
                                                ForeColor="Red" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnSubDocs" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubDocs_Click" />
                                            <asp:Button ID="btnUptDocs" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUptDocs_Click" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnCALDocs" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCALDocs_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-primary" style="height: 264px;" runat="server">
                        <div class="box-header">
                            <h3 class="box-title">Records</h3>
                            <div class="pull-right">
                                <asp:LinkButton ID="LbtnDocsExport" runat="server"  OnClick="LbtnDocsExport_Click" Font-Size="12px" CssClass="btn btn-info" Style="margin-top: 3px;" ForeColor="White">Export Documents&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                &nbsp;&nbsp; 
                            </div>
                        </div>
                        <br />
                        <div class="rows" style="margin-top: 5px;">
                            <div>
                                <asp:GridView ID="grd_docs" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                    Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grd_docs_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnedit" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                    CommandName="EDIT_DOCSNAME" ToolTip="Edit">
                                                        <i class="fa fa-edit"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOCS" runat="server" Text='<%# Bind("DOC_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfilename" runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('IWRS_DOCS', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color: blue; font-size: 15px"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtndelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Document : ", Eval("DOC_NAME")) %>' CommandArgument='<%# Bind("ID") %>'
                                                    CommandName="DELETE_DOCSNAME" ToolTip="Delete">
                                                        <i class="fa fa-trash-o"></i>
                                                </asp:LinkButton>
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
    </contenttemplate>
</asp:Content>
