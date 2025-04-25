<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_DATA_SDV.aspx.cs" Inherits="CTMS.CTMS_DATA_SDV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function DataEntry(element) {
            if ($("#<%=drpIndication.ClientID%>").val() == "0") {
                $("#<%=drpIndication.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var Indic = $("#<%= drpIndication.ClientID%>").val();
            var SUBJID = $(element).closest('tr').find('td:first').text();
            var Visit = $('#MainContent_gridData_wrapper th').eq($(element).index()).text();
            window.location.href = "DM_OpenCRF.aspx?Indic=" + Indic + "&SUBJID=" + SUBJID + "&Visit=" + Visit;
            return false;
        }

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }

        function Print() {
            if ($("#<%=drpInvID.ClientID%>").val() == "0") {
                $("#<%=drpInvID.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            if ($("#<%=drpIndication.ClientID%>").val() == "0") {
                $("#<%=drpIndication.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            if ($("#<%=drpSubID.ClientID%>").val() == "0") {
                $("#<%=drpSubID.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = '<%= Session["PROJECTID"] %>'
            var INVID = $("#<%=drpInvID.ClientID%>").val();
            var INDICATIONID = $("#<%=drpIndication.ClientID%>").val();
            var SUBJID = $("#<%=drpSubID.ClientID%>").val();
            var test = "ReportDM_Status_DrillDown.aspx?ProjectId=" + ProjectId + "&INVID=" + INVID + "&INDICATIONID=" + INDICATIONID + "&SUBJID=" + SUBJID + "&STATUSID=" + STATUSID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Data Status DrillDown
                <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                    Visible="false" CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control required">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Select Indication:
                            </label>
                            <div class="Control">
                                <asp:DropDownList runat="server" ID="drpIndication" CssClass="form-control required"
                                    AutoPostBack="True" OnSelectedIndexChanged="drpIndication_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Subject ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btngetdata_Click" />
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: 15px;"></asp:Label>
                        </div>
                        <div class="pull-right">
                            <asp:Button ID="btnSDV" runat="server" Text="SDV Complete" CssClass="btn btn-primary btn-sm cls-btnSave"
                                Style="margin-bottom: 10px;" Visible="false" OnClick="btnSDV_Click" />
                        </div>
                        <asp:GridView ID="grdData1" runat="server" CellPadding="3" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped Datatable1 margin-top6" OnRowDataBound="gridData1_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                    HeaderStyle-CssClass="txt_center width60px">
                                    <HeaderTemplate>
                                        <a href="JavaScript:ManipulateAll('_grdData1');" id="_Folder" style="color: #333333">
                                            <i id="img_grdData1" class="icon-plus-sign-alt"></i></a>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server" id="anchor">
                                            <a href="JavaScript:divexpandcollapse('_grdData1<%# Eval("VISITNUM") %>');" style="color: #333333">
                                                <i id="img_grdData1<%# Eval("VISITNUM") %>" class="icon-plus-sign-alt"></i></a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VISITNUM" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVISITNUM" Text='<%# Bind("VISITNUM") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VISIT NAME" HeaderStyle-CssClass="left" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVISIT" Text='<%# Bind("VISIT") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                <div style="float: right; font-size: 13px;">
                                                </div>
                                                <div>
                                                    <%--<div class="rows">
                                                                                                    <div class="col-md-12">--%>
                                                    <div id="_grdData1<%# Eval("VISITNUM") %>" style="display: none; position: relative;
                                                        overflow: auto;">
                                                        <asp:GridView ID="grdData2" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                            CssClass="ChildGrid table table-bordered table-striped" OnRowDataBound="gridData2_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID" Text='<%# Bind("ID") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="MODULEID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="MODULE NAME" HeaderStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SDV" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="txt_center"
                                                                    ItemStyle-Width="33%" HeaderStyle-Width="33%">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSDV" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
