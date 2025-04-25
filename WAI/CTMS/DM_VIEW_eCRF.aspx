<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_VIEW_eCRF.aspx.cs" Inherits="CTMS.DM_VIEW_eCRF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">View eCRF</h3>
        </div>
        <div class="form-group">
            <div class="has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="rows">
                <div style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label width90px">
                            Select Module :
                        </label>
                        <div class="Control">
                            <asp:DropDownList runat="server" ID="ddlModule" CssClass="form-control required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="display: inline-flex">
                    <div style="display: inline-flex">
                        <div class="Control">
                            <asp:Button ID="btnPrint" runat="server" Text="Print CRF" CssClass="btn btn-sm btn-primary" OnClick="btnPrint_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
