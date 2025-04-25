<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_KITS_EXP_LIST.aspx.cs" Inherits="CTMS.NIWRS_KITS_EXP_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<%--    <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <style type="text/css">
        .fontBlue {
            color: Blue;
            cursor: pointer;
        }

        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup1 {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            min-width: 500px;
            max-width: 650px;
        }

        h5 {
            background-color: #007bff;
            padding: 0.4em 1em;
            margin-top: 0px;
            font-weight: bold;
            color: white;
        }

        .modal-body {
            position: relative;
            padding: 0px;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(this).parent().parent().parent().find('.tab-content').not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);

            if (div.style.display == "none") {
                div.style.display = "inline";

            } else {
                div.style.display = "none";
            }
        }

    </script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="List of Expiry Update Requests"></asp:Label></h3>
        </div>
        <div class="form-group">
            <div class="has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <br />
            <div class="rows">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvREUQESTS" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="false"
                            OnRowDataBound="gvREUQESTS_RowDataBound" CssClass="table table-bordered txt_center table-striped notranslate"
                            OnRowCommand="gvREUQESTS_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Request generated for">
                                    <ItemTemplate>
                                        <asp:Label ID="Criteria" runat="server" Text='<%# Bind("Criteria") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Levels">
                                    <ItemTemplate>
                                        <asp:Label ID="LEVELS" runat="server" Text='<%# Bind("LEVELS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Countries">
                                    <ItemTemplate>
                                        <asp:Label ID="COUNTRYIDS" runat="server" Text='<%# Bind("COUNTRYIDS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sites">
                                    <ItemTemplate>
                                        <asp:Label ID="SITEIDS" runat="server" Text='<%# Bind("SITEIDS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason for Expiry Update">
                                    <ItemTemplate>
                                        <asp:Label ID="REQUEST_COMMENT" runat="server" Text='<%# Bind("REQUEST_COMMENT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New Expiry Update">
                                    <ItemTemplate>
                                        <asp:Label ID="REQUEST_EXPDAT" runat="server" Text='<%# Bind("REQUEST_EXPDAT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label>Request Details </label>
                                        <br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server">
                                            <div>
                                                <asp:Label ID="REQUESTBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("REQUESTBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="REQUEST_CAL_DAT" runat="server" Text='<%# Bind("REQUEST_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="REQUEST_CAL_TZDAT" runat="server" Text='<%# Eval("REQUEST_CAL_TZDAT")+" , "+Eval("REQUEST_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnApprove" Text="Approve" CommandName="Approved" CommandArgument='<%# Bind("ID") %>' CssClass="btn btn-success" ForeColor="White"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reject">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnReject" Text="Reject" CommandName="Rejected" CommandArgument='<%# Bind("ID") %>' CssClass="btn btn-danger" ForeColor="White"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Show Requested Kits">
                                    <ItemTemplate>
                                        <a href="JavaScript:divexpandcollapse('_KIT_<%# Container.DataItemIndex + 1 %>');"
                                            style="color: #333333">
                                            <asp:Label ID="SHOWKITS" CssClass="fontBlue" runat="server" Text="Show kits"></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%" style="padding: 2px; padding-left: 2%;">
                                                <div style="float: right; font-size: 13px;">
                                                </div>
                                                <div>
                                                    <div id="_KIT_<%# Container.DataItemIndex + 1 %>" style="display: none; position: relative; overflow: auto;">

                                                        <div id="tabscontainer1" class="nav-tabs-custom" runat="server">
                                                            <div class="nav-tabs-custom">
                                                                <ul class="nav nav-tabs">
                                                                    <li id="li1_1" runat="server" class="active"><a href="#tab1-1_<%# Container.DataItemIndex + 1 %>" data-toggle="tab">Central Inventory &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_POOL"></asp:Label>
                                                                    </a></li>
                                                                    <li id="li1_2" runat="server"><a href="#tab1-2_<%# Container.DataItemIndex + 1 %>" data-toggle="tab">Central to Country Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_COUNTRY_ORDERS"></asp:Label>
                                                                    </a></li>
                                                                </ul>
                                                                <div class="tab">
                                                                    <div id="tab1-1_<%# Container.DataItemIndex + 1 %>" class="tab-content current">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="grd_NIWRS_KITS_POOL" runat="server" AutoGenerateColumns="true"
                                                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <div id="tab1-2_<%# Container.DataItemIndex + 1 %>" class="tab-content">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="grd_NIWRS_KITS_COUNTRY_ORDERS" runat="server" AutoGenerateColumns="true"
                                                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div id="tabscontainer2" class="nav-tabs-custom" runat="server">
                                                            <div class="nav-tabs-custom">
                                                                <ul class="nav nav-tabs">
                                                                    <li id="li2_1" runat="server" class="active"><a href="#tab2-1_<%# Container.DataItemIndex + 1 %>" data-toggle="tab">Country Inventory &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_COUNTRY"></asp:Label>
                                                                    </a></li>
                                                                    <li id="li2_2" runat="server"><a href="#tab2-2_<%# Container.DataItemIndex + 1 %>" data-toggle="tab">Country to Site Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_SITE_ORDERS"></asp:Label>
                                                                    </a></li>
                                                                    <li id="li2_3" runat="server"><a href="#tab2-3_<%# Container.DataItemIndex + 1 %>" data-toggle="tab">Country to Country Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_COUNTRY_TRANSF_ORDERS"></asp:Label>
                                                                    </a></li>
                                                                    <li id="li2_4" runat="server"><a href="#tab2-4_<%# Container.DataItemIndex + 1 %>" data-toggle="tab">Country to Central Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS"></asp:Label>
                                                                    </a></li>
                                                                </ul>
                                                                <div class="tab">
                                                                    <div id="tab2-1_<%# Container.DataItemIndex + 1 %>" class="tab-content current">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="grd_NIWRS_KITS_COUNTRY" runat="server" AutoGenerateColumns="true"
                                                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <div id="tab2-2_<%# Container.DataItemIndex + 1 %>" class="tab-content">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="grd_NIWRS_KITS_SITE_ORDERS" runat="server" AutoGenerateColumns="true"
                                                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <div id="tab2-3_<%# Container.DataItemIndex + 1 %>" class="tab-content">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="grd_NIWRS_KITS_COUNTRY_TRANSF_ORDERS" runat="server" AutoGenerateColumns="true"
                                                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <div id="tab2-4_<%# Container.DataItemIndex + 1 %>" class="tab-content">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="grd_NIWRS_KITS_COUNTRY_CENTRAL_ORDERS" runat="server" AutoGenerateColumns="true"
                                                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div id="tabscontainer3" class="nav-tabs-custom" runat="server">
                                                            <div class="nav-tabs-custom">
                                                                <ul class="nav nav-tabs">
                                                                    <li id="li3_1" runat="server" class="active"><a href="#tab3-1_<%# Container.DataItemIndex + 1 %>" data-toggle="tab">Site Inventory &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_SITE"></asp:Label>
                                                                    </a></li>
                                                                    <li id="li3_2" runat="server"><a href="#tab3-2_<%# Container.DataItemIndex + 1 %>" data-toggle="tab">Site to Site Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_SITE_TRANSF_ORDERS"></asp:Label>
                                                                    </a></li>
                                                                    <li id="li3_3" runat="server"><a href="#tab3-3_<%# Container.DataItemIndex + 1 %>" data-toggle="tab">Site to Country Order (In Transit) &nbsp;&nbsp;:&nbsp;&nbsp;
                                                        <asp:Label runat="server" ID="lbl_NIWRS_KITS_SITE_COUNTRY_ORDERS"></asp:Label>
                                                                    </a></li>
                                                                </ul>
                                                                <div class="tab">
                                                                    <div id="tab3-1_<%# Container.DataItemIndex + 1 %>" class="tab-content current">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="grd_NIWRS_KITS_SITE" runat="server" AutoGenerateColumns="true"
                                                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <div id="tab3-2_<%# Container.DataItemIndex + 1 %>" class="tab-content">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="grd_NIWRS_KITS_SITE_TRANSF_ORDERS" runat="server" AutoGenerateColumns="true"
                                                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <div id="tab3-3_<%# Container.DataItemIndex + 1 %>" class="tab-content">
                                                                        <div class="form-group">
                                                                            <asp:GridView ID="grd_NIWRS_KITS_SITE_COUNTRY_ORDERS" runat="server" AutoGenerateColumns="true"
                                                                                CssClass="table table-bordered table-striped Datatable txtCenter" OnPreRender="GridView_PreRender">
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
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
                <br />
            </div>
        </div>
    </div>
    <cc1:ModalPopupExtender ID="modalApp" runat="server" PopupControlID="pnlApp" TargetControlID="Button_Popup" BackgroundCssClass="Background"></cc1:ModalPopupExtender>
    <asp:Panel ID="pnlApp" runat="server" Style="display: none;" CssClass="Popup1">
        <asp:Button runat="server" ID="Button_Popup" Style="display: none" />
        <h5 class="heading">Approval/Rejection Comment</h5>
        <asp:HiddenField runat="server" ID="hdnApproval" />
        <asp:HiddenField runat="server" ID="hdnREQUESTID" />
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-4">
                    <label>
                        Please enter comment :</label>
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" MaxLength="200" CssClass="form-control  width300px" Rows="4"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="row" style="margin-bottom: 10px;">
            <div class="col-md-2">
                &nbsp;
            </div>
            <div class="col-md-10 txt_center">
                <asp:Button ID="btnSumit" runat="server" CssClass="btn btn-success" Height="30px" Width="60px" Text="Submit" OnClick="btnSumit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" Height="30px" Width="60px" OnClick="btnCancel_Click" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
