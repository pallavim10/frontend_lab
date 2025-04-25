<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Code_Action.aspx.cs" Inherits="CTMS.Code_Action" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-primary">
        <div class="box-header">
            <div>
                <h3 class="box-title">User Action</h3>
            </div>
        </div>
        <div class="box-header">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Select Action: &nbsp;
                                <asp:Label ID="Label8" runat="server" Font-Size="Small"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="drpAction" CssClass="form-control  width200px required">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                            <asp:ListItem Value="Disapprove" Text="Disapprove"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="label col-md-2">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="label col-md-2">
                    Enter Comment :&nbsp;
                                <asp:Label ID="Label2" runat="server" Font-Size="Small"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="form-control width200px"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="margin-top: 10px;">
            <div class="col-md-12">
                <div class="col-md-2">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                        Text="Submit" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancle" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="btnCancle_Click" />

                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
