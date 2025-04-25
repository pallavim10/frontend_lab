<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MM_Change_Report_QueryText.aspx.cs" Inherits="CTMS.MM_Change_Report_QueryText" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Change Query Text</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Site ID : 
                </div>
                <div class="col-md-7">
                    <asp:Label ID="lblSITEID" runat="server" CssClass="form-control width300px"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Subject ID : 
                </div>
                <div class="col-md-7">
                    <asp:Label ID="lblSUBJID" runat="server" CssClass="form-control width300px"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Visit Name : 
                </div>
                <div class="col-md-7">
                    <asp:Label ID="lblVISIT" runat="server" CssClass="form-control width300px"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Module Name : 
                </div>
                <div class="col-md-7">
                    <asp:Label ID="lblMODULENAME" runat="server" CssClass="form-control width300px"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Old Query Text : 
                </div>
                <div class="col-md-7">
                    <asp:Label ID="lblOLDQUERYTEXT" TextMode="MultiLine" runat="server" CssClass="form-control width300px"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    New Query Text : 
                </div>
                <div class="col-md-7">
                    <asp:TextBox ID="txtNewQueryText" TextMode="MultiLine" runat="server" CssClass="form-control width300px required"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div12" runat="server">
            <div class="col-md-12">
                <div class="label col-md-2">
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-danger btn-sm"
                        OnClick="btnBack_Click" />
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
