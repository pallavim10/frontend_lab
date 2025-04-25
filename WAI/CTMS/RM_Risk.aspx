<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RM_Risk.aspx.cs" Inherits="CTMS.RM_Risk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Manage Masters
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
                            <div class="label col-md-2">
                                Category :
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlcategory" runat="server" Width="250px" AutoPostBack="true"
                                    class="form-control drpControl required" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Sub Category : &nbsp;
                                <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlsubcategory" runat="server" Width="250px" AutoPostBack="true"
                                    class="form-control drpControl required" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Phase :&nbsp;
                                <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtPhase" CssClass="form-control required width250px"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                Factor :&nbsp;
                                <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlfactor" Width="250px" runat="server" AutoPostBack="true"
                                    class="form-control drpControl required">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Risk Considerations :&nbsp;
                                <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRiskCons" Height="36px" Width="720px" TextMode="MultiLine"
                                    CssClass="form-control "> 
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Suggested Mitigation :&nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtSugMitig" Height="36px" Width="720px" TextMode="MultiLine"
                                    CssClass="form-control "> 
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Suggested Risk category : &nbsp;
                                <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox runat="server" ID="txtSugRiskCat" Height="36px" Width="720px" TextMode="MultiLine"
                                    CssClass="form-control "> 
                                </asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Risk Nature : &nbsp;
                                <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:RadioButton ID="radio1" runat="server" GroupName="NATURE" Text="Static" />&nbsp;&nbsp;
                                <asp:RadioButton ID="radio2" runat="server" GroupName="NATURE" Text="Dynamic" />
                            </div>
                            <div class="label col-md-2">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Key Value : &nbsp;
                                <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:RadioButton Text="Yes" runat="server" ID="radyes" Checked="true" GroupName="key" />&nbsp;&nbsp;
                                <asp:RadioButton Text="No" runat="server" ID="radno" GroupName="key" />
                            </div>
                            <div class="label col-md-2">
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Transcelerate Category : &nbsp;
                                <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtTransCat" runat="server" Height="36px" Width="720px" TextMode="MultiLine"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-2">
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm"
                                OnClick="btnsubmit_Click" Style="margin-left: 20px;" />
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
