<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MM_QueryRouted_Comments.aspx.cs" Inherits="CTMS.MM_QueryRouted_Comments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatePNL">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Response against Queries Routed to MM</h3>
                </div>
                <div class="row">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        <asp:HiddenField runat="server" ID="DM_QUERYID" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Site ID : 
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblSITEID" runat="server" CssClass="form-control width300px"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Subject ID : 
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblSUBJID" runat="server" CssClass="form-control width300px"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Visit Name : 
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblVISIT" runat="server" CssClass="form-control width300px"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Module Name : 
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblMODULENAME" runat="server" CssClass="form-control width300px"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Query Text : 
                        </div>
                        <div class="col-md-7">
                            <asp:Label ID="lblQUERYTEXT" TextMode="MultiLine" runat="server" CssClass="form-control width300px"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Existing Comments : 
                        </div>
                        <div class="col-md-9">
                            <asp:GridView runat="server" ID="grdComments" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnPreRender="grd_data_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderText="Reason">
                                        <ItemTemplate>
                                            <asp:Label ID="lblREASON" runat="server" Text='<%# Eval("REASON") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOMMENTS" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <label>Comment Details</label><br />
                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Commented By]</label><br />
                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div>
                                                <div>
                                                    <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select Reason : 
                        </div>
                        <div class="col-md-7">
                            <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control required width300px" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Close query" Value="Closed query"></asp:ListItem>
                                <asp:ListItem Text="Route to DM" Value="Routed to DM"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Enter Comments : 
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control required width300px" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" id="div12" runat="server">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-danger btn-sm"
                                OnClick="btnBack_Click" />
                        </div>
                        <div class="col-md-4" cs>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBack" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
