<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_DataEntry_Mutiple.aspx.cs" Inherits="CTMS.DM_DataEntry_Mutiple" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 46px;
            background-color: #E98454;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                DATA ENTRY &nbsp;||&nbsp;
                <asp:Label runat="server" ID="lblSiteId" />&nbsp;||&nbsp;<asp:Label runat="server"
                    ID="lblSubjectId" />&nbsp;||&nbsp;<asp:Label runat="server" ID="lblVisit" />&nbsp;||&nbsp;<asp:Label
                        runat="server" Visible="false" ID="lblIndication" />&nbsp;<asp:Label runat="server"
                            ID="lblPVID" />&nbsp;
            </h3>
        </div>
        <br />
        <div class="form-group" style="display: inline-flex">
            <label class="label">
                Select Visit:
            </label>
            <div class="Control">
                <asp:DropDownList ID="drpVisit" runat="server" AutoPostBack="True" CssClass="form-control "
                    OnSelectedIndexChanged="drpVisit_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" style="display: inline-flex">
            <label class="label">
                Select Visit Count:
            </label>
            <div class="Control">
                <asp:DropDownList ID="drpVisitCount" runat="server" AutoPostBack="True" CssClass="form-control "
                    OnSelectedIndexChanged="drpVisitCount_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group" style="display: inline-flex">
            <label class="label">
                Select Module:
            </label>
            <div class="Control">
                <asp:DropDownList ID="drpModule" runat="server" AutoPostBack="True" CssClass="form-control "
                    OnSelectedIndexChanged="drpModule_SelectedIndexChanged1">
                </asp:DropDownList>
            </div>
        </div>
        <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;
            margin-left: 10px; font-size: 14px;"></asp:Label>
        <div class="box-body">
            <div class="form-group">
                <table class="style1">
                    <tr>
                        <td>
                            <asp:Label ID="lblRemaining" runat="server" Style="color: #CC3300; font-weight: 700;
                                margin-left: 80px; font-size: 15px;"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="list-group-item">
                        <td colspan="3" style="text-align: left">
                            <asp:Label ID="lblModuleName" runat="server" Text="" Font-Size="12px" Font-Bold="true"
                                Font-Names="Arial"></asp:Label>
                        </td>
                        <td style="text-align: RIGHT">
                            <asp:Button ID="btnAdddNew" Text="ADD" runat="server" Style="margin-left: 925px"
                                CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnAdddNew_Click" />
                        </td>
                    </tr>
                    <%-- <tr class="list-group-item">
                        <td colspan="3" >
                         
 
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <%--<asp:GridView ID="grd_DATA" runat="server" CellPadding="3" Name="DSAE" AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped" ShowHeader="True" ToolTip="ADVERSE EVENT"
                                CellSpacing="2" OnRowDataBound="grd_DATA_RowDataBound" EmptyDataText="No records found">
                                <Columns>
                                    <asp:TemplateField HeaderText="PVID" HeaderStyle-CssClass="width100px align-center"
                                        ItemStyle-CssClass="width100px align-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPVID" Font-Size="Small" Text='<%# Bind("PVID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RECID" HeaderStyle-CssClass="width100px align-center"
                                        ItemStyle-CssClass="width100px align-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="EVENT CODE" HeaderStyle-CssClass="width100px align-center"
                                        ItemStyle-CssClass="width100px align-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeqNo" Font-Size="Small" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VIEW" HeaderStyle-CssClass="width100px align-center"
                                        ItemStyle-CssClass="width100px align-center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPAGENUM" runat="server" Text="View" CommandArgument="RECID"
                                                CommandName="Select" OnClick="lnkPAGENUM_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MANUAL QUERY" HeaderStyle-CssClass="width100px align-center"
                                        ItemStyle-CssClass="width100px align-center">
                                        <ItemTemplate>
                                            <img src="Images/manualquery2.png" alt="" id="lnkMANUALQUERY" title="Manual Query"
                                                height="16" width="19" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AUTO QUERY" HeaderStyle-CssClass="width100px align-center"
                                        ItemStyle-CssClass="width100px align-left">
                                        <ItemTemplate>
                                            <img src="Images/manualquery7.jpg" alt="" id="lnkAUTOQUERY" title="Auto Query" height="14"
                                                width="16" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COMMENTS" HeaderStyle-CssClass="width100px align-center"
                                        ItemStyle-CssClass="width100px align-center">
                                        <ItemTemplate>
                                            <img src="Images/index.png" id="lnkCOMMENTS" alt="" height="14" width="16" title="Comments"
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AUDIT TRAIL" HeaderStyle-CssClass="width100px align-center"
                                        ItemStyle-CssClass="width100px align-center">
                                        <ItemTemplate>
                                            <img src="Images/Audit_Trail.png" id="lnkAUDITTRAIL" title="Audit trail" height="15"
                                                width="18" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>--%>
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="1120">
                                <asp:GridView ID="grd_DATA" runat="server" CellPadding="3" Name="DSAE" AutoGenerateColumns="True"
                                    CssClass="table table-bordered table-striped" ShowHeader="True" ToolTip="ADVERSE EVENT"
                                    CellSpacing="2" OnRowDataBound="grd_DATA_RowDataBound" EmptyDataText="No records found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="width100px align-center" ItemStyle-CssClass="width100px align-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPAGENUM" runat="server" Text="View" CommandArgument="RECID"
                                                    CommandName="Select" OnClick="lnkPAGENUM_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manual Query" HeaderStyle-CssClass="width100px align-center disp-none"
                                            ItemStyle-CssClass="width100px align-center disp-none">
                                            <ItemTemplate>
                                                <img src="Images/manualquery2.png" alt="" id="lnkMANUALQUERY" title="Manual Query"
                                                    height="16" width="19" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query" HeaderStyle-CssClass="width100px align-center"
                                            ItemStyle-CssClass="width100px align-center">
                                            <ItemTemplate>
                                                <img src="Images/manualquery7.jpg" alt="" id="lnkAUTOQUERY" title="Auto Query" height="14"
                                                    width="16" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="width100px align-center disp-none"
                                            ItemStyle-CssClass="width100px align-center disp-none">
                                            <ItemTemplate>
                                                <img src="Images/index.png" id="lnkCOMMENTS" alt="" height="14" width="16" title="Comments"
                                                    runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audit Trail" HeaderStyle-CssClass="width100px align-center disp-none"
                                            ItemStyle-CssClass="width100px align-center disp-none">
                                            <ItemTemplate>
                                                <img src="Images/Audit_Trail.png" id="lnkAUDITTRAIL" title="Audit trail" height="15"
                                                    width="18" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RECID" HeaderStyle-CssClass="width100px align-center disp-none"
                                            ItemStyle-CssClass="width100px align-center disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRECID" Font-Size="Small" Text='<%# Bind("RECID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
