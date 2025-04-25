<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NCTMS_SET_FU_LETTER.aspx.cs" Inherits="CTMS.NCTMS_SET_FU_LETTER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        //        CKEDITOR.config.toolbar = [
        //   ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Cut', 'Copy', 'Paste', 'Find', 'Replace', '-', 'Outdent', 'Indent', '-', 'NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        //   ['Image', 'Table', '-', 'Link', 'Flash', 'Smiley', 'TextColor', 'BGColor'],
        //   '/',
        //   ['Styles', 'Format', 'Font', 'FontSize']
        //   ];

        CKEDITOR.config.height = 500;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Follow Up Letter for &nbsp;&nbsp;
                <asp:Label ID="lblMVID" runat="server" />
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="txtFULETTER" CssClass="ckeditor" Height="100%" TextMode="MultiLine"
                            Width="100%"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div style="margin-left: 25%;">
                        <asp:LinkButton runat="server" ID="lbtnSave" Text="Save" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="lbtnSave_Click"></asp:LinkButton>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
