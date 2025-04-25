<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_ALLOC_KITS.aspx.cs" Inherits="CTMS.NIWRS_ALLOC_KITS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Allocate Kits
                    </h3>
                </div>
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpSITEID" runat="server" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="Div1" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    From Kit No. :
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpFrom" runat="server" CssClass="form-control required" OnSelectedIndexChanged="drpFrom_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="Div2" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    To Kit No.:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpTo" runat="server" CssClass="form-control required" AutoPostBack="True"
                                        OnSelectedIndexChanged="drpTo_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="Div3" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <asp:LinkButton runat="server" ID="lbtnAllocate" Text="Allocate" ForeColor="White"
                                    CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnAllocate_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 20%;">
                                    <asp:GridView ID="gvKITS" runat="server" AllowSorting="True" Style="margin-left: 10%;"
                                        AutoGenerateColumns="False" CssClass="table table-bordered table-striped txt_center Datatable1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Kit Number" HeaderStyle-Width="50%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_KIT" Width="100%" CssClass="label" runat="server" Text='<%# Bind("KITNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
