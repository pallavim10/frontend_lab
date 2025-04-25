<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NCTMS_VISIT_REPORT.aspx.cs" Inherits="CTMS.NCTMS_VISIT_REPORT" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                View SAE Report
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <rsweb:ReportViewer ID="ReportViewer1" AsyncRendering="false" SizeToReportContent="true"
                    runat="server" Height="100%" Width="100%">
                </rsweb:ReportViewer>
            </div>
        </div>
    </div>
</asp:Content>
