<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnalyzingLaboratoryHomePage.aspx.cs" Inherits="SpecimenTracking.AnalyzingLaboratoryHomePage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Analyzing Laboratory</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="AnalyzingLaboratoryHomePage.aspx">Analyzing Laboratory</a></li>
                            <li class="breadcrumb-item active">Analyzing Laboratory</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
         <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="data-original-title">
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row" id="div4" runat="server">
                                    <asp:ListView ID="lstm" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstm_ItemDataBound">
                                        <GroupTemplate>
                                            <div class="col-lg-3 col-xs-6">
                                                <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                            </div>
                                        </GroupTemplate>
                                        <ItemTemplate>
                                            <a id="main" runat="server" href='<%# Eval("NavigationURL")%>' title='<%# Eval("FunctionName") %>' style="color: white; text-align: left">
                                                <div class="info-box">
                                                    <span class="info-box-icon" style='<%# "text-align: left;background-color:" + DataBinder.Eval(Container.DataItem, "Color") + ";" %>'><i id="ICONCLASS" runat="server" class='<%# Eval("Icon") %>'></i></span>
                                                    <div class="info-box-content">
                                                        <span id="FunctionName" runat="server" class="info-box-text text-md colorText"><%# Eval("FunctionName") %></span>
                                                        <span class="info-box-number text-lg textcoutcolor">
                                                            <asp:Label ID="lbltotal" runat="server"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </a>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
