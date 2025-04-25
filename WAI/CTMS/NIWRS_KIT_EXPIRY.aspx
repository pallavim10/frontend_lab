<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_KIT_EXPIRY.aspx.cs" Inherits="CTMS.NIWRS_KIT_EXPIRY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Update Kit Expiry
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <div style="display: inline-flex">
                                <label class="label width80px">
                                    By Block No. :
                                </label>
                                <div class="Control">
                                    <asp:DropDownList runat="server" ID="ddlBlock" CssClass="form-control drpControl"
                                        OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="display: inline-flex">
                                <label class="label width80px">
                                    By Kit No. :
                                </label>
                                <div class="Control">
                                    <asp:DropDownList runat="server" ID="ddlKitNo" CssClass="form-control drpControl"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlKitNo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="display: inline-flex">
                                <label class="label width100px">
                                    Current Expiry Date :
                                </label>
                                <div class="Control">
                                    <asp:TextBox runat="server" ID="txtExpiryDate" CssClass="form-control txtDate width150px" OnTextChanged="txtExpiryDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
    <div class="box box-danger">
        <div class="box-body" id="DivData" runat="server" visible="false">
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Available in Central Depot :
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="lblCentralDepot" CssClass="form-control"> 
                        </asp:Label>
                    </div>
                    <div class="label col-md-3">
                        Available in Central Country:
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="lblCentralCountry" CssClass="form-control"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Available in Country Depot :
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="lblCountryDepot" CssClass="form-control"> 
                        </asp:Label>
                    </div>
                    <div class="label col-md-3">
                        Available in Country Site:
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="lblCountrySite" CssClass="form-control"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Available in Site :
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="lblSite" CssClass="form-control"> 
                        </asp:Label>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="label col-md-2">
                        Enter Expiry Date :
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtExp" Width="80%" CssClass="form-control required txtDate"> 
                        </asp:TextBox>
                    </div>
                    <div class="label col-md-3">
                        Enter Reason :
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtDesc" Width="300px" Height="60px" TextMode="MultiLine"
                            CssClass="form-control required"> 
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12 txt_center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnSubmit_Click" Style="margin-left: 20px;" />&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
