<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_PROC_UPLOAD.aspx.cs" Inherits="CTMS.eTMF_PROC_UPLOAD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Upload Documents</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfMainDocId" />
        <div class="row" id="div1" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    File Name:
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblfilename" runat="server" Font-Bold="true" ForeColor="Maroon"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row" id="div13" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Note : &nbsp;
                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" CssClass="form-control width300px"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" id="div6" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Select File : &nbsp;
                    <asp:Label ID="Label16" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="X-Small" />
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div12" runat="server" style="padding-top: 15px;">
            <div class="col-md-12">
                <div class="label col-md-2">
                </div>
                <div class="col-md-3" align="right">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnUpload_Click" />
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
