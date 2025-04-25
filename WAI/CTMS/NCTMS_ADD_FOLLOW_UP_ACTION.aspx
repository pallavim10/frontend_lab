<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NCTMS_ADD_FOLLOW_UP_ACTION.aspx.cs" Inherits="CTMS.NCTMS_ADD_FOLLOW_UP_ACTION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: true
            });

        }
    </script>
    <script type="text/javascript">
        CKEDITOR.config.toolbar = [
           ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
           ['Styles', 'Format', 'Font', 'FontSize', 'Table']
           ];

        CKEDITOR.config.height = 150;

        function CallCkedit() {

            CKEDITOR.replace("MainContent_txtEmailBody");

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Add Follow-Up Action</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Site ID:
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblSITEID" runat="server" CssClass="form-control"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Visit Type:
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblVisitType" runat="server" CssClass="form-control"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Visit Id:
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblVisitId" runat="server" CssClass="form-control"></asp:Label>
                </div>
            </div>
        </div>
        <div id="Div1" class="row" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Comment:
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtComment" CssClass="ckeditor required" Height="10%"
                        TextMode="MultiLine" Width="99%"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" id="div12" runat="server" style="padding-top: 15px; padding-bottom: 15px;">
            <div class="col-md-12">
                <div class="col-md-2">
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary btn-sm"
                        OnClick="btnBack_Click" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm cls-btnSave"
                        Visible="false" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                        OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Records</h3>
        </div>
        <div class="box">
            <asp:GridView runat="server" ID="grdFollowupActions" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped Datatable" OnRowCommand="grdFollowupActions_RowCommand"
                AllowSorting="true" Width="100%" OnPreRender="grd_data_PreRender">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                        <ItemTemplate>
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Site Id" HeaderStyle-CssClass="txtCenter" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visit Type">
                        <ItemTemplate>
                            <asp:Label ID="VISIT_NAME" runat="server" Text='<%# Bind("VISIT_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                        <ItemTemplate>
                            <asp:Label ID="VISITID" runat="server" Text='<%# Bind("VISITID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visit Id" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="SVID" runat="server" Text='<%# Bind("SVID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comment">
                        <ItemTemplate>
                            <asp:Literal ID="COMMENT" runat="server" Text='<%# Bind("COMMENT") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Entered By">
                        <ItemTemplate>
                            <asp:Label ID="User_Name" runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Entered Date">
                        <ItemTemplate>
                            <asp:Label ID="ENTEREDAT" runat="server" Text='<%# Bind("ENTEREDAT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="EditFollowUpAction" CommandArgument='<%# Bind("ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="DeleteFollowUpAction"
                                CommandArgument='<%# Bind("ID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
