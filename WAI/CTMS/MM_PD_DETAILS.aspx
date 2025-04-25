<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MM_PD_DETAILS.aspx.cs" Inherits="CTMS.MM_PD_DETAILS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

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

        function ClickNav(ID) {

            var TabElement = $($('#' + ID).find('a'));

            $(TabElement).parent().addClass("active");
            $(TabElement).parent().siblings().removeClass("active");
            var tab = $(TabElement).attr("href");
            $(".tab-content").not(tab).css("display", "none");
            $(tab).fadeIn();

        }
    </script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
    <div class="box" style="z-index: 9999">
        <div id="tabscontainer" class="nav-tabs-custom" runat="server">
            <ul class="nav nav-tabs">
                <li id="li1" runat="server" class="active"><a href="#tab-1" data-toggle="tab">Details</a></li>
                <li id="li2" runat="server"><a href="#tab-2" data-toggle="tab">Other Related</a></li>
                <li id="li3" runat="server"><a href="#tab-3" data-toggle="tab">Site<asp:Label runat="server" ID="lblSite"></asp:Label>- Related </a></li>
                <li id="li4" runat="server"><a href="#tab-4" data-toggle="tab">Subject<asp:Label runat="server" ID="lblSubjectID"></asp:Label>- Protocol Deviations </a></li>
                <li id="li5" runat="server"><a href="#tab-5" data-toggle="tab">Audit Trail</a></li>
            </ul>
            <div class="tab">
                <div id="tab-1" class="tab-content current">
                    <asp:ListView ID="lstPDdetails" runat="server" AutoGenerateColumns="false">
                        <GroupTemplate>
                            <div class="col-md-6">
                                <asp:LinkButton ID="itemPlaceholder" runat="server" />
                            </div>
                        </GroupTemplate>
                        <ItemTemplate>
                            <div class="col-md-12 label" style="display: inline-flex;">
                                <div class="col-md-5">
                                    <asp:Label runat="server" ID="lblName" Text='<%# Eval("NAME") +" :"%>' Width="90%"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblVal" Text='<%# Bind("VAL") %>' CssClass="form-control" Width="100%" Height="100%" TextMode="MultiLine"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:ListView>

                </div>
                <div id="tab-2" class="tab-content">
                    <div class="row">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdRelated" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate"
                                    OnRowDataBound="grdPROTVIOL_RowDataBound" OnRowCommand="grdPROTVIOL_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <div class="txt_center" style="display: inline-flex;">
                                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="View">
                                                        <i title="View" class="fa fa-search" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tab-3" class="tab-content current">
                    <div class="row">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdSiteRelated" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate"
                                    OnRowDataBound="grdPROTVIOL_RowDataBound" OnRowCommand="grdPROTVIOL_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <div class="txt_center" style="display: inline-flex;">
                                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="View">
                                                        <i title="View" class="fa fa-search" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tab-4" class="tab-content">
                    <div class="row">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdSubjectRelated" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate"
                                    OnRowDataBound="grdPROTVIOL_RowDataBound" OnRowCommand="grdPROTVIOL_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <div class="txt_center" style="display: inline-flex;">
                                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="View">
                                                        <i title="View" class="fa fa-search" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tab-5" class="tab-content">
                    <div class="row">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="grdAuditTrail" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">&nbsp;</h3>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Classification by medical</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="col-md-5">
                                        <label>Process:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" ID="ddlPROCESS" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-5">
                                        <label>Category:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" ID="ddlCAT" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="col-md-5">
                                        <label>Sub-Category:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList runat="server" ID="ddlSUBCAT" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-5">
                                        <label>Classification by category:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label runat="server" ID="lblCLASS" CssClass="form-control" Width="100%"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="col-md-5">
                                <label>Classification by medical:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="ddlCLASS_MED" CssClass="form-control" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="col-md-5">
                                <label>Impact by medical:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtIMPACT_MED" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="1000" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-5">
                                <label>Notes by medical:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtNOTES_MED" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="1000" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Classification/Summary by sponsor</h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="col-md-5">
                                <label>Final Classification:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="ddlCLASS_SPONSOR" CssClass="form-control" Width="100%">
                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Minor" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Major" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div class="col-md-5">
                                <label>Final summary:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtSUMMARY_SPONSOR" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="1000" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-5">
                                <label>Notes by sponsor:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtNOTES_SPONSOR" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="1000" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
    <div class="box box-warning">
        <div class="box-body">
            <div class="form-group">
                <br />
                <div class="row">
                    <div class="col-md-12 txtCenter">
                        <asp:LinkButton runat="server" ID="btnSubmit" CssClass="btn btn-success btn-sm cls-btnSave" Text="Submit" ForeColor="White" OnClick="btnSubmit_Click"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="btnCancel" CssClass="btn btn-sm btn-danger" Text="Cancel" ForeColor="White" OnClick="btnCancel_Click"></asp:LinkButton>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
