<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MM_Push_Query.aspx.cs" Inherits="CTMS.MM_Push_Query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Push MM Query To DM</h3>
            &nbsp&nbsp&nbsp&nbsp
            <div class="pull-left">
            </div>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <asp:HiddenField ID="hdnModuleid" runat="server" />
        <asp:HiddenField ID="hdnPVID" runat="server" />
        <asp:HiddenField ID="hdnRECID" runat="server" />
        <div class="row" style="margin-top: 10px;" id="div2" runat="server">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Site Id : &nbsp;
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblINVID" runat="server" CssClass="form-control width300px"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="margin-top: 10px;" id="div3" runat="server">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Subject Id: &nbsp;
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblSubjectid" runat="server" CssClass="form-control width300px"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="margin-top: 10px;" id="div1" runat="server">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Visit Name : &nbsp;
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblvisitname" runat="server" CssClass="form-control width300px"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="margin-top: 10px;" id="divmapto" runat="server">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Module Name : &nbsp;
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblmodulename" runat="server" CssClass="form-control width300px"></asp:Label>
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="margin-top: 10px;" id="div4" runat="server">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Select Field : &nbsp;
                    <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlFieldName" runat="server" class="form-control drpControl width300px required">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="margin-top: 10px;" id="div5" runat="server">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Enter Query Text: &nbsp;
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtQueryText" TextMode="MultiLine" runat="server" Style="height: 100px" MaxLength="1000"
                        CssClass="form-control width300px required"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row" id="div12" runat="server">
            <div class="col-md-12">
                <div class="label col-md-2">
                    <asp:Button ID="btnBack" runat="server" Text="Back To MM Query Report" CssClass="btn btn-danger btn-sm"
                        OnClick="btnBack_Click" />
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnSubmit_Click" />&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnShowQuery" runat="server" Text="Show Module Data" Style="margin-left: 10px;"
                        CssClass="btn btn-DarkGreen btn-sm" OnClick="lbtnShowQuery_Click" />
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
