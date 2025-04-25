<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RESOURCES_RACT_MITIGATION.aspx.cs" Inherits="CTMS.RESOURCES_RACT_MITIGATION" %>

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
                        RACT with Mitigation
                    </h3>
                    <div class="pull-right" style="margin-right: 20px;">
                        <asp:Label ID="lblpagination" runat="server" Font-Bold="true" CssClass="form-control"></asp:Label>
                    </div>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    Category :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpCategory" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                        OnSelectedIndexChanged="drpCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    Reference ID :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblrefId" Style="min-height: 22px; height: auto" Width="250px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    Objective :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblObjectie" Style="min-height: 22px; height: auto" Width="720px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    RACT.Questions for Discussion :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblRACTQUE" Style="min-height: 22px; height: auto" Width="720px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    Considerations :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblConsiderations" Style="min-height: 22px; height: auto" Width="720px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    Examples for Considering High Risk :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblHightRisk" Style="min-height: 22px; height: auto" Width="720px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    Examples for considering medium risk :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblMediumRisk" Style="min-height: 22px; height: auto" Width="720px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    Examples for considering low risk :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblLowRisk" Style="min-height: 22px; height: auto" Width="720px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    Mitigation.Questions for Discussion :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblDisc" Style="min-height: 22px; height: auto" Width="720px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-3">
                                    Potential controls/mitigation actions :&nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" ID="lblpotential" Style="min-height: 22px; height: auto" Width="720px" TextMode="MultiLine"
                                        CssClass="form-control "> 
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="pull-left">
                                <asp:Button ID="btnPrevious" runat="server" Text="Previous" CssClass="btn btn-primary btn-sm"
                                    Style="margin-left: 20px;" OnClick="btnPrevious_Click" />
                            </div>
                            <div class="pull-right">
                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn btn-primary btn-sm"
                                    Style="margin-right: 30px;" OnClick="btnNext_Click" />
                            </div>
                        </div>
                        <br />
                        <asp:HiddenField ID="hdnCurrentID" runat="server" />
                        <asp:HiddenField ID="hdnNEXTID" runat="server" />
                        <asp:HiddenField ID="hdnPREVID" runat="server" />
                        <asp:HiddenField ID="hdnPAGINATION" runat="server" />
                        <asp:HiddenField ID="hdnTotal" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
