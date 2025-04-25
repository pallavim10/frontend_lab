<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HomePage.aspx.cs" Inherits="CTMS.HomePage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    </style>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="form-group">
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div class="data-original-title">
        </div>
        <div class="row">
            <div class="col-md-12">
                <br />
                <div class="row" id="div4" runat="server">
                    <asp:ListView ID="lstm" runat="server" AutoGenerateColumns="false">
                        <GroupTemplate>
                            <div class="col-lg-3 col-xs-6">
                                <asp:LinkButton ID="itemPlaceholder" runat="server" />
                            </div>
                        </GroupTemplate>
                        <ItemTemplate>
                            <!-- small box -->
                            <a id="main" runat="server" href='<%# DataBinder.Eval(Container.DataItem, "SysURL") +"?menu="+ DataBinder.Eval(Container.DataItem, "SystemName") %>' title='<%# Eval("SystemName") %>' style="color: white; text-align: left">
                                <div id="divcol" runat="server" class="small-box" style='<%# "text-align: left;background-color:" + DataBinder.Eval(Container.DataItem, "Color") + ";" %>'>
                                    <div class="inner">
                                        <div style="font-size: x-large;">
                                            <%# Eval("SystemName") %>
                                        </div>
                                        <p>
                                            <br />
                                        </p>
                                        <br />
                                    </div>
                                    <div class="icon">
                                        <i id="ICONCLASS" runat="server" class='<%# Eval("Icon") %>'></i>
                                    </div>
                                    <div class="small-box-footer">
                                    </div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
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
