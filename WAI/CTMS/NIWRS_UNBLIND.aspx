<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_UNBLIND.aspx.cs" Inherits="CTMS.NIWRS_UNBLIND" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Unblinding Request</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="rows">
                                <div style="height: 264px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Site :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label runat="server" ID="lblSite" CssClass="form-control width200px"> 
                                                </asp:Label>
                                                <asp:HiddenField runat="server" ID="hfRESPOEMAIL" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Randomization Number :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label runat="server" ID="lblRandNum" CssClass="form-control width200px"> 
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                
                                                    <asp:Label runat="server" ID="SUBJECTTEXT">&nbsp;:</asp:Label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label runat="server" ID="lblSUBJID" CssClass="form-control width200px"> 
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Treatment Arms :</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Label runat="server" ID="lblTreat" CssClass="form-control width200px"> 
                                                </asp:Label>
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
        </div>
    </div>
</asp:Content>
