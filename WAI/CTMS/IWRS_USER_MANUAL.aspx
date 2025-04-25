<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IWRS_USER_MANUAL.aspx.cs" Inherits="CTMS.IWRS_USER_MANUAL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function changePDFSource(newSrc) {
            document.getElementById("pdfViewer").src = newSrc;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">User Manaul </h3>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="box-body">
        <div id="root">
            <iframe ID="pdfViewer" src="" style="height:600px; overflow-x: hidden" width="100%"></iframe>
        </div>
    </div>
</asp:Content>
