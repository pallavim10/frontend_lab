<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="SpecimenTracking.HomePage" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
        .col-md-3 {
    /*max-width: 100% !important;*/
}
}
    </style>
    <link href="fonts/New%20Font%20Awesome/css/all.css" rel="stylesheet" />
    <link href="fonts/New%20Font%20Awesome/css/fontawesome.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Home</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Home</li>
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
                            <div class='<%# lstm.Items.Count == 1 ? "col-lg-12" : "col-md-12" %>'>
                                <div class="row" id="div4" runat="server">
                                    <asp:ListView ID="lstm" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstm_ItemDataBound">
                                       <%-- <GroupTemplate>
                                            <div id="grptemplate">
                                                <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                            </div>
                                        </GroupTemplate>
                                        <ItemTemplate>
                                            <div id="itemTemplateDiv" class="col-md-3">
                                            <a id="main" runat="server" href='<%# DataBinder.Eval(Container.DataItem, "SysURL") +"?menu="+ DataBinder.Eval(Container.DataItem, "SystemName") %>' title='<%# Eval("SystemName") %>' style="color: white; text-align: left">
                                                <div class="info-box">
                                                    <span class="info-box-icon" style='<%# "text-align: left;background-color:" + DataBinder.Eval(Container.DataItem, "Color") + ";" %>'><i id="ICONCLASS" runat="server" class='<%# Eval("Icon") %>'></i></span>
                                                    <div class="info-box-content">
                                                        <span id="FunctionName" runat="server" class="info-box-text text-md colorText"><%# Eval("SystemName") %></span>
                                                        <span class="info-box-number text-lg textcoutcolor">
                                                            <asp:Label ID="lbltotal" runat="server"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </a>
                                                </div>
                                        </ItemTemplate>--%>
                                         <GroupTemplate>
                            <div class='<%# lstm.Items.Count == 1 ? "col-lg-12" : "col-lg-3 col-xs-6" %>'>
                                <asp:LinkButton ID="itemPlaceholder" runat="server" />
                            </div>
                        </GroupTemplate>
                        <ItemTemplate>
                            <a id="main" runat="server" href='<%# DataBinder.Eval(Container.DataItem, "SysURL") +"?menu="+ DataBinder.Eval(Container.DataItem, "SystemName") %>' title='<%# Eval("SystemName") %>' style="color: white; text-align: left">
                                                <div class="info-box">
                                                    <span class="info-box-icon" style='<%# "text-align: left;background-color:" + DataBinder.Eval(Container.DataItem, "Color") + ";" %>'><i id="ICONCLASS" runat="server" class='<%# Eval("Icon") %>'></i></span>
                                                    <div class="info-box-content">
                                                        <span id="FunctionName" runat="server" class="info-box-text text-md colorText"><%# Eval("SystemName") %></span>
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
    <asp:Label ID="openModalPwdExpiry" runat="server" ReadOnly="True" Text=""></asp:Label>
    <cc1:ModalPopupExtender ID="modalPwdExpiry" runat="server" BehaviorID="mpe" PopupControlID="pnlPwdExpiry" TargetControlID="openModalPwdExpiry"
        CancelControlID="btnPwdExpiryNo" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPwdExpiry" runat="server" CssClass="Popup1" align="center" Style="border: 5px solid #ccc; display: none;">
        <h3 class="heading">Password Expiry Alert !!!</h3>
        <div class="modal-body" runat="server">
            <div id="ModelPopup">
                <div class="row">
                    <asp:Label runat="server" ID="lblPwdExpDays" Style="color: red; font-size: 25px; font-weight: bold;"></asp:Label>
                </div>
                <br />
                <div class="row">
                    <label style="color: blue; font-size: 20px; font-weight: bold;">
                        Do you want to change it now?
                    </label>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12 txtCenter">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-5">
                            <asp:Button ID="btnPwdExpiryYes" runat="server" Text="Yes" CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnPwdExpiryYes_Click" />
                        </div>
                        <div class="col-md-5">
                            <asp:Button ID="btnPwdExpiryNo" runat="server" Text="No" CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" />
                        </div>
                        <div class="col-md-1">
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
