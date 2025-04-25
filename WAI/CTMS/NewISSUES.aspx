<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NewISSUES.aspx.cs" Inherits="CTMS.ISSUES" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style4
        {
            width: 188px;
        }
        .style5
        {
        }
        .style7
        {
            width: 145px;
        }
        .style9
        {
            width: 49px;
        }
        .style10
        {
            width: 151px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                New Issue</h3>
        </div>
        <div class="row">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="Pagelevel">
            <div class="row margin-top4">
                <div class="col-sm-1 ">
                    <div class="label">
                        Project
                    </div>
                </div>
                <div class="col-sm-2 ">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Drp_Project" runat="server" class="form-control drpControl required"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-2">
                    <div class="label" style="margin-left: 120px;">
                        Site ID</div>
                </div>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_InvID" runat="server" AutoPostBack="true" Width="196px"
                                class="form-control drpControl" OnSelectedIndexChanged="drp_InvID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-top4">
                <div class=" col-sm-1">
                    <div class="label">
                        Subject ID
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_SUBJID" runat="server" AutoPostBack="true" class="form-control drpControl">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-2">
                    <div class="label" style="margin-left: 120px;">
                        Department
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_DEPT" runat="server" AutoPostBack="true" class="form-control drpControl required"
                                Width="196px">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-top4">
                <div class=" col-sm-1">
                    <div class="label">
                        Source
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtSource" runat="server" class="form-control required" Width="150px"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <div class="label" style="margin-left: 120px;">
                        Reference
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtReference" runat="server" class="form-control" Width="196px"></asp:TextBox>
                </div>
            </div>
            <div class="row margin-top4">
                <div class="col-sm-1">
                    <div class="label">
                        Summary
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtSummary" runat="server" CssClass=" form-control required" TextMode="MultiLine"
                        Width="576px"></asp:TextBox>
                </div>
            </div>
            <div class="row margin-top4">
                <div class="col-sm-1">
                    <div class="label">
                        Status
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_Status" runat="server" AutoPostBack="true" class="form-control drpControl required">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-1">
                    <div class="label">
                        Classification
                    </div>
                </div>
                <div class="col-sm-1">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_Priority" runat="server" AutoPostBack="true" Width="100px"
                                Style="margin-left: -20px;" class="form-control drpControl required">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-1">
                    <div class="label" style="margin-left: 20px">
                        Keywords
                    </div>
                </div>
                <div class="col-sm-1 ">
                    <asp:TextBox ID="txtKeywords" runat="server" class="form-control" Width="120px" Style="margin-left: -20px"></asp:TextBox>
                </div>
            </div>
            <div class="row margin-top4">
                <div class="col-sm-1">
                    <div class="label">
                        Description
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control" TextMode="MultiLine"
                        Width="576px"></asp:TextBox>
                </div>
            </div>

            <%--<div class="row margin-top4">
                <div class=" col-sm-1">
                    <div class="label">
                        Source
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="TextBox1" runat="server" class="form-control required" Width="150px"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <div class="label" style="margin-left: 120px;">
                        Reference
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="TextBox2" runat="server" class="form-control" Width="196px"></asp:TextBox>
                </div>
            </div>--%>

            <div class="row margin-top4">
                <div class="col-sm-1">
                    <div class="label">
                        Nature
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_Nature" runat="server" AutoPostBack="true" class="form-control drpControl required">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-2">
                    <div class="label" style="margin-left: 120px;">
                        Category
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_PDCode1" runat="server" AutoPostBack="true" Width="196px"
                                class="form-control drpControl" OnSelectedIndexChanged="drp_PDCode1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-top4">
                <div class="col-sm-1">
                    <div class="label">
                        Sub Category
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_PDCode2" runat="server" AutoPostBack="true" 
                                class="form-control drpControl" OnSelectedIndexChanged="drp_PDCode2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-2">
                    <div class="label" style="margin-left: 120px;">
                        Factor
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlFactor" runat="server" AutoPostBack="true"  Width="196px" class="form-control drpControl required">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row margin-top4">
                <div class="col-sm-1">
                    <div class="label">
                        Attachment
                    </div>
                </div>
                <div class="col-sm-3">
                    <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" EnableViewState="true"
                        Font-Size="Small" />
                </div>
                <div class="col-sm-2">
                    <asp:Button runat="server" ID="upload" Text="Upload" Font-Size="Small" OnClick="upload_Click"
                        CssClass="disp-none" />
                </div>
            </div>
            <div>
                <asp:GridView ID="grdAttachment" runat="server" Width="100%" AutoGenerateColumns="false"
                    CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                    PagerStyle-CssClass="pgr">
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Attachment" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="Name" runat="server" Text='<%# Bind("Name") %>' CssClass="form-control"
                                    TextMode="MultiLine" Width="100%" Height="40px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ContentType" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:TextBox ID="ContentType" runat="server" Font-Size="X-Small" Text='<%# Bind("ContentType") %>'
                                    Width="22px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attachments" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:TextBox ID="Attachments" runat="server" Font-Size="X-Small" Text='<%# Bind("Attachments") %>'
                                    Width="22px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr"></PagerStyle>
                </asp:GridView>
            </div>
            <br />
            <asp:Button ID="bntSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm cls-btnSave "
                OnClick="bntSave_Click" />
        </div>
    </div>
</asp:Content>
