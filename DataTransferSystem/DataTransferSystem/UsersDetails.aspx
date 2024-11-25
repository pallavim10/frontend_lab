<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsersDetails.aspx.cs" Inherits="DataTransferSystem.UsersDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Users Details</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item text-blue">Manage Users</li>
                            <li class="breadcrumb-item active">User Details</li>
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
                                <h3 class="card-title">User Details</h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>User Name </label>
                                            <asp:TextBox runat="server" ID="txtUserName" ReadOnly="true" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Full Name </label>
                                            <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Email Address </label>
                                            <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control "></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailID" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Contact No </label>
                                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control "></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label>Department</label>
                                            <asp:TextBox runat="server" ID="txtDepartment" CssClass="form-control "></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <!-- textarea -->
                                        <div class="form-group">
                                            <label>Designation </label>
                                            <asp:TextBox runat="server" ID="txtDesignation" CssClass="form-control "></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <label>Permissions </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <asp:CheckBox runat="server" ID="chkMngMaster" />
                                            <label>Manage Master </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2">
                                        &nbsp;
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <asp:CheckBox runat="server" ID="chkMngUsers" />
                                            <label>Manage Users </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        &nbsp;
                                    </div>
                                </div>
                                <br />
                                <center>
                                    <asp:LinkButton runat="server" ID="lbtnUpdate" Text="Update" Style="width: 93px;" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnUpdate_Click"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" Style="width: 93px;" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

