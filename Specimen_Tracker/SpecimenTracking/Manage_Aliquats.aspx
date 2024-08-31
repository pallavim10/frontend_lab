<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage_Aliquats.aspx.cs" Inherits="SpecimenTracking.Manage_Aliquats" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Manage Aliquats</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Manage Aliquats</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Add Aliquot Details</h3>
                                        <div class="pull-right">
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Enter Sequence Number : &nbsp;</label>
                                                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtSeqtNo" runat="server" CssClass="form-control required numeric w-50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Eneter Aliquot ID : &nbsp;</label>
                                                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtaliquotID" runat="server" CssClass="form-control required w-50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Eneter Aliquot Type : &nbsp;</label>
                                                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:TextBox ID="txtaliquottype" runat="server" CssClass="form-control required w-50"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <center>
                                                            <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false" OnClick="lbnUpdate_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                                        </center>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Records</h3>
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExport_Click" ForeColor="Black">Export Aliquot &nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                            &nbsp;&nbsp;
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <asp:GridView ID="GrdAliquots" AutoGenerateColumns="false" runat="server" class="table table-bordered table-striped responsive grid" DataKeyNames="ID" EmptyDataText="No Data Found!" Width="100%">
                                                    <Columns>
                                                        <%--<asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnview" runat="server" class="btn-info btn-xs" ToolTip="View" CommandName="ViewUser" CommandArgument='<%# Bind("ID") %>' OnClientClick="setFocusOnDiv();"><i class="fas fa-eye btn-xs"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUsser" runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="text-center  align-middle" ItemStyle-CssClass="text-center align-middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblname" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
