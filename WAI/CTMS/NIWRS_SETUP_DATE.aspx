<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_SETUP_DATE.aspx.cs" Inherits="CTMS.NIWRS_SETUP_DATE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
    </div>
    <div class="box-body">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-primary" id="div11" runat="server">
                    <div class="box-header">
                        <h3 class="box-title">
                            Change System Date</h3>
                    </div>
                    <div class="rows">
                        <div style="height: 264px;">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Enter Date :</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox runat="server" ID="txtCurrentDate" CssClass="form-control required txtDate"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Button runat="server" ID="btnChange" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="txtCurrentDate_TextChanged" Text="Change Date" />
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
