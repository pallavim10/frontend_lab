<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RESOURCES_PD_Category.aspx.cs" Inherits="CTMS.RESOURCES_PD_Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/jscript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Protocol Deviaation Categories
                    </h3>
                </div>
            </div>
            <div class="box-body">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <ul class="nav nav-tabs">
                    <li runat="server" id="li1">
                        <asp:LinkButton runat="server" ID="lbtnli1" OnClick="lbtnli1_Click">Tabular View</asp:LinkButton>
                    </li>
                    <li runat="server" id="li2">
                        <asp:LinkButton runat="server" ID="lbtnli2" OnClick="lbtnli2_Click">Form View</asp:LinkButton>
                    </li>
                </ul>
                <div class="tab">
                    <div id="tab2" runat="server" visible="false">
                        <div class="pull-right" style="margin-right: 20px;">
                            <asp:Label ID="lblpagination" runat="server" Font-Bold="true" CssClass="form-control"></asp:Label>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Category :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpCategory" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                        OnSelectedIndexChanged="drpCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Sub-Category :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpSubCategory" runat="server" CssClass="form-control width250px"
                                        AutoPostBack="True" OnSelectedIndexChanged="drpSubCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Factor :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblFactor" Style="min-height: 22px; height: auto" Width="727px"
                                        TextMode="MultiLine" CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Classification :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpClassification" runat="server" CssClass="form-control  width250px"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpClassification_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Impact :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpImpact" runat="server" CssClass="form-control  width250px"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpImpact_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Rationale - Case Examples : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblrationalExample" Style="min-height: 22px; height: auto"
                                        Width="727px" TextMode="MultiLine" CssClass="form-control "> 
                                    </asp:Label>
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
                                    Data to be used for analysis? : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblDataAnalysis" Style="min-height: 22px; height: auto"
                                        Width="727px" TextMode="MultiLine" CssClass="form-control "> 
                                    </asp:Label>
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
                                    <h4 class="box-title">
                                        Map with Risk Bank
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Category :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpRiskCategory" runat="server" CssClass="form-control  width250px"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpRiskCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Sub-Category :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpRiskSubCategory" runat="server" CssClass="form-control  width250px"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpRiskSubCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Factor :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpRiskFactor" runat="server" CssClass="form-control  width250px">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    &nbsp;
                                </div>
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="pull-left">
                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="btn btn-primary btn-sm"
                                    Style="margin-left: 20px;" OnClick="btnPrevious_Click" />
                            </div>
                            <div class="txt_center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm"
                                    OnClick="btnSubmit_Click" />
                                <div class="pull-right">
                                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-primary btn-sm"
                                        Style="margin-right: 30px;" OnClick="btnNext_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <asp:HiddenField ID="hdnCurrentID" runat="server" />
                        <asp:HiddenField ID="hdnNEXTID" runat="server" />
                        <asp:HiddenField ID="hdnPREVID" runat="server" />
                        <asp:HiddenField ID="hdnPAGINATION" runat="server" />
                        <asp:HiddenField ID="hdnTotal" runat="server" />
                    </div>
                    <div id="tab1" runat="server" visible="true">
                        <div class="rows">
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="label col-md-2">
                                        Category :&nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpTabCategory" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                            OnSelectedIndexChanged="drpTabCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="label col-md-2">
                                        Sub-Category :&nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpTabSubCategory" runat="server" CssClass="form-control width250px"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpTabSubCategory_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="label col-md-2">
                                        Classification :&nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpTabClassfication" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                            OnSelectedIndexChanged="drpTabClassfication_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="label col-md-2">
                                        Impact :&nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpTabImpact" runat="server" CssClass="form-control width250px"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpTabImpact_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div style="width: 100%; overflow: auto;">
                                        <div>
                                            <asp:GridView ID="grdPDCAT" runat="server" AutoGenerateColumns="false" OnPreRender="grd_data_PreRender"
                                                CssClass="table table-bordered Datatable table-striped notranslate">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Category" runat="server" Text='<%# Bind("Category") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub-Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SubCategory" runat="server" Text='<%# Bind("Subcategory") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Factor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Factor" runat="server" Text='<%# Bind("Factor") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Classification">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Classification" runat="server" Text='<%# Bind("Classification") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Impact">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Impact" runat="server" Text='<%# Bind("Impact") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rationale - Case Examples">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Rationale_Case_Examples" runat="server" Text='<%# Bind("Rationale_Case_Examples") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Data to be used for analysis?">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Data_to_be_Use" runat="server" Text='<%# Bind("Data_to_be_Use") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
