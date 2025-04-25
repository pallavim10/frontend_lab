<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProtDev_New.aspx.cs" Inherits="CTMS.ProtDev_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Protocol Deviation</h3>
        </div>
        <div class="row">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <table>
            <tr>
                <td class="label">
                    Project
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Drp_Project" runat="server" class="form-control drpControl required"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label">
                    Site ID
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_InvID" runat="server" AutoPostBack="true" class="form-control drpControl required"
                                OnSelectedIndexChanged="drp_InvID_SelectedIndexChanged1">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label">
                    Subject ID
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_SUBJID" runat="server" AutoPostBack="true" class="form-control drpControl required ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Department
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_DEPT" runat="server" AutoPostBack="true" class="form-control drpControl required">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label">
                    Visit No.
                </td>
                <td>
                    <asp:TextBox ID="txtVISITNUM" runat="server" class="form-control"></asp:TextBox>
                </td>
                <td class="label">
                    Date of Identified
                </td>
                <td>
                    <asp:TextBox ID="txtDateIdentified" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td class="label">
                    Status
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_Status" runat="server" AutoPostBack="true" class="form-control drpControl required">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label">
                    Source
                </td>
                <td>
                    <asp:TextBox ID="txtSource" runat="server" class="form-control"></asp:TextBox>
                </td>
                <td class="label">
                    Reference
                </td>
                <td>
                    <asp:TextBox ID="txtReference" runat="server" class="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="label">
                    Category
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drp_Nature" CssClass="form-control required"
                                AutoPostBack="true" OnSelectedIndexChanged="drp_Nature_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label">
                    Sub Category
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drp_PDCode1" CssClass="form-control required"
                                AutoPostBack="True" OnSelectedIndexChanged="drp_PDCode1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label">
                    Factor
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drp_PDCode2" CssClass="form-control required"
                                OnSelectedIndexChanged="drp_PDCode2_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Classification
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_Priority_Ops" runat="server" class="form-control drpControl required">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label">
                    Re-Classification
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_Priority_Med" runat="server" class="form-control drpControl">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label">
                    Final Classification
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="drp_Priority_Final" runat="server" class="form-control drpControl ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Summary
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtSummary" runat="server" CssClass=" form-control required" TextMode="MultiLine"
                        Width="565px" Height="36px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Description
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtDescription" runat="server" class="form-control" TextMode="MultiLine"
                        Width="565px" Height="36px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="label">
                    Date of Ocuurence
                </td>
                <td>
                    <asp:TextBox ID="txtOCDate" runat="server" class="form-control txtDate"></asp:TextBox>
                </td>
                <td class="label">
                    Date of Report
                </td>
                <td>
                    <asp:TextBox ID="txtCloseDate" runat="server" class="form-control txtDate"></asp:TextBox>
                </td>
                <td class="label">
                    Count
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtPdmasterID" runat="server" Visible="false" class="form-control"></asp:TextBox>
                            <asp:TextBox ID="txtCount" runat="server" Style="color: Red" class="form-control txt_center"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Rationalise
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtRationalise" runat="server" class="form-control" TextMode="MultiLine"
                        Width="565px" Height="36px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    <asp:Button ID="bntSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm cls-btnSave margin-left10 "
                        OnClick="bntSave_Click" />
                </h3>
            </div>
        </div>
    </div>
    <div class="col-md-8">
        <div id="tabscontainer" class="nav-tabs-custom" runat="server">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab-1" data-toggle="tab">Comments</a></li>
                <li><a href="#tab-2" data-toggle="tab">Impact</a></li>
                <li><a href="#tab-3" data-toggle="tab">CAPA</a></li>
            </ul>
            <div class="tab">
                <div id="tab-1" class="tab-content current">
                    <asp:GridView ID="grdCmts" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" OnRowDataBound="grdCmts_RowDataBound">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Comment" runat="server" Text='<%# Bind("Comment") %>' CssClass="form-control"
                                        TextMode="MultiLine" Width="100%" Height="40px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EnteredDate" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="DTENTERED" Text='<%# Bind("DTENTERED") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EnteredBy" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="ENTEREDBY" Text='<%# Bind("ENTEREDBY") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UPDATE_FLAG_cmt" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="UPDATE_FLAG_cmt" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_cmt") %>'
                                        Width="22px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="30px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Button ID="bntCommentAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                        OnClick="bntCommentAdd_Click" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
                <div id="tab-2" class="tab-content">
                    <asp:GridView ID="grdImpact" runat="server" AutoGenerateColumns="false" CssClass="Gtable table-bordered table-striped margin-top4"
                        AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdImpact_RowDataBound" PagerStyle-CssClass="pgr">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="PROTVOIL ID" ItemStyle-CssClass="txt_center width80px">
                                <ItemTemplate>
                                    <asp:TextBox ID="PROTVOIL_ID" runat="server" Text='<%# Bind("PROTVOIL_ID") %>' ReadOnly="true"
                                        Width="60px" Style="text-align: center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Impact" ItemStyle-CssClass="txt_center width250px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Impact" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UPDATE_FLAG_Impact" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="UPDATE_FLAG_Impact" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_Impact") %>'
                                        Width="22px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="txt_center">
                                <HeaderTemplate>
                                    <asp:Button ID="Add" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                        OnClick="bntImpactAdd_Click" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
                <div id="tab-3" class="tab-content">
                    <asp:GridView ID="grdCAPA" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" OnRowDataBound="grdCAPA_RowDataBound">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="PROTVOIL ID" ItemStyle-CssClass="txt_center width80px">
                                <ItemTemplate>
                                    <asp:TextBox ID="PROTVOIL_ID" runat="server" Text='<%# Bind("PROTVOIL_ID") %>' ReadOnly="true"
                                        Width="60px" Style="text-align: center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CAPA" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:TextBox ID="CAPA" runat="server" Text='<%# Bind("CAPA") %>' CssClass="form-control width250pximp" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Responsibility" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Responsibility" runat="server" Width="120px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Resolution Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Resolution_DT" runat="server" Text='<%# Bind("Resolution_DT") %>'
                                        CssClass="form-control txtDate" Width="100px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UPDATE_FLAG_CAPA" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="UPDATE_FLAG_CAPA" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_CAPA") %>'
                                        Width="22px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="30px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                        OnClick="bntCAPAAdd_Click" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
