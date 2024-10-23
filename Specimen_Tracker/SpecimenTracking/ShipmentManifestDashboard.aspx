<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShipmentManifestDashboard.aspx.cs" Inherits="SpecimenTracking.ShipmentManifestDashboard" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .Body {
            margin-left: 7%;
        }

        .Margin7 {
            margin-left: 7%;
            margin-right: 7%;
            font-size: larger;
            margin-top: 1%;
        }

        .box {
            margin-bottom: 0%;
            margin-bottom: 0px;
            width: auto;
        }

        .fontBold {
            font-weight: bold;
        }

        .color {
            color: #333333;
        }

        .colorRed {
            color: #ff0000;
        }

        .hover-end {
            padding: 0;
            margin: 0;
            font-size: 75%;
            text-align: center;
            position: absolute;
            bottom: 0;
            width: 100%;
            opacity: 0.8;
        }

        .nav-tabs-custom > .deepak > .nav-tabs > li.active {
            border-top-color: #3c8dbc;
            border-top-style: solid;
            border-top: 3px solid #3c8dbc;
            margin-bottom: -2px;
            margin-right: 5px;
            position: relative;
            list-style: none;
        }

        h3 {
            background-color: #007bff;
            padding: 0.4em 1em;
            margin-top: 0px;
            font-weight: bold;
            color: white;
        }

        .colorText {
            color: #212529;
        }

        .textcoutcolor {
            color: #212529;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Dashboard</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx?menu=Home">Home</a></li>
                            <li class="breadcrumb-item active">Dashboard</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="data-original-title">
                    </div>
                    <div class="col-md-12 row">
                        <asp:ListView ID="lstm" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstm_ItemDataBound">
                            <GroupTemplate>
                                <div class="col-md-4">
                                    <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                </div>
                            </GroupTemplate>
                            <ItemTemplate>
                                <a id="main" runat="server" href='<%# DataBinder.Eval(Container.DataItem, "NavigationURL") %>' title='<%# Eval("FunctionName") %>' style="color: white; text-align: left">
                                    <div class="info-box" style="height: 100px;">
                                        <span class="info-box-icon" style='<%# "text-align: left; height: 80px;width: 80px; background-color:" + DataBinder.Eval(Container.DataItem, "Color") + ";" %>'><i id="ICONCLASS" runat="server" class='<%# Eval("Icon") %>'></i></span>
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
        </section>
    </div>
</asp:Content>
