<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_PROJECT.aspx.cs" Inherits="SpecimenTracking.UMT_PROJECT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="Scripts/btnSave_Required.js"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Mailbox Configuration</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">Manage Project</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Project Details</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExport_Click" ForeColor="Black">Export Details&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp;                       
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>
                                            Enter Sponsor Name:
                                        </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtSponsorName" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>
                                            Enter Project name:
                                        </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtProjectName" Style="width: 100%;" runat="server"
                                            Text="" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <center>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmit_Click" Text="Submit" />
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
