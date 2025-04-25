<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RESOURCES_Risk_Indicators.aspx.cs" Inherits="CTMS.RESOURCES_Risk_Indicators" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/jscript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false
            });

            $('.select').select2();

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
                        Risk Indicator
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
                                    Risk Indicator :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblRiskIndi" CssClass="form-control  width250px"></asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    Experience :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblExp" CssClass="form-control  width250px"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Discussion/Details :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblDetails" CssClass="form-control" Style="display: inline-block;
                                        width: 727px; min-height: 22px; height: auto"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Relative Value :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblRelativeValue" CssClass="form-control  width250px"></asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    Core or Specific :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblCoreSpecific" CssClass="form-control  width250px"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Level of Scrutiny :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblLevelSec" CssClass="form-control  width250px"></asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    Frequency :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblFreq" CssClass="form-control  width250px"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Threshold Basis : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblThreshold" Style="min-height: 22px; height: auto"
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
                                    Scorecard :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblScorecard" CssClass="form-control  width250px"></asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    Weighting :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblWeighting" CssClass="form-control  width250px"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Mitigation Actions :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:Label runat="server" ID="lblmitigation" CssClass="form-control  width250px"></asp:Label>
                                </div>
                                <div class="label col-md-2">
                                    RACT Traceability :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtract" CssClass="form-control  width250px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Primary PI :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtprimarypi" CssClass="form-control  width250px"></asp:TextBox>
                                </div>
                                <div class="label col-md-2">
                                    Secondary PI :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtsecpi" CssClass="form-control  width250px"></asp:TextBox>
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
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Event Description (Project Level) : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtRiskDescription" Height="36px" Width="727px" TextMode="MultiLine"
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
                                    Event Description (Country Level) : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtRiskDescriptionC" Height="36px" Width="727px"
                                        TextMode="MultiLine" CssClass="form-control "> 
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
                                    Event Description (Inv. Level) : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox runat="server" ID="txtRiskDescriptionInv" Height="36px" Width="727px"
                                        TextMode="MultiLine" CssClass="form-control "> 
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
                                    Impacts : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:ListBox ID="lstRiskImpact" runat="server" CssClass="select" SelectionMode="Multiple"
                                        Width="727px"></asp:ListBox>
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
                                    Risk Type : &nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:ListBox ID="lstRiskType" runat="server" CssClass="select" SelectionMode="Multiple"
                                        Width="727px"></asp:ListBox>
                                </div>
                                <div class="label col-md-2">
                                </div>
                                <div class="col-md-4">
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
                                        Relative Value :&nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpTabRelativeValue" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                            OnSelectedIndexChanged="drpTabRelativeValue_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="label col-md-2">
                                        Frequency :&nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpTabFreq" runat="server" CssClass="form-control width250px"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpTabFreq_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="label col-md-2">
                                        Level of Scrutiny :&nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpTabLevelSec" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                            OnSelectedIndexChanged="drpTabLevelSec_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="label col-md-2">
                                        Core or Specific :&nbsp;
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="drpTabCoreSpecific" runat="server" CssClass="form-control width250px"
                                            AutoPostBack="True" OnSelectedIndexChanged="drpTabCoreSpecific_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div style="width: 100%; overflow: auto;">
                                        <div>
                                            <asp:GridView ID="grdRiskIndicators" runat="server" AutoGenerateColumns="false" OnPreRender="grd_data_PreRender"
                                                CssClass="table table-bordered Datatable table-striped notranslate">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Category" runat="server" Text='<%# Bind("Category") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub-Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SubCategory" runat="server" Text='<%# Bind("Sub_Category") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Risk Indicator">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RiskIndicator" runat="server" Text='<%# Bind("Risk_Indicator") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Experience">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Experience" runat="server" Text='<%# Bind("Experience") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discussion/Details">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DiscussionDetails" runat="server" Text='<%# Bind("Discussion_Details") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Relative Value" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RelativeValue" runat="server" Text='<%# Bind("Relative_Value") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Core or Specific" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CoreorSpecific" runat="server" Text='<%# Bind("Core_or_Specific") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Level of Scrutiny" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LevelofScrutiny" runat="server" Text='<%# Bind("Level_of_Scrutiny") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Frequency" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Frequency" runat="server" Text='<%# Bind("Frequency") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Threshold Basis" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThresholdBasis" runat="server" Text='<%# Bind("Threshold_Basis") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Scorecard" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Scorecard" runat="server" Text='<%# Bind("Scorecard") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Weighting" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Weighting" runat="server" Text='<%# Bind("Weighting") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mitigation Actions" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="MitigationActions" runat="server" Text='<%# Bind("Mitigation_Actions") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RACT Traceability" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RACTTraceability" runat="server" Text='<%# Bind("RACT_Traceability") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Primary PI" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PrimaryPI" runat="server" Text='<%# Bind("Primary_PI") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Secondary PI" ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SecondaryPI" runat="server" Text='<%# Bind("Secondary_PI") %>' />
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
